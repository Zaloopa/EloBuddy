using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace ZLP_Ryze
{
    public class Combos
    {
        public static void Combo()
        {
            if (More.Target == null || More.Unkillable(More.Target)) return;

            if (Menus.Combo["Qc"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady() &&
                More.Target.IsValidTarget(Spells.Q.Range))
            {
                var prediction = Spells.Q.GetPrediction(More.Target);
                if (prediction.HitChance >= More.Hit())
                    Spells.Q.Cast(prediction.CastPosition);
            }

            if (Menus.Combo["Wc"].Cast<CheckBox>().CurrentValue && Spells.W.IsReady() &&
                More.Target.IsValidTarget(Spells.W.Range) &&
                (!Menus.Combo["Qc"].Cast<CheckBox>().CurrentValue || !Spells.Q.IsReady()) &&
                (!Menus.Combo["Ec"].Cast<CheckBox>().CurrentValue || !Spells.E.IsReady()))
                Spells.W.Cast(More.Target);

            if (Menus.Combo["Ec"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady() &&
                More.Target.IsValidTarget(Spells.E.Range) &&
                (!Menus.Combo["Qc"].Cast<CheckBox>().CurrentValue || !Spells.Q.IsReady()))
                Spells.E.Cast(More.Target);
        }

        public static void Harass()
        {
            if (More.Target == null || More.Unkillable(More.Target)) return;

            if (Menus.Combo["Qh"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady() &&
                More.Target.IsValidTarget(Spells.Q.Range))
            {
                var prediction = Spells.Q.GetPrediction(More.Target);
                if (prediction.HitChance >= More.Hit())
                    Spells.Q.Cast(prediction.CastPosition);
            }

            if (Menus.Combo["Eh"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady() &&
                More.Target.IsValidTarget(Spells.E.Range) &&
                (!Menus.Combo["Qh"].Cast<CheckBox>().CurrentValue || !Spells.Q.IsReady()))
                Spells.E.Cast(More.Target);
        }

        public static void Flee()
        {
            if (More.Target == null) return;

            if (Spells.Q.IsReady() && More.Target.IsValidTarget(Spells.Q.Range) &&
                (Player.Instance.HasBuff("RyzeQIconNoCharge") || Player.Instance.HasBuff("RyzeQIconFullCharge")))
            {
                var prediction = Spells.Q.GetPrediction(More.Target);
                if (prediction.HitChance >= More.Hit())
                    Spells.Q.Cast(prediction.CastPosition);
                if (More.CollisionT && Player.Instance.HasBuff("RyzeQIconFullCharge"))
                    Spells.Q.Cast(Player.Instance.Position.Extend(More.Target, Spells.Q.Range).To3DWorld());
            }

            if (Spells.W.IsReady() && More.Target.IsValidTarget(Spells.W.Range) && More.Target.HasBuff("RyzeE"))
                Spells.W.Cast(More.Target);

            if (Spells.E.IsReady() && More.Target.IsValidTarget(Spells.E.Range) &&
                (!Spells.Q.IsReady() || More.CollisionT || More.Target.HasBuff("RyzeE")))
                Spells.E.Cast(More.Target);
        }

        public static void Collision()
        {
            if (Menus.Combo["Qc"].Cast<CheckBox>().CurrentValue && Spells.Q.IsReady() &&
                More.HitQ != null && More.CountE > 1)
            {
                var prediction = Spells.Q.GetPrediction(More.HitQ);
                if (prediction.HitChance >= More.Hit())
                    Spells.Q.Cast(prediction.CastPosition);
            }

            if (Menus.Combo["Ec"].Cast<CheckBox>().CurrentValue && Spells.E.IsReady())
            {
                if (More.DieE != null)
                {
                    var target = TargetSelector.GetTarget(300, DamageType.Magical, More.DieE.Position, true);
                    if (target != null)
                        Spells.E.Cast(More.DieE);
                }

                if (More.HasE != null)
                {
                    var target = TargetSelector.GetTarget(300, DamageType.Magical, More.HasE.Position, true);
                    if (target != null)
                        Spells.E.Cast(More.HasE);
                }

                if (More.HitE != null && More.HasE == null && More.DieE == null)
                {
                    var target = TargetSelector.GetTarget(300, DamageType.Magical, More.HitE.Position, true);
                    if (target != null)
                        Spells.E.Cast(More.HitE);
                }
            }
        }

        public static bool Casted;

        public static void Escape()
        {
            if (Spells.R.IsReady() || Casted)
            {
                Casted = Spells.Zhonya.IsOwned() && Spells.Zhonya.IsReady();
                var turret = EntityManager.Turrets.Allies.Where(x => !x.IsDead)
                             .OrderBy(x => x.Distance(Player.Instance.Position)).FirstOrDefault();

                if (turret == null || !Casted) return;

                if (Spells.R.IsInRange(turret))
                {
                    Spells.R.Cast(turret.Position);

                    if (!Spells.R.IsReady() && More.CanCast())
                    {
                        Spells.Zhonya.Cast();
                        Casted = false;
                    }
                }

                else
                {
                    Spells.R.Cast(Player.Instance.Position.Extend(turret, Spells.R.Range).To3DWorld());

                    if (!Spells.R.IsReady() && More.CanCast())
                    {
                        Spells.Zhonya.Cast();
                        Casted = false;
                    }
                }
            }
        }
    }
}