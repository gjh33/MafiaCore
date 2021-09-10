using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    class ActionExecutionRequestTable
    {
        private Dictionary<Action, Queue<Entry>> requests = new Dictionary<Action, Queue<Entry>>();

        public void AddRequest(Action action, Player executingPlayer, Context executionContext)
        {
            if (!requests.ContainsKey(action) || requests[action] == null)
            {
                requests[action] = new Queue<Entry>();
            }
            requests[action].Enqueue(new Entry
            {
                ExecutingPlayer = executingPlayer,
                ExecutionContext = executionContext,
            });
        }

        public IEnumerable<Entry> GetExecutionContextsFor(Action action)
        {
            if (!requests.ContainsKey(action) || requests[action] == null) return new Queue<Entry>();
            return requests[action];
        }

        public void Clear()
        {
            requests.Clear();
        }

        public class Entry
        {
            public Player ExecutingPlayer;
            public Context ExecutionContext;
        }
    }
}
