using System;

namespace MafiaCore.Selectors
{
    [Serializable]
    public class RandomNumberSelector : Selector<int>
    {
        public Selector<int> Min;
        public Selector<int> Max;

        public override int Select(ExecutionParams executionContext)
        {
            return executionContext.GameContext.rng.Next(Min.Select(executionContext), Max.Select(executionContext));
        }
    }
}