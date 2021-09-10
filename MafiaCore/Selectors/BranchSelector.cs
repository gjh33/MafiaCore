using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    public class BranchSelector<T> : ISelector<T>
    {
        public Condition Condition;
        public ISelector<T> TrueSelector;
        public ISelector<T> FalseSelector;

        public T Select(ExecutionParams executionContext)
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
