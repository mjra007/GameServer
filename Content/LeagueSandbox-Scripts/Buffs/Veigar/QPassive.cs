using GameServerCore.Enums;
using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;
using GameServerCore.Domain.GameObjects.Spell;

namespace VeigarBalefulStrike
{
    internal class VeigarBalefulStrike : IBuffGameScript
    {
        public BuffType BuffType => BuffType.COUNTER;
        public BuffAddType BuffAddType => BuffAddType.STACKS_AND_OVERLAPS;
        public int MaxStacks => 2147483647;
        public bool IsHidden => true;

        public IStatsModifier StatsModifier { get; private set; } = new StatsModifier();

        public void OnUpdate(float diff)
        {

        }

        public void OnActivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {
            var buffer = StatsModifier.AbilityPower.FlatBonus - (buff.StackCount - 1);


            StatsModifier.AbilityPower.FlatBonus = buff.StackCount + buffer;
            unit.AddStatModifier(StatsModifier);

        }

        public void OnDeactivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {
        }
    }
}
