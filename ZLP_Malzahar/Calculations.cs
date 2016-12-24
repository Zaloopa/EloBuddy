using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace ZLP_Malzahar
{
    public class Calculations
    {
        public static float Damage, Edmg, Rdmg;

        public static void Execute()
        {
            var ap = (int) Player.Instance.TotalMagicalDamage / 100;

            foreach (var enemy in EntityManager.Heroes.Enemies.Where(e => e.IsHPBarRendered))
            {
                var q = Spells.Q.IsReady() ? Player.Instance.CalculateDamageOnUnit(enemy, DamageType.Magical,
                        new[] { 0f, 70f, 110f, 150f, 190f, 230f }[Spells.Q.Level]
                        + 0.7f * Player.Instance.TotalMagicalDamage) : 0f;
                var e = Spells.E.IsReady() ? Player.Instance.CalculateDamageOnUnit(enemy, DamageType.Magical,
                        (new[] { 0f, 10f, 14.375f, 18.75f, 23.125f, 27.5f }[Spells.E.Level]
                        + 0.0875f * Player.Instance.TotalMagicalDamage) * 8f) : 0f;
                var r = Spells.R.IsReady() ? Player.Instance.CalculateDamageOnUnit(enemy, DamageType.Magical,
                        new[] { 0f, 0.05f + 0.015f * ap, 0.07f + 0.015f * ap, 0.09f + 0.015f * ap }[Spells.R.Level]
                        * enemy.MaxHealth) * 3f : 0f;

                Damage = q + e + r;
                Edmg = e;
                Rdmg = r;

                if (Spells.Ignite != null && Spells.Ignite.IsReady())
                {
                    var ignite = Player.Instance.GetSummonerSpellDamage(enemy, DamageLibrary.SummonerSpells.Ignite);
                    Damage += ignite;
                }
            }
        }
    }
}