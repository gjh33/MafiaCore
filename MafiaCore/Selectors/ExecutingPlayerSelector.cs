using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    public class ExecutingPlayerSelector : ISelector<Player>
    {
        public Player Select(ExecutionParams executionContext)
        {
            return executionContext.ExecutingPlayer;
        }
    }
}
