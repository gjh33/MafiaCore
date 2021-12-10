using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    [Serializable]
    public class ExecutingPlayerSelector : Selector<Player>
    {
        public override Player Select(ExecutionParams executionContext)
        {
            return executionContext.ExecutingPlayer;
        }
    }
}
