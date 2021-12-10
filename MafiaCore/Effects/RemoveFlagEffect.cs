using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Effects
{
    [Serializable]
    public class RemoveFlagEffect : Effect
    {
        public Selector<Context> ContextSelector;
        public Selector<string> FlagSelector;

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
