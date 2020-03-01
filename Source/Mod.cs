using Verse;
using HarmonyLib;

namespace ThermalUnits
{
    public class Mod : Verse.Mod
    {
        public Mod(ModContentPack content) : base(content)
        {
            new Harmony("MySteamUsername.rimworld.ThermalUnits.main").PatchAll();
        }
    }
}
