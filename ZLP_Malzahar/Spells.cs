using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace ZLP_Malzahar
{
    public class Spells
    {
        public static Spell.Skillshot Q, W;
        public static Spell.Targeted E, R, Ignite;
        public static Item Zhonya, Rylai;

        public static void Initialize()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 1000, int.MaxValue, 85, DamageType.Magical)
            { AllowedCollisionCount = -1 };
            W = new Spell.Skillshot(SpellSlot.W, 450, SkillShotType.Circular, 250, int.MaxValue, 250)
            { AllowedCollisionCount = -1 };
            E = new Spell.Targeted(SpellSlot.E, 650, DamageType.Magical);
            R = new Spell.Targeted(SpellSlot.R, 700, DamageType.Magical);

            var slot = Player.Instance.GetSpellSlotFromName("summonerdot");
            if (slot != SpellSlot.Unknown)
                Ignite = new Spell.Targeted(slot, 600);

            Zhonya = new Item(ItemId.Zhonyas_Hourglass);
            Rylai = new Item(ItemId.Rylais_Crystal_Scepter);
        }
    }
}