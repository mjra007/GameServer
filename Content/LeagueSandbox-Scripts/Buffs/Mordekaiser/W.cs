﻿using System.Numerics;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;

namespace MordekaiserCreepingDeath
{
    class MordekaiserCreepingDeath : IBuffGameScript
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
        float damage;

        public void OnActivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {
            Target = unit;
            Owner = ownerSpell.CastInfo.Owner;
            Spell = ownerSpell;
            var APratio = Owner.Stats.AbilityPower.Total * 0.2f;
            damage = 24f + (14f * (ownerSpell.CastInfo.SpellLevel - 1)) + APratio;
            AddParticleTarget(Owner, "mordekaiser_creepingDeath_aura.troy", unit, 2, lifetime: buff.Duration);
            StatsModifier.Armor.FlatBonus += 10 + (5 * (ownerSpell.CastInfo.SpellLevel - 1));
            StatsModifier.MagicResist.FlatBonus += 10 + (5 * (ownerSpell.CastInfo.SpellLevel - 1));
            unit.AddStatModifier(StatsModifier);

            var units = GetUnitsInRange(Target.Position, 350f, true);
            for (int i = units.Count - 1; i >= 0; i--)
            {
                if (units[i].Team != Spell.CastInfo.Owner.Team && !(units[i] is IObjBuilding || units[i] is IBaseTurret) && units[i] is IObjAiBase ai)
                {
                    units[i].TakeDamage(Owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, false);
                    units.RemoveAt(i);
                }
            }
        }

        public void OnDeactivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {


        }

        public void OnPreAttack(ISpell spell)
        {

        }

        public void OnUpdate(float diff)
        {
            ticks++;
            if (ticks == 60)
            {
                var units = GetUnitsInRange(Target.Position, 350f, true);
                for (int i = units.Count - 1; i >= 0; i--)
                {
                    if (units[i].Team != Spell.CastInfo.Owner.Team && !(units[i] is IObjBuilding || units[i] is IBaseTurret) && units[i] is IObjAiBase ai)
                    {
                        units[i].TakeDamage(Owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, false);
                        units.RemoveAt(i);

                    }
                }

                ticks = 0;
            }
        }
    }
}
