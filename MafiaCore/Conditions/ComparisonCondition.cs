using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Conditions
{
    public class ComparisonCondition : Condition
    {
        public enum ComparisonOperator { GreaterThan, GreaterThanOrEqualTo, EqualTo, LessThanOrEqualTo, LessThan }

        public ComparisonOperator Operator;
        public ISelector<int> LeftOperandValueSelector;
        public ISelector<int> RightOperandValueSelector;

        public override bool Evaluate(ExecutionParams context)
        {
            int left = LeftOperandValueSelector.Select(context);
            int right = RightOperandValueSelector.Select(context);

            switch (Operator)
            {
                case ComparisonOperator.GreaterThan:
                    return left > right;
                case ComparisonOperator.GreaterThanOrEqualTo:
                    return left >= right;
                case ComparisonOperator.EqualTo:
                    return left == right;
                case ComparisonOperator.LessThanOrEqualTo:
                    return left <= right;
                case ComparisonOperator.LessThan:
                    return left < right;
                default:
                    return false;
            }
        }
    }
}
