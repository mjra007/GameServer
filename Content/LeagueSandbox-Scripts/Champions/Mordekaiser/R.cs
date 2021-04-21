using GameServerCore.Enums;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using LeagueSandbox.GameServer.API;
using System.Collections.Generic;
using GameServerCore.Domain.GameObjects.Spell.Missile;


namespace Spells
{
    public class MordekaiserChildrenOfTheGrave : ISpellScript
    {
        IMinion minion;
        IObjAiBase Owner;
        IBuff Buff;
        ISpell Spell;
        IAttackableUnit Target;
        public ISpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true
            // TODO
        };

        public void OnActivate(IObjAiBase owner, ISpell spell)
        {
        }

        public void OnDeactivate(IObjAiBase owner, ISpell spell)
        {
        }

        public void OnSpellPreCast(IObjAiBase owner, ISpell spell, IAttackableUnit target, Vector2 start, Vector2 end)
        {
            Owner = owner;
            Target = target;
        }

        public void OnSpellCast(ISpell spell)
        {
            Buff = AddBuff("MordekaiserChildrenOfTheGrave", 10.0f, 1, spell, Target, Owner);
        }

        public void OnSpellPostCast(ISpell spell)
        {
        }

        public void OnSpellChannel(ISpell spell)
        {
        }

        public void OnSpellChannelCancel(ISpell spell)
        {
        }

        public void OnSpellPostChannel(ISpell spell)
        {
        }
        public void TargetExecute(IAttackableUnit unit, bool isCrit)
        {
           /* if (Owner.HasBuff("MordekaiserMaceOfSpades"))
            {
                var ADratio = Owner.Stats.AttackDamage.FlatBonus;
                var APratio = Owner.Stats.AbilityPower.Total * 0.4f;
                var damage = 80f + (30 * (Spell.CastInfo.SpellLevel - 1)) + ADratio + APratio;

                var units = GetUnitsInRange(Owner.Position, 600f, true);
                for (int i = 0; i < units.Count; i++)
                {
                    if (units[i].Team != Owner.Team && !(units[i] is IObjBuilding || units[i] is IBaseTurret) && units[i] is IObjAiBase ai)
                    {
                        if ((units.Count - 1) == 1)
                        {
                            units[i].TakeDamage(Owner, damage * 1.65f, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_ATTACK, true);
                        }
                        else
                        {
                            units[i].TakeDamage(Owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
                        }
                    }
                }
                RemoveBuff(Buff);


                //Ghould debuff (Sets All the stats and decaying health)

                                //MovSpeed Buffs:

            }*/
        }
        public void OnUpdate(float diff)
        {

        }


    }
}