using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Effects
{
    public class SetTargetEffect : Effect
    {
        public ISelector<Context> ContextSelector;
        public ISelector<string> TargetSelector;
        public ISelector<Player> PlayerSelector;

        public override void Apply(ExecutionParams executionContext)
        {
            Context selectedContext = ContextSelector.Select(executionContext);
            if (selectedContext == null) return;
            string selectedTarget = TargetSelector.Select(executionContext);
            if (string.IsNullOrEmpty(selectedTarget)) return;
            Player selectedPlayer = PlayerSelector.Select(executionContext);
            if (selectedPlayer == null) return;
            selectedContext.SetTarget(selectedTarget, selectedPlayer);
        }
    }
}
