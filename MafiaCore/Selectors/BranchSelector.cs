using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    [Serializable]
    public class BranchSelector<T> : Selector<T>
    {
        public Condition Condition;
        public Selector<T> TrueSelector;
        public Selector<T> FalseSelector;

        public override T Select(ExecutionParams executionContext)
        {
            if (Condition.Evaluate(executionContext))
            {
                if (TrueSelector == null) return default;
                return TrueSelector.Select(executionContext);
            }
            else
            {
                if (FalseSelector == null) return default;
                return FalseSelector.Select(executionContext);
            }
        }
    }
}
