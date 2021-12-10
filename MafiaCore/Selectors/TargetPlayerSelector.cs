using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    [Serializable]
    public class TargetPlayerSelector : Selector<Player>
    {
        public Selector<Context> ContextSelector;
        public Selector<string> TargetSelector;

        public override Player Select(ExecutionParams executionContext)
        {
            Context selectedContext = ContextSelector.Select(executionContext);
            if (selectedContext == null) return null;
            string selectedTarget = TargetSelector.Select(executionContext);
            if (string.IsNullOrEmpty(selectedTarget)) return null;
            return selectedContext.GetTarget(selectedTarget);
        }
    }
}
