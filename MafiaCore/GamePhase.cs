using System;
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
        public abstract void Request(Game game, Action action, Player requester, Context localContext);
        public abstract bool ActionPermitted(Action action);
    }
}
