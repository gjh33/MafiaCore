using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    public class Context
    {
        private HashSet<string> flags = new HashSet<string>();
        private Dictionary<string, int> counters = new Dictionary<string, int>();
        private Dictionary<string, Player> targets = new Dictionary<string, Player>();

        public Context() { }

        public Context(Context copy)
        {
            foreach (string flag in flags)
            {
                flags.Add(flag);
            }

            foreach (KeyValuePair<string, int> counter in counters)
            {
                counters.Add(counter.Key, counter.Value);
            }

            foreach (KeyValuePair<string, Player> target in targets)
            {
                targets.Add(target.Key, target.Value);
            }
        }

        public void AddFlag(string flag)
        {
            flags.Add(flag);
        }

        public void RemoveFlag(string flag)
        {
            flags.Remove(flag);
        }

        public bool HasFlag(string flag)
        {
            return flags.Contains(flag);
        }

        public void SetCounter(string counter, int val)
        {
            counters[counter] = val;
        }

        public int GetCounter(string counter)
        {
            if (!counters.ContainsKey(counter)) return 0;
            else return counters[counter];
        }

        public void SetTarget(string target, Player player)
        {
            targets[target] = player;
        }

        public Player GetTarget(string target)
        {
            if (!targets.ContainsKey(target)) return null;
            else return targets[target];
        }

        public void RemoveTarget(string target)
        {
            targets.Remove(target);
        }
    }
}
