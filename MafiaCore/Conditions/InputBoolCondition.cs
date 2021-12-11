using System;

namespace MafiaCore.Conditions
{
    [Serializable]
    public class InputBoolCondition : Condition
    {
        public Input<bool> Input;
        public override bool Evaluate(ExecutionParams context)
        {
            foreach (InputEntry entry in context.InputEntries)
            {
                if (entry.GetInput() == Input && entry is InputEntry<bool> typedEntry)
                {
                    return typedEntry.Value;
                }
            }

            return default;
        }
    }
}