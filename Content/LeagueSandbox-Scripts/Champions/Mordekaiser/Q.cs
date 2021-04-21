﻿using GameServerCore.Enums;
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
    public class MordekaiserMaceOfSpades : ISpellScript
    {
        IMinion minion;
        IObjAiBase Owner;
        IBuff Buff;
        ISpell Spell;
        int i;
        public ISpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            TriggersSpellCasts = false,
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
            Spell = spell;
            spell.CastInfo.Owner.CancelAutoAttack(true);
            ApiEventManager.OnHitUnit.AddListener(this, spell.CastInfo.Owner, TargetExecute, true);
            Buff = AddBuff("MordekaiserMaceOfSpades", 10.0f, 1, spell, owner, owner);

        }

        public void OnSpellCast(ISpell spell)
        {
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
            if (Owner.HasBuff("MordekaiserMaceOfSpades"))
            {
                var ADratio = Owner.Stats.AttackDamage.FlatBonus;
                var APratio = Owner.Stats.AbilityPower.Total * 0.4f;
                var damage = 80f + (30 * (Spell.CastInfo.SpellLevel - 1)) + ADratio + APratio;

                AddParticleTarget(Owner, "mordakaiser_siphonOfDestruction_self.troy", Owner, lifetime: 1f);
                var units = GetUnitsInRange(Owner.Position, 600f, true);
                for (i = units.Count - 1; i >= 0; i--)
                {
                    if (units[i].Team == Owner.Team || units[i] is IBaseTurret || units[i] is INexus || units[i] is IObjBuilding || units[i] is ILaneTurret)
                    {
                        units.RemoveAt(i);
                    }
                }
                for (i = 0; i < units.Count; i++)
                {
                    //if (units[i].Team != Owner.Team && !(units[i] is IObjBuilding || units[i] is IBaseTurret || units[i] is INexus || units[i] is ILaneTurret) && units[i] is IObjAiBase ai)
                    //{
                    if ((units.Count) == 1)
                    {
                        units[i].TakeDamage(Owner, damage * 1.65f, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_ATTACK, true);
                        AddParticleTarget(Owner, "mordakaiser_maceOfSpades_tar2.troy", units[i], lifetime: 1);

                    }
                    else
                    {
                        units[i].TakeDamage(Owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
                        AddParticleTarget(Owner, "mordakaiser_maceOfSpades_tar.troy", units[i], lifetime: 1);

                    }
                    // }
                    RemoveBuff(Buff);
                }


            }
        }
        public void OnUpdate(float diff)
        {

        }


    }
}