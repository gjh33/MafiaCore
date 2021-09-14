using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    public class ConstantSelector<T> : Selector<T>
    {
        public T Value;

        public override T Select(ExecutionParams executionContext)
        {
            return Value;
        }
    }
}
