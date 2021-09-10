using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    public class PlayerContextSelector : ISelector<Context>
    {
        public ISelector<Player> PlayerSelector;

        public Context Select(ExecutionParams executionContext)
        {
            Player selectedPlayer = PlayerSelector.Select(executionContext);
            if (selectedPlayer == null) return null;
            return selectedPlayer.Context;
        }
    }
}
