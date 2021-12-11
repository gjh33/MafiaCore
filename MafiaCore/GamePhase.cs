using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    [Serializable]
    public abstract class GamePhase
    {
        public string Name;

        public abstract void OnBegin(Game game);
        public abstract void OnEnd(Game game);
        public abstract void Request(Game game, Action action, Player requester, List<InputEntry> inputs);
        public abstract bool ActionPermitted(Action action);
    }
}
