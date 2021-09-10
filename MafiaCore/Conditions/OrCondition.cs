using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Conditions
{
    public class OrCondition : Condition
    {
        public List<Condition> SubConditions = new List<Condition>();

        public override bool Evaluate(ExecutionParams context)
        {
            bool answer = false;
            foreach (Condition condition in SubConditions)
            {
                answer |= condition.Evaluate(context);
            }
            return answer;
        }
    }
}
