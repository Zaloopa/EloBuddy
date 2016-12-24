using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace ZLP_Malzahar
{
    public class Drawings
    {
        public static void OnDraw(EventArgs args)
        {
            if (!Menus.Draw["draw"].Cast<CheckBox>().CurrentValue) return;

            if (Menus.Draw["Q"].Cast<CheckBox>().CurrentValue && Spells.Q.IsLearned)
                Circle.Draw(Color.LightBlue, Spells.Q.Range, Player.Instance.Position);
            if (Menus.Draw["W"].Cast<CheckBox>().CurrentValue && Spells.W.IsLearned)
                Circle.Draw(Color.BlueViolet, Spells.W.Range, Player.Instance.Position);
            if (Menus.Draw["E"].Cast<CheckBox>().CurrentValue && Spells.E.IsLearned)
                Circle.Draw(Color.DeepSkyBlue, Spells.E.Range, Player.Instance.Position);
            if (Menus.Draw["R"].Cast<CheckBox>().CurrentValue && Spells.R.IsReady())
                Circle.Draw(Color.LightSkyBlue, Spells.R.Range, Player.Instance.Position);
            if (Menus.Draw["ignite"].Cast<CheckBox>().CurrentValue && Spells.Ignite != null && Spells.Ignite.IsReady())
                Circle.Draw(Color.OrangeRed, Spells.Ignite.Range, Player.Instance.Position);

            if (Menus.Draw["damage"].Cast<CheckBox>().CurrentValue)
            {
                Calculations.Execute();

                foreach (var enemy in EntityManager.Heroes.Enemies.Where(e => e.IsHPBarRendered))
                {
                    var hp = (enemy.TotalShieldHealth() - Calculations.Damage > 0f ?
                              enemy.TotalShieldHealth() - Calculations.Damage : 0f) /
                             (enemy.MaxHealth + enemy.AllShield + enemy.AttackShield + enemy.MagicShield);
                    var start = new Vector2(enemy.HPBarPosition.X + hp * 107, enemy.HPBarPosition.Y - 10f);
                    var end = new Vector2(enemy.HPBarPosition.X + hp * 107, enemy.HPBarPosition.Y);
                    var color = enemy.TotalShieldHealth() - Calculations.Damage > 0f ?
                                System.Drawing.Color.Lime : System.Drawing.Color.Red;

                    Drawing.DrawLine(start, end, 1, color);
                }
            }
        }
    }
}