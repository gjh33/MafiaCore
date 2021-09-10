using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    public class ConstantSelector<T> : ISelector<T>
    {
        public T Value;

        public T Select(ExecutionParams executionContext)
        {
            return Value;
        }
    }
}
