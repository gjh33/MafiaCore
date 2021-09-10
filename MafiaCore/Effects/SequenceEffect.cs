using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Effects
{
    public class SequenceEffect : Effect
    {
        public List<Effect> SubEffects = new List<Effect>();

        public override void Apply(ExecutionParams context)
        {
            foreach (Effect effect in SubEffects)
            {
                effect.Apply(context);
            }
        }
    }
}
