using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Effects
{
    public class BranchEffect : Effect
    {
        public Condition Condition;
        public Effect TrueEffect;
        public Effect FalseEffect;

        public override void Apply(ExecutionParams context)
        {
            if (Condition.Evaluate(context))
            {
                if (TrueEffect == null) return;
                TrueEffect.Apply(context);
            }
            else
            {
                if (FalseEffect == null) return;
                FalseEffect.Apply(context);
            }
        }
    }
}
