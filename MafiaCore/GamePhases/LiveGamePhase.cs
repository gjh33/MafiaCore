using System;
using System.Collections.Generic;

namespace MafiaCore
{
    [Serializable]
    public class LiveGamePhase : GamePhase
    {
        public List<Action> PermittedActions = new List<Action>();
        
        public override void OnBegin(Game game)
        {
        }

        public override void OnEnd(Game game)
        {
        }

        public override void Request(Game game, Action action, Player requester, List<InputEntry> inputs)
        {
            if (action.ExecutionCondition.Evaluate(new ExecutionParams(requester, game.Context, inputs)))
            {
                action.ExecutionEffect.Apply(new ExecutionParams(requester, game.Context, inputs));
            }
        }

        public override bool ActionPermitted(Action action)
        {
            return PermittedActions.Contains(action);
        }
    }
}