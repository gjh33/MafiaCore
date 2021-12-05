using System;
using System.Collections.Generic;

namespace MafiaCore
{
    /// <summary>
    /// The primary Game class that executes the game logic using data classes.
    /// 
    /// Any actions or effects are performed in the following order:
    /// Game Mode's Effects/Actions
    /// Teams' Effects/Actions
    /// Each Player's Game Mode Shared Effects/Actions
    /// Each Player's Team's Shared Effects/Actions
    /// Each Player's Role's Effects/Actions
    /// Each Player's Requested Actions
    /// 
    /// The exception being actions which are always executed in pipeline order
    /// first, and ordered by the above second.
    /// </summary>
    public class Game
    {
        public enum State { PreGame, Playing, Finished }

        // Primary sata set initially
        public GameMode GameMode;
        public List<Player> Players = new List<Player>();

        // Secondary data that's computed
        private List<Team> Teams = new List<Team>();
        private GameVariant GameVariant;

        // Queryable data that changes during game
        public State CurrentState = State.PreGame;
        public GamePhase CurrentGamePhase => GameMode.GamePhases[currentGamePhaseIndex];

        private int currentGamePhaseIndex;
        private GameContext gameContext;
        private ActionExecutionRequestTable actionExecutionRequests = new ActionExecutionRequestTable();

        public Game(GameMode gameMode, IEnumerable<Player> players)
        {
            GameMode = gameMode;
            Players.AddRange(players);
        }

        public void StartGame()
        {
            gameContext = new GameContext();
            gameContext.Players.AddRange(Players);
            GameVariant = GameMode.GetVariant(Players.Count);
            Teams = GameVariant.ComputeTeams();
            currentGamePhaseIndex = 0;
            AssignRoles();
            PerformStartingEffects();
            CurrentState = State.Playing;
        }

        public void EnqueueActionExecutionRequest(Action action, Player executingPlayer, Context requestContext)
        {
            actionExecutionRequests.AddRequest(action, executingPlayer, requestContext);
        }

        public void ExecuteCurrentPhase()
        {
            if (CurrentGamePhase.ExecutionPipeline != null)
            {
                foreach (Action action in CurrentGamePhase.ExecutionPipeline.ActionExecutionOrder)
                {
                    if (GameMode.AlwaysExecuteActions.Contains(action))
                    {
                        if (action.ExecutionCondition.Evaluate(new ExecutionParams(null, gameContext, new Context())))
                        {
                            action.ExecutionEffect.Apply(new ExecutionParams(null, gameContext, new Context()));
                        }
                    }
                    foreach (Team team in GameVariant.ComputeTeams())
                    {
                        if (team.AlwaysExecuteActions.Contains(action))
                        {
                            if (action.ExecutionCondition.Evaluate(new ExecutionParams(null, gameContext, new Context())))
                            {
                                action.ExecutionEffect.Apply(new ExecutionParams(null, gameContext, new Context()));
                            }
                        }
                    }
                    foreach (Player player in Players)
                    {
                        if (GameMode.SharedAlwaysExecuteActions.Contains(action))
                        {
                            if (action.ExecutionCondition.Evaluate(new ExecutionParams(player, gameContext, new Context())))
                            {
                                action.ExecutionEffect.Apply(new ExecutionParams(player, gameContext, new Context()));
                            }
                        }
                    }
                    foreach (Player player in Players)
                    {
                        if (player.Role.Team.SharedAlwaysExecuteActions.Contains(action))
                        {
                            if (action.ExecutionCondition.Evaluate(new ExecutionParams(player, gameContext, new Context())))
                            {
                                action.ExecutionEffect.Apply(new ExecutionParams(player, gameContext, new Context()));
                            }
                        }
                    }
                    foreach (Player player in Players)
                    {
                        if (player.Role.AlwaysExecuteActions.Contains(action))
                        {
                            if (action.ExecutionCondition.Evaluate(new ExecutionParams(player, gameContext, new Context())))
                            {
                                action.ExecutionEffect.Apply(new ExecutionParams(player, gameContext, new Context()));
                            }
                        }
                    }
                    foreach (ActionExecutionRequestTable.Entry entry in actionExecutionRequests.GetExecutionContextsFor(action))
                    {
                        if (action.ExecutionCondition.Evaluate(new ExecutionParams(entry.ExecutingPlayer, gameContext, entry.ExecutionContext)))
                        {
                            action.ExecutionEffect.Apply(new ExecutionParams(entry.ExecutingPlayer, gameContext, entry.ExecutionContext));
                        }
                    }
                }
            }
            actionExecutionRequests.Clear();
        }

        public void MoveToNextPhase()
        {
            actionExecutionRequests.Clear();
            currentGamePhaseIndex++;
            currentGamePhaseIndex %= GameMode.GamePhases.Count;
        }

        public List<Action> GetAllPlayerActionsFor(Player player)
        {
            List<Action> allActions = new List<Action>();
            allActions.AddRange(GameMode.SharedUserActions);
            allActions.AddRange(player.Role.Team.SharedUserActions);
            allActions.AddRange(player.Role.PlayerActions);
            return allActions;
        }

        public List<Action> GetAvailableActionsForPlayer(Player player)
        {
            List<Action> available = new List<Action>();
            foreach (Action action in GetAllPlayerActionsFor(player))
            {
                // If the current game phase supports this action
                if (CurrentGamePhase.ExecutionPipeline.ActionExecutionOrder.Contains(action))
                {
                    // If the action passes it's availability condition
                    if (action.AvailabilityCondition == null || action.AvailabilityCondition.Evaluate(new ExecutionParams(player, gameContext, new Context())))
                    {
                        available.Add(action);
                    }
                }
            }
            return available;
        }

        public List<Team> CheckForWinningTeams()
        {
            List<Team> winningTeams = new List<Team>();
            foreach (Team team in Teams)
            {
                if (team.WinCondition.Evaluate(new ExecutionParams(null, gameContext, new Context())))
                {
                    winningTeams.Add(team);
                }
            }

            return new List<Team>(winningTeams);
        }

        public List<Player> CheckForWinningPlayers()
        {
            List<Player> winners = new List<Player>();
            foreach (Player player in Players)
            {
                if (player.Role.WinCondition == null) continue;
                if (player.Role.WinCondition.Evaluate(new ExecutionParams(player, gameContext, new Context())))
                {
                    winners.Add(player);
                }
            }

            return winners;
        }

        // If there are more players than the current variant, duplicate
        // the last role
        private void AssignRoles()
        {
            // Fill out any missing roles by duplicating last
            List<Role> roles = new List<Role>(GameVariant.Roles);
            Role lastRole = roles[roles.Count - 1];
            for (int i = roles.Count; i < Players.Count; i++)
            {
                roles.Add(lastRole);
            }

            // TODO: Shuffle roles

            for(int i = 0; i < Players.Count; i++)
            {
                Players[i].AssignRole(roles[i]);
            }
        }

        private void PerformStartingEffects()
        {
            GameMode.StartingEffect?.Apply(new ExecutionParams(null, gameContext, new Context()));
            foreach (Team team in GameVariant.ComputeTeams())
            {
                team.StartingEffect?.Apply(new ExecutionParams(null, gameContext, new Context()));
            }
            foreach (Player player in Players)
            {
                GameMode.SharedStartingEffect?.Apply(new ExecutionParams(player, gameContext, new Context()));
            }
            foreach (Player player in Players)
            {
                player.Role.Team.SharedStartingEffect?.Apply(new ExecutionParams(player, gameContext, new Context()));
            }
            foreach (Player player in Players)
            {
                player.Role.StartingEffect?.Apply(new ExecutionParams(player, gameContext, new Context()));
            }
        }
    }
}
