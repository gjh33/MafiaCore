using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Conditions
{
    [Serializable]
    public class AndCondition : Condition
    {
        public List<Condition> SubConditions = new List<Condition>();

        public override bool Evaluate(ExecutionParams context)
        {
            bool answer = true;
            foreach (Condition condition in SubConditions)
            {
                answer &= condition.Evaluate(context);
            }
            return answer;
        }
    }
}
