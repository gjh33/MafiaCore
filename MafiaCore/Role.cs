using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    [Serializable]
    public class Role
    {
        public string Name;
        public Team Team;
        public Condition WinCondition;
        public List<Action> PlayerActions = new List<Action>();
        public List<Action> AlwaysExecuteActions = new List<Action>();
        public Effect StartingEffect;
    }
}
