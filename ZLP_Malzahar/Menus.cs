using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace ZLP_Malzahar
{
    public class Menus
    {
        public static Menu Main, Combo, Clear, Misc, Draw;
        public static Slider MinDelay, MaxDelay, HarassMana, LaneMana, JungleMana, LastMana;

        public static void Initialize()
        {
            Main = MainMenu.AddMenu("ZLP Malzahar", "ZLP_Malzahar");
            Main.AddLabel("Made by Zaloopa");
            Main.AddSeparator();
            Main.AddGroupLabel("Humanizer");
            Main.Add("human", new CheckBox("Use Spell Humanizer"));
            MinDelay = Main.Add("min", new Slider("Min. Delay (ms)", 1, 0, 1000));
            MaxDelay = Main.Add("max", new Slider("Max. Delay (ms)", 249, 0, 1000));
            Main.AddGroupLabel("Q Hit Chance");
            Main.Add("hit", new ComboBox("Hit Chance", 1, "Low", "Medium", "High"));

            Combo = Main.AddSubMenu("Combo & Harass");
            {
                Combo.AddGroupLabel("Combo");
                Combo.Add("Qc", new CheckBox("Use Q"));
                Combo.Add("Wc", new CheckBox("Use W"));
                Combo.Add("Ec", new CheckBox("Use E"));
                Combo.Add("Rc", new CheckBox("Use R"));
                Combo.AddGroupLabel("Harass");
                Combo.Add("Qh", new CheckBox("Use Q"));
                Combo.Add("Wh", new CheckBox("Use W"));
                Combo.Add("Eh", new CheckBox("Use E"));
                HarassMana = Combo.Add("HarassMP", new Slider("Don't use spells if MP below {0}%", 40));
            }

            Clear = Main.AddSubMenu("Lane & Jungle Clear");
            {
                Clear.AddGroupLabel("Lane Clear");
                Clear.Add("Qlc", new CheckBox("Use Q"));
                Clear.Add("Wlc", new CheckBox("Use W"));
                Clear.Add("Elc", new CheckBox("Use E"));
                LaneMana = Clear.Add("lcmp", new Slider("Don't use spells if MP below {0}%", 40));
                Clear.AddGroupLabel("Jungle Clear");
                Clear.Add("Qjc", new CheckBox("Use Q"));
                Clear.Add("Wjc", new CheckBox("Use W"));
                Clear.Add("Ejc", new CheckBox("Use E"));
                JungleMana = Clear.Add("jcmp", new Slider("Don't use spells if MP below {0}%", 40));
                Clear.AddGroupLabel("Last Hit");
                Clear.Add("Qlh", new CheckBox("Use Q"));
                Clear.Add("Elh", new CheckBox("Use E"));
                LastMana = Clear.Add("lhmp", new Slider("Don't use spells if MP below {0}%", 25));
            }

            Misc = Main.AddSubMenu("Misc Settings");
            {
                Misc.AddGroupLabel("Kill Steal");
                Misc.Add("Q", new CheckBox("Use Q"));
                Misc.Add("E", new CheckBox("Use E"));
                Misc.Add("R", new CheckBox("Use R", false));
                Misc.Add("ignite", new CheckBox("Use Ignite"));
                Misc.AddGroupLabel("Other Options");
                Misc.Add("int", new CheckBox("Auto Q on interruptable spell", false));
            }

            Draw = Main.AddSubMenu("Drawings");
            {
                Draw.Add("draw", new CheckBox("Enable Drawings"));
                Draw.AddSeparator();
                Draw.Add("Q", new CheckBox("Draw Q"));
                Draw.Add("W", new CheckBox("Draw W", false));
                Draw.Add("E", new CheckBox("Draw E"));
                Draw.Add("R", new CheckBox("Draw R", false));
                Draw.Add("ignite", new CheckBox("Draw Ignite", false));
                Draw.AddSeparator();
                Draw.Add("damage", new CheckBox("Damage Indicator"));
            }
        }
    }
}