using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace ZLP_Ryze
{
    public class Menus
    {
        public static Menu Main, Combo, Clear, Misc, Draw;
        public static Slider MinDelay, MaxDelay, ComboHealth, HarassMana, LaneMana, JungleMana, LastMana, Auto;

        public static void Initialize()
        {
            Main = MainMenu.AddMenu("ZLP Ryze", "ZLP_Ryze");
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
                ComboHealth = Combo.Add("MinHP", new Slider("Defensive combo if HP below {0}% (0 = disable)", 30));
                Combo.AddGroupLabel("Harass");
                Combo.Add("Qh", new CheckBox("Use Q"));
                Combo.Add("Eh", new CheckBox("Use E"));
                HarassMana = Combo.Add("HarassMP", new Slider("Don't use spells if MP below {0}%", 40));
                Combo.Add("auto", new CheckBox("Auto Harass (Q)", false));
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
                Clear.Add("Wlh", new CheckBox("Use W"));
                Clear.Add("Elh", new CheckBox("Use E"));
                LastMana = Clear.Add("lhmp", new Slider("Don't use spells if MP below {0}%", 25));
            }

            Misc = Main.AddSubMenu("Misc Settings");
            {
                Misc.AddGroupLabel("Kill Steal");
                Misc.Add("Q", new CheckBox("Use Q"));
                Misc.Add("W", new CheckBox("Use W"));
                Misc.Add("E", new CheckBox("Use E"));
                Misc.Add("ignite", new CheckBox("Use Ignite"));
                Misc.AddGroupLabel("Escape");
                Misc.Add("esc", new KeyBind("Escape with R + Zhonya's", false, KeyBind.BindTypes.HoldActive, "A".ToCharArray()[0]));
                Auto = Misc.Add("auto", new Slider("Auto R + Zhonya's if HP below {0}% (0 = disable)", 20));
                Misc.AddGroupLabel("Other Options");
                Misc.Add("gap", new CheckBox("Auto W on gapcloser", false));
                Misc.Add("stack", new CheckBox("Auto stack Tear"));
            }

            Draw = Main.AddSubMenu("Drawings");
            {
                Draw.Add("draw", new CheckBox("Enable Drawings"));
                Draw.AddSeparator();
                Draw.Add("Q", new CheckBox("Draw Q"));
                Draw.Add("WE", new CheckBox("Draw W/E"));
                Draw.Add("R", new CheckBox("Draw R"));
                Draw.Add("ignite", new CheckBox("Draw Ignite", false));
                Draw.AddSeparator();
                Draw.Add("damage", new CheckBox("Damage Indicator"));
            }
        }
    }
}