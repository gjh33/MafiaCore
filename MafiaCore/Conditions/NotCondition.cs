using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Conditions
{
    public class NotCondition : Condition
    {
        public Condition Condition;

        public override bool Evaluate(ExecutionParams context)
        {
            return !Condition.Evaluate(context);
        }
    }
}
