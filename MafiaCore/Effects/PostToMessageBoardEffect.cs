using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Effects
{
    public class PostToMessageBoardEffect : Effect
    {
        public ISelector<Context> ContextSelector;
        public ISelector<string> MessageSelector;

        public override void Apply(ExecutionParams executionContext)
        {
            Context selectedContext = ContextSelector.Select(executionContext);
            if (selectedContext == null) return;
            string selectedMessage = MessageSelector.Select(executionContext);
            if (string.IsNullOrEmpty(selectedMessage)) return;
            selectedContext.PostMessage(selectedMessage);
        }
    }
}
