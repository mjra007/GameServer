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
    public class NasusQ : ISpellScript
    {
        IMinion minion;
        IObjAiBase Owner;
        IBuff Buff;
        IBuff Buff2;
        ISpell Spell;
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
            Buff = AddBuff("NasusQ", 10.0f, 1, spell, owner, owner);
            Buff2 = AddBuff("NasusQStacks", 5f, 1, spell, owner, owner, true);

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
            if (Owner.HasBuff("NasusQ"))
            {
                float stackCount = Buff2.StackCount;


                var damage = 30f + (20f * (Spell.CastInfo.SpellLevel - 1)) + stackCount;;
                unit.TakeDamage(Owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);

                if (unit.IsDead)
                {
                    if (unit is IChampion)
                    {
                        AddBuff("NasusQStacks", 1f, 6, Spell, Owner, Owner, true);
                        AddBuff("NasusQStacks", 1f, 6, Spell, Owner, Owner, true);
                        AddBuff("NasusQStacks", 1f, 6, Spell, Owner, Owner, true);
                        AddBuff("NasusQStacks", 1f, 6, Spell, Owner, Owner, true);
                        AddBuff("NasusQStacks", 1f, 6, Spell, Owner, Owner, true);
                        AddBuff("NasusQStacks", 1f, 6, Spell, Owner, Owner, true);
                    }
                    else
                    {
                        AddBuff("NasusQStacks", 1f, 3, Spell, Owner, Owner, true);
                        AddBuff("NasusQStacks", 1f, 3, Spell, Owner, Owner, true);
                        AddBuff("NasusQStacks", 1f, 3, Spell, Owner, Owner, true);
                    }
                }
            }
            RemoveBuff(Buff);

        }
        public void OnUpdate(float diff)
        {

        }


    }
}