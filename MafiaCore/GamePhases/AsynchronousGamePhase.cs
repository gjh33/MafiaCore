using System;
using System.Collections.Generic;

namespace MafiaCore
{
    [Serializable]
    public class AsynchronousGamePhase : GamePhase
    {
        public List<Action> ActionExecutionOrder = new List<Action>();

        private HashSet<ActionRequest> queuedRequests = new HashSet<ActionRequest>();

        public override void Request(Game game, Action action, Player requester, List<InputEntry> inputs)
        {
            queuedRequests.Add(new ActionRequest
            {
                Action = action,
                Requester = requester,
                Inputs = inputs,
            });
        }

        public override bool ActionPermitted(Action action)
        {
            return ActionExecutionOrder.Contains(action);
        }

        public override void OnBegin(Game game)
        {
            queuedRequests.Clear();
        }

        public override void OnEnd(Game game)
        {
            ExecuteRequests(game);
            queuedRequests.Clear();
        }

        private void ExecuteRequests(Game game)
        {
            foreach (Action action in ActionExecutionOrder)
            {
                if (game.GameMode.AlwaysExecuteActions.Contains(action))
                {
                    if (action.ExecutionCondition.Evaluate(new ExecutionParams(null, game.Context, null)))
                    {
                        action.ExecutionEffect.Apply(new ExecutionParams(null, game.Context, null));
                    }
                }

                foreach (Team team in game.Teams)
                {
                    if (team.AlwaysExecuteActions.Contains(action))
                    {
                        if (action.ExecutionCondition.Evaluate(new ExecutionParams(null, game.Context, null)))
                        {
                            action.ExecutionEffect.Apply(new ExecutionParams(null, game.Context, null));
                        }
                    }
                }

                foreach (Player player in game.Players)
                {
                    if (game.GameMode.SharedAlwaysExecuteActions.Contains(action))
                    {
                        if (action.ExecutionCondition.Evaluate(new ExecutionParams(player, game.Context, null)))
                        {
                            action.ExecutionEffect.Apply(new ExecutionParams(player, game.Context, null));
                        }
                    }
                }

                foreach (Player player in game.Players)
                {
                    if (player.Role.Team.SharedAlwaysExecuteActions.Contains(action))
                    {
                        if (action.ExecutionCondition.Evaluate(new ExecutionParams(player, game.Context, null)))
                        {
                            action.ExecutionEffect.Apply(new ExecutionParams(player, game.Context, null));
                        }
                    }
                }

                foreach (Player player in game.Players)
                {
                    if (player.Role.AlwaysExecuteActions.Contains(action))
                    {
                        if (action.ExecutionCondition.Evaluate(new ExecutionParams(player, game.Context, null)))
                        {
                            action.ExecutionEffect.Apply(new ExecutionParams(player, game.Context, null));
                        }
                    }
                }

                foreach (ActionRequest request in queuedRequests)
                {
                    if (request.Action == action)
                    {
                        if (action.ExecutionCondition.Evaluate(new ExecutionParams(request.Requester, game.Context,
                            request.Inputs)))
                        {
                            action.ExecutionEffect.Apply(new ExecutionParams(request.Requester, game.Context,
                                request.Inputs));
                        }
                    }
                }
            }
        }

        [Serializable]
        public struct ActionRequest
        {
            public Action Action;
            public Player Requester;
            public List<InputEntry> Inputs;
        }
    }
}