using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using LeagueSandbox.GameServer.Scripting.CSharp;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;


namespace MordekaiserChildrenOfTheGrave
{
    internal class MordekaiserChildrenOfTheGrave : IBuffGameScript
    {
        IAttackableUnit Unit;
        float ticks;
        int ticks2;
        float damage2;
        IObjAiBase Owner;
        IParticle p;
        int limiter;

        public BuffType BuffType => BuffType.SLOW;
        public BuffAddType BuffAddType => BuffAddType.STACKS_AND_RENEWS;
        public int MaxStacks => 1;
        public bool IsHidden => false;

        public IStatsModifier StatsModifier { get; private set; } = new StatsModifier();

        public void OnActivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {
            Unit = unit;
            var owner = ownerSpell.CastInfo.Owner;

            var damage = unit.Stats.HealthPoints.Total * (0.12f + (0.025f * (ownerSpell.CastInfo.SpellLevel - 1)) + (owner.Stats.AbilityPower.Total * 0.0002f));

            unit.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            owner.Stats.CurrentHealth += damage;

            p = AddParticleTarget(owner, "mordekeiser_cotg_tar.troy", unit, lifetime: buff.Duration);


            Owner = owner;
            damage2 = unit.Stats.HealthPoints.Total * (0.012f + (0.0025f * (ownerSpell.CastInfo.SpellLevel - 1)) + (owner.Stats.AbilityPower.Total * 0.00002f));
            limiter = 0;

        }

        public void OnDeactivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {
            RemoveParticle(p);
        }

        public void OnUpdate(float diff)
        {
            ticks++;

            if (ticks == 60f)
            {
                Unit.TakeDamage(Unit, damage2, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, false);
                Owner.Stats.CurrentHealth = Owner.Stats.CurrentHealth + damage2;
                ticks = 0;
            }
            if (Unit != null)
            {
                if (Unit.IsDead && limiter == 0)
                {
                    var ghost = AddMinion(Owner, Unit.Model, Unit.Model, Unit.Position);
                    AddParticleTarget(Owner, "mordekeiser_cotg_skin.troy", ghost, lifetime: 30f);
                    limiter++;

                    CreateTimer(30f, () =>
                    {
                        if (ghost != null && !ghost.IsDead)
                        {
                            ghost.Die(ghost);
                        }
                    });



                }
            }
        }
    }
}
//TODO: Make healing for POST MITIGATION damage
