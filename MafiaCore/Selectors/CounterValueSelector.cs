using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    public class CounterValueSelector : Selector<int>
    {
        public Selector<Context> ContextSelector;
        public Selector<string> CounterSelector;

        public override int Select(ExecutionParams executionContext)
        {
            Context selectedContext = ContextSelector.Select(executionContext);
            if (selectedContext == null) return 0;
            string selectedCounter = CounterSelector.Select(executionContext);
            if (string.IsNullOrEmpty(selectedCounter)) return 0;
            return selectedContext.GetCounter(selectedCounter);
        }
    }
}
