using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    [Serializable]
    public class LocalContextSelector : Selector<Context>
    {
        public override Context Select(ExecutionParams executionContext)
        {
            return executionContext.RequestContext;
        }
    }
}
