using GameServerCore.Enums;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using GameServerCore.Domain.GameObjects.Spell.Missile;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using LeagueSandbox.GameServer.API;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;

namespace Spells
{
    public class VeigarEventHorizon : ISpellScript
    {
        Vector2 truecoords;
        int ticks;
        IObjAiBase Owner;
        ISpell SPELL;
        UnitCrowdControl stun;
        float duration;

        public ISpellScriptMetadata ScriptMetadata => new SpellScriptMetadata()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,

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

        }

        public void OnSpellCast(ISpell spell)
        {
            var owner = spell.CastInfo.Owner as IChampion;
            var ownerSkinID = owner.Skin;
            truecoords = new Vector2(spell.CastInfo.TargetPosition.X, spell.CastInfo.TargetPosition.Z);
            var distance = Vector2.Distance(spell.CastInfo.Owner.Position, truecoords);
            if (distance > 650f)
            {
                truecoords = GetPointFromUnit(spell.CastInfo.Owner, 650f);
            }

            switch (ownerSkinID)
            {
                case 8:
                    AddParticleTarget(spell.CastInfo.Owner, "Veigar_Skin08_E_cas.troy", owner, lifetime: 1f);
                    AddParticle(spell.CastInfo.Owner, "Veigar_Skin08_E_cage_green.troy", truecoords, lifetime: 3f);
                    break;
                case 6:
                    AddParticle(spell.CastInfo.Owner, "Veigar_Base_E_cas.troy", truecoords);
                    AddParticle(spell.CastInfo.Owner, "Veigar_Skin06_E_cage_green.troy", truecoords, lifetime: 3f);
                    break;
                case 4:
                    AddParticle(spell.CastInfo.Owner, "Veigar_Base_E_cas.troy", truecoords) ;
                    AddParticle(spell.CastInfo.Owner, "Veigar_Skin04_E_cage_green.troy", truecoords, lifetime: 3f);
                    break;
                default:
                    // AddParticleTarget(spell.CastInfo.Owner, "Veigar_Base_E_cas.troy", owner, lifetime: 1f);  No idea what particle FX this was supposed to be, but it just spamms a bunch of blue squares around the target
                    AddParticle(spell.CastInfo.Owner, "Veigar_Base_E_cage_green.troy", truecoords, lifetime: 3f);
                    break;

            }




            //duration = 1.25f + (0.25f * spell.CastInfo.SpellLevel);
            //stun = new UnitCrowdControl(CrowdControlType.STUN, duration);


            //TODO: Make stun work Properly
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

            //if (ticks <= 180)
            //{
            //    var units = GetUnitsInRange(truecoords, 350f, true);
            //    for (int i = 0; i < units.Count; i++)
            //    {
            //        if (Vector2.Distance(units[i].Position, truecoords) >= 350f && Vector2.Distance(units[i].Position, truecoords) <= 370f)
            //        {
            //            units[i].ApplyCrowdControl(stun, Owner);
            //            AddBuff("VeigarEventHorizon", duration, 1, SPELL, units[i], Owner);

            //        }
            //    }

        }
    }
}