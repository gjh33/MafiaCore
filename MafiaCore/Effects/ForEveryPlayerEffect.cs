using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Effects
{
    [Serializable]
    public class ForEveryPlayerEffect : Effect
    {
        public Effect ForEachEffect;

        private int index = 0;

        public override void Apply(ExecutionParams executionContext)
        {
            for (index = 0; index < executionContext.GameContext.Players.Count; index++)
            {
                ForEachEffect.Apply(executionContext);
            }
        }

        public Player Select(ExecutionParams executionContext)
        {
            if (index < 0 || index >= executionContext.GameContext.Players.Count) return null;
            return executionContext.GameContext.Players[index];
        }
    }
}
