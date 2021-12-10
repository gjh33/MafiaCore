using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    [Serializable]
    public class Action
    {
        public string Name;
        public Condition AvailabilityCondition;
        public Effect InstantEffect;
        public Condition ExecutionCondition;
        public Effect ExecutionEffect;

        public List<Selector<string>> PlayerInputTargets;
        public List<Selector<string>> PlayerInputCounters;
        public List<Selector<string>> PlayerInputFlags;
    }
}
