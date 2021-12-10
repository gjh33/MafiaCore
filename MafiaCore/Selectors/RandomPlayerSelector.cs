using System;

namespace MafiaCore.Selectors
{
    [Serializable]
    public class RandomPlayerSelector : Selector<Player>
    {
        public override Player Select(ExecutionParams executionContext)
        {
            int index = executionContext.GameContext.rng.Next(0, executionContext.GameContext.Players.Count);
            return executionContext.GameContext.Players[index];
        }
    }
}