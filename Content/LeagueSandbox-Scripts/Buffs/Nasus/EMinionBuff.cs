using System.Numerics;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;

namespace NasusE
{
    class NasusE : IBuffGameScript
    {
        public BuffType BuffType => BuffType.COMBAT_ENCHANCER;
        public BuffAddType BuffAddType => BuffAddType.REPLACE_EXISTING;
        public int MaxStacks => 1;
        public bool IsHidden => false;

        public IStatsModifier StatsModifier { get; private set; } = new StatsModifier();

        IObjAiBase Owner;
        IBuff Buff;
        ISpell Spell;
        IAttackableUnit Target;
        int ticks;
        int ticks2;
        float damage1;
        float damage2;

        public void OnActivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {
            Target = unit;
            Owner = ownerSpell.CastInfo.Owner;
            Spell = ownerSpell;
            var APratio1 = Owner.Stats.AbilityPower.Total * 0.6f;
            var APratio2 = Owner.Stats.AbilityPower.Total * 0.12f;
            damage1 = 55f + (40f * (ownerSpell.CastInfo.SpellLevel - 1)) + APratio1;
            damage2 = 11f + (8f * (ownerSpell.CastInfo.SpellLevel - 1)) + APratio2;


            
            var units = GetUnitsInRange(Target.Position, 350f, true);
            for (int i = units.Count - 1; i >= 0; i--)
            {
                if (units[i].Team != Spell.CastInfo.Owner.Team && !(units[i] is IObjBuilding || units[i] is IBaseTurret) && units[i] is IObjAiBase ai)
                {
                    units[i].TakeDamage(Owner, damage1, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, false);
                    units.RemoveAt(i);
                }
            }
            ticks = 0;
            ticks2 = 0;
        }

        public void OnDeactivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {
            Target.Die(Target);

        }

        public void OnPreAttack(ISpell spell)
        {

        }

        public void OnUpdate(float diff)
        {
            ticks++;
            if (ticks == 50)
            {
                var units = GetUnitsInRange(Target.Position, 400f, true);
                for (int i = units.Count - 1; i >= 0; i--)
                {
                    if (units[i].Team != Spell.CastInfo.Owner.Team && !(units[i] is IObjBuilding || units[i] is IBaseTurret) && units[i] is IObjAiBase ai)
                    {
                        units[i].TakeDamage(Owner, damage2, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, false);
                        units.RemoveAt(i);

                    }
                }

                ticks = 0;
            }
            /*if (Target != null || !Target.IsDead)
            {
                if (ticks2 == 300)
                {
                    
                }
            }*/
        }
    }
}
