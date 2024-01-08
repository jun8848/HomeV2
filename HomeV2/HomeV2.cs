using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace HomeV2
{
    public class HomeV2 : IModApi
    {
        public void InitMod(Mod _modInstance)
        {
            Harmony harmony = new Harmony(base.GetType().ToString());
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            
        }
    }
}
