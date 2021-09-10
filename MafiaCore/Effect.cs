using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    public abstract class Effect
    {
        public abstract void Apply(ExecutionParams context);
    }
}
