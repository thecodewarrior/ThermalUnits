﻿using System.Reflection;
using System.Linq;
using Verse;
using RimWorld;
using UnityEngine;
using HarmonyLib;

namespace ThermalUnits
{
    public class Mod : Verse.Mod
    {
        public Mod(ModContentPack content) : base(content)
        {
#if DEBUG
            Harmony.DEBUG = true;
#endif

            Harmony harmony = new Harmony("thecodewarrior.rimworld.ThermalUnits.main");

            harmony.PatchAll();
        }
    }
}