using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace ZLP_Malzahar
{
    public class Spells
    {
        public static Spell.Skillshot Q, W;
        public static Spell.Targeted E, R, Ignite;
        public static Item Zhonya;

        public static void Initialize()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 500, int.MaxValue, 400, DamageType.Magical);
            W = new Spell.Skillshot(SpellSlot.W, 450, SkillShotType.Circular, 250, int.MaxValue, 250);
            E = new Spell.Targeted(SpellSlot.E, 650, DamageType.Magical);
            R = new Spell.Targeted(SpellSlot.R, 700, DamageType.Magical);

            var slot = Player.Instance.GetSpellSlotFromName("summonerdot");
            if (slot != SpellSlot.Unknown)
                Ignite = new Spell.Targeted(slot, 600);

            Zhonya = new Item(ItemId.Zhonyas_Hourglass);
        }
    }
}