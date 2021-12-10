using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    [Serializable]
    public class Team
    {
        public Condition WinCondition;
        public Effect StartingEffect;
        public Effect SharedStartingEffect;
        public List<Action> SharedUserActions = new List<Action>();
        public List<Action> SharedAlwaysExecuteActions = new List<Action>();
        public List<Action> AlwaysExecuteActions = new List<Action>();
    }
}
