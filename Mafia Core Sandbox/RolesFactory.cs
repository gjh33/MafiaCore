using MafiaCore;
using MafiaCore.Effects;
using MafiaCore.Selectors;
using MafiaCore.Conditions;

namespace Mafia_Core_Sandbox
{
    class Factory
    {
        public Team Town;
        public Team Mafia;

        public Effect InitTown = new SequenceEffect();
        public Effect AddTownFlag;
        public Effect UpdateTownCountEffect;
        public Action UpdateTownCountAction = new Action();

        public Factory()
        {
            GenerateTownData();

            Town = new Team();
            SequenceEffect townSharedStartup = new SequenceEffect();
            townSharedStartup.SubEffects.Add(AddTownFlag);
            Town.SharedStartingEffect = townSharedStartup;

            UpdateTownCountAction.ExecutionEffect = UpdateTownCountEffect;
            Town.AlwaysExecuteActions.Add(UpdateTownCountAction);

            Mafia = new Team();
        }

        public Role CreateVT()
        {
        }

        private void GenerateTownData()
        {
            SetCounterEffect clearCount = new SetCounterEffect()
            {
                ContextSelector = new GameContextSelector(),
                CounterSelector = new ConstantSelector<string> { Value = "Town Count" },
                ValueSelector = new ConstantSelector<int> { Value = 0 },
            };

            ForEveryPlayerEffect iterateAndCount = new ForEveryPlayerEffect();
            iterateAndCount.ForEachEffect = new BranchEffect()
            {
                Condition = new HasFlagCondition()
                {
                    ContextSelector = new PlayerContextSelector()
                    {
                        PlayerSelector = iterateAndCount
                    },
                    FlagSelector = new ConstantSelector<string> { Value = "Town" }
                },
                TrueEffect = new IncrementCounterEffect()
                {
                    ContextSelector = new GameContextSelector(),
                    CounterSelector = new ConstantSelector<string> { Value = "Town Count" }
                }
            };

            SequenceEffect countingSequence = new SequenceEffect();
            countingSequence.SubEffects.Add(clearCount);
            countingSequence.SubEffects.Add(iterateAndCount);

            UpdateTownCountEffect = countingSequence;

            AddTownFlag = new AddFlagEffect()
            {
                ContextSelector = new PlayerContextSelector()
                {
                    PlayerSelector = new ExecutingPlayerSelector()
                },
                FlagSelector = new ConstantSelector<string> { Value = "Town" }
            };
        }
    }
}
