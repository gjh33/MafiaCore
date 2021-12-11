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
        public List<InputEntry> InputEntries;

        public ExecutionParams(Player executingPlayer, GameContext gameContext, List<InputEntry> inputs)
        {
            ExecutingPlayer = executingPlayer;
            RequestContext = new Context();
            GameContext = gameContext;
            InputEntries = inputs;
        }
    }
}
