using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    public class ExecutionParams
    {
        public Player ExecutingPlayer;
        public List<Player> Players;
        public Context RequestContext;
        public GameContext GameContext;

        public ExecutionParams(Player executingPlayer, GameContext gameContext, Context localContext)
        {
            ExecutingPlayer = executingPlayer;
            RequestContext = localContext;
            GameContext = gameContext;
        }

    }
}
