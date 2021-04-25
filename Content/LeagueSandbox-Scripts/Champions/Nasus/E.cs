using GameServerCore.Enums;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using GameServerCore.Domain.GameObjects.Spell.Missile;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using LeagueSandbox.GameServer.API;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.Stats;
using GameServerCore;
using GameServerCore.Domain;


namespace Spells
{
    public class NasusE : ISpellScript
    {
        IObjAiBase Owner;
        IMinion minion;
        ISpell Spell;
        float CastRange;
        Vector2 current;
        Vector2 mouse;
        float ticks;

        public ISpellScriptMetadata ScriptMetadata => new SpellScriptMetadata()
        {
            TriggersSpellCasts = true,
            //IsDamagingSpell = true,

        };

        public void OnActivate(IObjAiBase owner, ISpell spell)
        {
        }

        public void TargetExecute(ISpell spell, IAttackableUnit target, ISpellMissile missile)
        {
        }

        public void OnDeactivate(IObjAiBase owner, ISpell spell)
        {
        }

        public void OnSpellPreCast(IObjAiBase owner, ISpell spell, IAttackableUnit target, Vector2 start, Vector2 end)
        {
            Owner = owner;
            Spell = spell;
            CastRange = spell.SpellData.CastRange[0];
            current = owner.Position;
            mouse = new Vector2(spell.CastInfo.TargetPosition.X, spell.CastInfo.TargetPosition.Z);


        }

        public void OnSpellCast(ISpell spell)
        {
            if (Extensions.IsVectorWithinRange(current, mouse, CastRange))
            {
                AddParticle(Owner, "Nasus_Base_E_Warning.troy", mouse, 1, lifetime: 1f);
                AddParticleTarget(Owner, "nassus_spiritFire_cas.troy", Owner, 1, lifetime: 1f);

                AddParticleTarget(Owner, "Nasus_Base_E_Staff_Swirl.troy", Owner, 1, lifetime: 1f);

                CreateTimer(0.5f, () =>
                {
                    minion = AddMinion(Owner, "Red_Minion_Basic", "Red_Minion_Basic", mouse, isVisible: false);
                    minion.SetIsTargetable(false);
                    AddBuff("NasusE", 5f, 1, spell, minion, Owner);
                    AddParticle(Owner, "Nasus_Base_E_SpiritFire.troy", mouse, 1, lifetime: 5f);


                });

            }
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

        public void OnUpdate(float diff)
        {
            //ticks++;
            /*if (ticks == 300)
            {
                minion.Die(minion);
            }*/
        }
    }
}
