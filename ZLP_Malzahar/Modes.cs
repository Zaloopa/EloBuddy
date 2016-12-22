using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace ZLP_Malzahar
{
    public class Modes
    {
        public static void Combo()
        {
            if (!More.CanCast()) return;

            if (Menus.Combo["Qc"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady() &&
                (!Menus.Combo["Wc"].Cast<CheckBox>().CurrentValue || !Spells.W.IsReady()) &&
                (!Menus.Combo["Ec"].Cast<CheckBox>().CurrentValue || !Spells.E.IsReady()))
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (target == null || More.Unkillable(target)) return;
                var prediction = Spells.Q.GetPrediction(target);
                if (target.IsValidTarget(Spells.Q.Range) && prediction.HitChance >= More.Hit())
                    Spells.Q.Cast(prediction.CastPosition);
            }

            if (Menus.Combo["Wc"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady() &&
                (!Menus.Combo["Ec"].Cast<CheckBox>().CurrentValue || !Spells.E.IsReady()))
            {
                var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Mixed);
                if (target == null || More.Unkillable(target)) return;
                var prediction = Spells.W.GetPrediction(target);
                if (target.IsValidTarget(Spells.W.Range) && prediction.HitChance >= More.Hit())
                    Spells.W.Cast(prediction.CastPosition);
            }

            if (Menus.Combo["Ec"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                if (target == null || More.Unkillable(target)) return;
                if (target.IsValidTarget(Spells.E.Range))
                    Spells.E.Cast(target);
            }

            if (Menus.Combo["Rc"].Cast<CheckBox>().CurrentValue && Spells.R.IsReady() &&
                (!Menus.Combo["Qc"].Cast<CheckBox>().CurrentValue || !Spells.Q.IsReady()) &&
                (!Menus.Combo["Wc"].Cast<CheckBox>().CurrentValue || !Spells.W.IsReady()) &&
                (!Menus.Combo["Ec"].Cast<CheckBox>().CurrentValue || !Spells.E.IsReady()))
            {
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                if (target == null || More.Unkillable(target)) return;
                if (target.IsValidTarget(Spells.R.Range))
                    Spells.R.Cast(target);
            }
        }

        public static void Harass()
        {
            if (Player.Instance.ManaPercent <= Menus.HarassMana.CurrentValue || !More.CanCast()) return;

            if (Menus.Combo["Qc"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady() &&
                Game.Time >= More.CastedE + 3000)
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (target == null || More.Unkillable(target)) return;
                var prediction = Spells.Q.GetPrediction(target);
                if (target.IsValidTarget(Spells.Q.Range) && prediction.HitChance >= More.Hit())
                    Spells.Q.Cast(prediction.CastPosition);
            }

            if (Menus.Combo["Wc"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady() &&
                (!Menus.Combo["Ec"].Cast<CheckBox>().CurrentValue || !Spells.E.IsReady()))
            {
                var target = TargetSelector.GetTarget(Spells.W.Range, DamageType.Mixed);
                if (target == null || More.Unkillable(target)) return;
                var prediction = Spells.W.GetPrediction(target);
                if (target.IsValidTarget(Spells.W.Range) && prediction.HitChance >= More.Hit())
                    Spells.W.Cast(prediction.CastPosition);
            }

            if (Menus.Combo["Ec"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                if (target == null || More.Unkillable(target)) return;
                if (target.IsValidTarget(Spells.E.Range))
                    Spells.E.Cast(target);
            }
        }

        public static void LaneClear()
        {
            if (Player.Instance.ManaPercent <= Menus.LaneMana.CurrentValue || !More.CanCast()) return;

            if (Menus.Clear["Qlc"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady() &&
                (!Menus.Clear["Wlc"].Cast<CheckBox>().CurrentValue || !Spells.W.IsReady()) &&
                (!Menus.Clear["Elc"].Cast<CheckBox>().CurrentValue || !Spells.E.IsReady()))
            {
                var minion = EntityManager.MinionsAndMonsters.EnemyMinions
                             .Where(m => m.IsValidTarget(Spells.Q.Range))
                             .OrderBy(m => m.Health).FirstOrDefault();
                if (minion == null) return;
                Spells.Q.Cast(minion);
            }

            if (Menus.Clear["Wlc"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady() &&
                (!Menus.Clear["Elc"].Cast<CheckBox>().CurrentValue || !Spells.E.IsReady()))
            {
                var minion = EntityManager.MinionsAndMonsters.EnemyMinions
                             .Where(m => m.IsValidTarget(Spells.W.Range) && m.HasBuff("malzahare"))
                             .OrderBy(m => m.Health).FirstOrDefault();
                if (minion == null) return;
                Spells.W.Cast(minion);
            }

            if (Menus.Clear["Elc"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady())
            {
                var minion = EntityManager.MinionsAndMonsters.EnemyMinions
                             .Where(m => m.IsValidTarget(Spells.E.Range) && !m.HasBuff("malzahare"))
                             .OrderBy(m => m.Health).FirstOrDefault();
                if (minion == null) return;
                Spells.E.Cast(minion);
            }
        }

        public static void JungleClear()
        {
            if (Player.Instance.ManaPercent <= Menus.JungleMana.CurrentValue || !More.CanCast()) return;

            if (Menus.Clear["Qlc"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady() &&
                Game.Time >= More.CastedE + 3000)
            {
                var minion = EntityManager.MinionsAndMonsters.EnemyMinions
                             .Where(m => m.IsValidTarget(Spells.Q.Range) && m.HasBuff("malzahare"))
                             .OrderBy(m => m.Health).FirstOrDefault();
                if (minion == null) return;
                Spells.Q.Cast(minion);
            }

            if (Menus.Clear["Wlc"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady() &&
                (!Menus.Clear["Elc"].Cast<CheckBox>().CurrentValue || !Spells.E.IsReady()))
            {
                var monster = EntityManager.MinionsAndMonsters.Monsters
                             .Where(m => m.IsValidTarget(Spells.W.Range) && m.HasBuff("malzahare"))
                             .OrderBy(m => m.MaxHealth).LastOrDefault();
                if (monster == null) return;
                Spells.W.Cast(monster);
            }

            if (Menus.Clear["Elc"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady())
            {
                var monster = EntityManager.MinionsAndMonsters.Monsters
                             .Where(m => m.IsValidTarget(Spells.E.Range) && !m.HasBuff("malzahare"))
                             .OrderBy(m => m.MaxHealth).LastOrDefault();
                if (monster == null) return;
                Spells.E.Cast(monster);
            }
        }

        public static void LastHit(Obj_AI_Base unit, Orbwalker.UnkillableMinionArgs args)
        {
            if (Player.Instance.ManaPercent <= Menus.LastMana.CurrentValue || unit == null) return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                if (Menus.Clear["Qlh"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady() &&
                    unit.Health <= Spells.Q.GetSpellDamage(unit))
                    Spells.Q.Cast(unit);

                if (Menus.Clear["Elh"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady() &&
                    unit.Health <= Spells.E.GetSpellDamage(unit) / 8)
                    Spells.E.Cast(unit);
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                if (Menus.Clear["Qlc"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady() &&
                    unit.Health <= Spells.Q.GetSpellDamage(unit))
                    Spells.Q.Cast(unit);

                if (Menus.Clear["Elc"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady() &&
                    unit.Health <= Spells.E.GetSpellDamage(unit) / 8)
                    Spells.E.Cast(unit);
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                if (Menus.Clear["Qjc"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady() &&
                    unit.Health <= Spells.Q.GetSpellDamage(unit))
                    Spells.Q.Cast(unit);

                if (Menus.Clear["Ejc"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady() &&
                    unit.Health <= Spells.E.GetSpellDamage(unit) / 8)
                    Spells.E.Cast(unit);
            }
        }

        public static void Flee()
        {
            if (!More.CanCast()) return;

            if (Menus.Combo["Qc"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (target == null || More.Unkillable(target)) return;
                var prediction = Spells.Q.GetPrediction(target);
                if (target.IsValidTarget(Spells.Q.Range) && prediction.HitChance >= More.Hit())
                    Spells.Q.Cast(prediction.CastPosition);
            }
        }

        public static void KillSteal(EventArgs args)
        {
            if (Menus.Misc["Q"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (target == null || More.Unkillable(target)) return;
                var prediction = Spells.Q.GetPrediction(target);
                if (target.IsValidTarget(Spells.Q.Range) && prediction.HitChance >= More.Hit() &&
                    target.TotalShieldHealth() <= Spells.Q.GetSpellDamage(target))
                    Spells.Q.Cast(target);
            }

            if (Menus.Misc["E"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
                if (target == null || More.Unkillable(target)) return;
                if (target.IsValidTarget(Spells.E.Range) &&
                    target.TotalShieldHealth() <= Spells.E.GetSpellDamage(target))
                    Spells.E.Cast(target);
            }

            if (Menus.Misc["R"].Cast<CheckBox>().CurrentValue && Spells.R.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.R.Range, DamageType.Magical);
                if (target == null || More.Unkillable(target)) return;
                if (target.IsValidTarget(Spells.R.Range) &&
                    target.TotalShieldHealth() <= Spells.R.GetSpellDamage(target))
                    Spells.R.Cast(target);
            }

            if (Menus.Misc["ignite"].Cast<CheckBox>().CurrentValue &&
                Spells.Ignite != null && Spells.Ignite.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Ignite.Range, DamageType.True);
                if (target == null || More.Unkillable(target)) return;
                if (target.IsValidTarget(Spells.Ignite.Range) && target.TotalShieldHealth() <=
                    Player.Instance.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite))
                    Spells.Ignite.Cast(target);
            }
        }

        public static void OnInt(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (Menus.Misc["int"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady() && More.CanCast() &&
                sender != null && sender.IsEnemy && sender.IsValidTarget(Spells.Q.Range))
            {
                var prediction = Spells.Q.GetPrediction(sender);
                if (prediction.HitChance >= More.Hit())
                    Spells.Q.Cast(prediction.CastPosition);
            }
        }
    }
}