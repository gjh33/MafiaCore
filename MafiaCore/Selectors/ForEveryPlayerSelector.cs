// While I don't like having this circular dependency, we're
// not doing separate assemblies anyways, and it was necessary
// for compatibility reasons.
// The reasons are that some platforms (like unity) cannot serialize
// interfaces, and the alternative was to have ISelector which ForEveryPlayerEffect
// could also implement, thus avoiding the circular dependency. Oh well.

using System;
using MafiaCore.Effects;

namespace MafiaCore.Selectors
{
    [Serializable]
    public class ForEveryPlayerSelector : Selector<Player>
    {
        public ForEveryPlayerEffect ForEveryPlayerEffect;

        public override Player Select(ExecutionParams executionContext) => ForEveryPlayerEffect.Select(executionContext);
    }
}
