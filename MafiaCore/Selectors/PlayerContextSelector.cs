using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    [Serializable]
    public class PlayerContextSelector : Selector<Context>
    {
        public Selector<Player> PlayerSelector;

        public override Context Select(ExecutionParams executionContext)
        {
            Player selectedPlayer = PlayerSelector.Select(executionContext);
            if (selectedPlayer == null) return null;
            return selectedPlayer.Context;
        }
    }
}
