using System;

namespace MafiaCore.Selectors
{
    [Serializable]
    public class InputValueSelector<T> : Selector<T>
    {
        public Input<T> Input;
        
        public override T Select(ExecutionParams executionContext)
        {
            foreach (InputEntry entry in executionContext.InputEntries)
            {
                if (entry.GetInput() == Input && entry is InputEntry<T> typedEntry)
                {
                    return typedEntry.Value;
                }
            }

            return default;
        }
    }
}