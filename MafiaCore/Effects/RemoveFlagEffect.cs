using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Effects
{
    public class RemoveFlagEffect : Effect
    {
        public ISelector<Context> ContextSelector;
        public ISelector<string> FlagSelector;

        public override void Apply(ExecutionParams executionContext)
        {
            Context selectedContext = ContextSelector.Select(executionContext);
            if (selectedContext == null) return;
            string selectedFlag = FlagSelector.Select(executionContext);
            if (string.IsNullOrEmpty(selectedFlag)) return;
            selectedContext.RemoveFlag(selectedFlag);
        }
    }
}
