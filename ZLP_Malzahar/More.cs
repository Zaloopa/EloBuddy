using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;

namespace ZLP_Malzahar
{
    public class More
    {
        public static void StopOrb(EventArgs args)
        {
            Orbwalker.DisableAttacking = EntityManager.Heroes.AllHeroes.Count(h => h.HasBuff("AlZaharNetherGrasp")) > 0;
            Orbwalker.DisableMovement = EntityManager.Heroes.AllHeroes.Count(h => h.HasBuff("AlZaharNetherGrasp")) > 0;
        }

        public static void CastW(Obj_AI_Base target)
        {
            if (target.IsInRange(Player.Instance, Spells.W.Range))
                Spells.W.Cast(target.Position);
            else
                Spells.W.Cast(Player.Instance.Position.Extend(target, Spells.W.Range).To3DWorld());
        }

        public static bool Unkillable(AIHeroClient target)
        {
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "UndyingRage"))
                return true;
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "ChronoShift"))
                return true;
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "JudicatorIntervention"))
                return true;
            if (target.Buffs.Any(b => b.IsValid() && b.DisplayName == "kindredrnodeathbuff"))
                return true;
            if (target.HasBuffOfType(BuffType.Invulnerability))
                return true;

            return target.IsInvulnerable;
        }

        public static float LastCast;

        public static bool CanCast()
        {
            var delay = (float) new Random().Next(Menus.MinDelay.CurrentValue, Menus.MaxDelay.CurrentValue);
            return !Menus.Main["human"].Cast<CheckBox>().CurrentValue ||
                   LastCast * 1000f + delay <= Game.Time * 1000f;
        }

        public static float CastedE;

        public static void OnCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe || args.Slot == SpellSlot.Recall) return;

            LastCast = Game.Time;

            if (args.Slot == SpellSlot.R)
            {
                Orbwalker.DisableAttacking = true;
                Orbwalker.DisableMovement = true;
            }

            if (args.Slot == SpellSlot.E)
            {
                CastedE = Game.Time * 1000f + 3000f;
            }

        }

        public static HitChance Hit()
        {
            switch (Menus.Main["hit"].Cast<ComboBox>().CurrentValue)
            {
                case 0:
                    return HitChance.Low;
                case 1:
                    return HitChance.Medium;
                case 2:
                    return HitChance.High;
                default:
                    return HitChance.Unknown;
            }
        }
    }
}