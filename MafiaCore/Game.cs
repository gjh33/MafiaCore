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

        public GameMode GameMode => gameMode;
        public State GameState => currentState;
        public GameContext Context => context;
        public IEnumerable<Player> Players => players;
        public IEnumerable<Team> Teams => teams;
        public GamePhase CurrentGamePhase => gameMode.GamePhases[currentGamePhaseIndex];
        public GameVariant Variant => variant;

        public int RngSeed => rngSeed;
        
        private List<Team> teams = new List<Team>();
        private List<Player> players = new List<Player>();
        private GameVariant variant;
        private int currentGamePhaseIndex;
        private State currentState = State.PreGame;
        private GameMode gameMode;
        private GameContext context;
        private int rngSeed;

        public Game(GameMode gameMode, IEnumerable<Player> players)
        {
            this.gameMode = gameMode;
            this.players.AddRange(players);
        }

        public void StartGame()
        {
            int seed = Guid.NewGuid().GetHashCode();
            StartGame(seed);
        }

        public void StartGame(int rngSeed)
        {
            this.rngSeed = rngSeed;
            context = new GameContext();
            context.rng = new Random(rngSeed);
            context.Players.AddRange(players);
            variant = gameMode.GetVariant(players.Count);
            teams = variant.ComputeTeams();
            currentGamePhaseIndex = 0;
            AssignRoles();
            PerformStartingEffects();
            currentState = State.Playing;
        }

        public void MoveToNextPhase()
        {
            CurrentGamePhase?.OnEnd(this);
            currentGamePhaseIndex++;
            currentGamePhaseIndex %= gameMode.GamePhases.Count;
            CurrentGamePhase?.OnBegin(this);
        }

        public List<Action> GetAllPlayerActionsFor(Player player)
        {
            List<Action> allActions = new List<Action>();
            allActions.AddRange(gameMode.SharedUserActions);
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
                if (CurrentGamePhase.ActionPermitted(action))
                {
                    // If the action passes it's availability condition
                    if (action.AvailabilityCondition == null || action.AvailabilityCondition.Evaluate(new ExecutionParams(player, context, null)))
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
            foreach (Team team in teams)
            {
                if (team.WinCondition.Evaluate(new ExecutionParams(null, context, null)))
                {
                    winningTeams.Add(team);
                }
            }

            return new List<Team>(winningTeams);
        }

        public List<Player> CheckForWinningPlayers()
        {
            List<Player> winners = new List<Player>();
            foreach (Player player in players)
            {
                if (player.Role.WinCondition == null) continue;
                if (player.Role.WinCondition.Evaluate(new ExecutionParams(player, context, null)))
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
            List<Role> roles = new List<Role>(variant.Roles);
            Role lastRole = roles[roles.Count - 1];
            for (int i = roles.Count; i < players.Count; i++)
            {
                roles.Add(lastRole);
            }

            // Shuffle a list using swaps
            // Credit: https://stackoverflow.com/a/1262619
            int n = roles.Count;
            while (n > 1)
            {
                n--;
                int k = context.rng.Next(n + 1);
                Role value = roles[k];
                roles[k] = roles[n];
                roles[n] = value;
            }

            // Assign Roles
            for(int i = 0; i < players.Count; i++)
            {
                players[i].AssignRole(roles[i]);
            }
        }

        private void PerformStartingEffects()
        {
            gameMode.StartingEffect?.Apply(new ExecutionParams(null, context, null));
            foreach (Team team in variant.ComputeTeams())
            {
                team.StartingEffect?.Apply(new ExecutionParams(null, context, null));
            }
            foreach (Player player in players)
            {
                gameMode.SharedStartingEffect?.Apply(new ExecutionParams(player, context, null));
            }
            foreach (Player player in players)
            {
                player.Role.Team.SharedStartingEffect?.Apply(new ExecutionParams(player, context, null));
            }
            foreach (Player player in players)
            {
                player.Role.StartingEffect?.Apply(new ExecutionParams(player, context, null));
            }
        }
    }
}
