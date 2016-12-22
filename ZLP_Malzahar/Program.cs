using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

namespace ZLP_Malzahar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoad;
        }

        public static void OnLoad(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Malzahar") return;

            Chat.Print("ZLP Malzahar loaded. GLHF!");

            Menus.Initialize();
            Spells.Initialize();

            Game.OnTick += OnTick;
            Game.OnTick += Modes.KillSteal;
            Game.OnTick += More.StopOrb;

            Orbwalker.OnUnkillableMinion += Modes.LastHit;
            Interrupter.OnInterruptableSpell += Modes.OnInt;
            Obj_AI_Base.OnProcessSpellCast += More.OnCast;
            Drawing.OnDraw += Drawings.OnDraw;
        }

        public static void OnTick(EventArgs args)
        {
            if (Player.Instance.IsDead || Player.Instance.IsRecalling()) return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                Modes.Combo();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                Modes.Harass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                Modes.LaneClear();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                Modes.JungleClear();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                Modes.Flee();
        }
    }
}
