using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Effects
{
    public class ClearTargetEffect : Effect
    {
        public ISelector<Context> ContextSelector;
        public ISelector<string> TargetSelector;

        public override void Apply(ExecutionParams executionContext)
        {
            Context selectedContext = ContextSelector.Select(executionContext);
            if (selectedContext == null) return;
            string selectedTarget = TargetSelector.Select(executionContext);
            if (string.IsNullOrEmpty(selectedTarget)) return;
            selectedContext.RemoveTarget(selectedTarget);
        }
    }
}
