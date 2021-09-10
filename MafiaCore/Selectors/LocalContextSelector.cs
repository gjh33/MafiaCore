using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    public class LocalContextSelector : ISelector<Context>
    {
        public Context Select(ExecutionParams executionContext)
        {
            return executionContext.RequestContext;
        }
    }
}
