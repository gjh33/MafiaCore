using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    [Serializable]
    public abstract class Condition
    {
        public abstract bool Evaluate(ExecutionParams context);
    }
}
