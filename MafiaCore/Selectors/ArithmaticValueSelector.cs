using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    public class ArithmaticValueSelector : Selector<int>
    {
        public enum MathOperation { Add, Subtract, Multiply, Divide }

        public MathOperation Operation;
        public Selector<int> LeftOperandValueSelector;
        public Selector<int> RightOperandValueSelector;

        public override int Select(ExecutionParams executionContext)
        {
            int left = LeftOperandValueSelector.Select(executionContext);
            int right = RightOperandValueSelector.Select(executionContext);

            switch (Operation)
            {
                case MathOperation.Add:
                    return left + right;
                case MathOperation.Subtract:
                    return left - right;
                case MathOperation.Multiply:
                    return left * right;
                case MathOperation.Divide:
                    return left / right;
                default:
                    return 0;
            }
        }
    }
}
