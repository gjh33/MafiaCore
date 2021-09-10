using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    public class TargetPlayerSelector : ISelector<Player>
    {
        public ISelector<Context> ContextSelector;
        public ISelector<string> TargetSelector;

        public Player Select(ExecutionParams executionContext)
        {
            Context selectedContext = ContextSelector.Select(executionContext);
            if (selectedContext == null) return null;
            string selectedTarget = TargetSelector.Select(executionContext);
            if (string.IsNullOrEmpty(selectedTarget)) return null;
            return selectedContext.GetTarget(selectedTarget);
        }
    }
}
