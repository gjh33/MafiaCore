using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    public abstract class Selector<T>
    {
        public abstract T Select(ExecutionParams executionContext);
    }
}
