using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Conditions
{
    public class HasFlagCondition : Condition
    {
        public ISelector<Context> ContextSelector;
        public ISelector<string> FlagSelector;

        public override bool Evaluate(ExecutionParams context)
        {
            Context selectedContext = ContextSelector.Select(context);
            if (selectedContext == null) return false;
            string flag = FlagSelector.Select(context);
            if (string.IsNullOrEmpty(flag)) return false;
            return selectedContext.HasFlag(flag);
        }
    }
}
