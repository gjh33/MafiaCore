using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Effects
{
    [Serializable]
    public class SetCounterEffect : Effect
    {
        public Selector<Context> ContextSelector;
        public Selector<string> CounterSelector;
        public Selector<int> ValueSelector;


        public override void Apply(ExecutionParams executionContext)
        {
            Context selectedContext = ContextSelector.Select(executionContext);
            if (selectedContext == null) return;
            string selectedCounter = CounterSelector.Select(executionContext);
            if (string.IsNullOrEmpty(selectedCounter)) return;
            selectedContext.SetCounter(selectedCounter, ValueSelector.Select(executionContext));
        }
    }
}
