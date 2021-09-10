using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    public class GameMode
    {
        public Effect StartingEffect;
        public Effect SharedStartingEffect;
        public List<GamePhase> GamePhases = new List<GamePhase>();
        public List<GameVariant> Variants = new List<GameVariant>();
        public List<Action> SharedUserActions = new List<Action>();
        public List<Action> SharedAlwaysExecuteActions = new List<Action>();
        public List<Action> AlwaysExecuteActions = new List<Action>();

        public GameVariant GetVariant(int playerCount)
        {
            GameVariant bestChoice = null;
            foreach(GameVariant variant in Variants)
            {
                if (variant.Roles.Count <= playerCount)
                {
                    if (bestChoice == null || bestChoice.Roles.Count < variant.Roles.Count)
                    {
                        bestChoice = variant;
                    }
                }
            }
            return bestChoice;
        }
    }
}
