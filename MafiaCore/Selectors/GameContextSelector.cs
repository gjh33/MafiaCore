using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    public class GameContextSelector : Selector<Context>
    {
        public override Context Select(ExecutionParams executionContext)
        {
            return executionContext.GameContext;
        }
    }
}
