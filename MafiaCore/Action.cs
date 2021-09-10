using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    public class Action
    {
        public Condition AvailabilityCondition;
        public Condition ExecutionCondition;
        public Effect ExecutionEffect;
    }
}
