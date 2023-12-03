using HarmonyLib;
using System;
using UnityEngine;
using System.Collections; // IEnumerator
using System.Collections.Generic; // List

namespace PlantBeStill
{
    // Triggered when a "growing" plant matures into a "grown plant"
    
    [HarmonyPatch(typeof(GrowingPlant), "SpawnGrownModelAsync")]
    class GrowingPlant_SpawnGrownModelAsync_Patch 
    {
        static void Postfix(GrowingPlant __instance, ref IEnumerator __result)
        {
          Action postfixAction = () => { 
            if(!LocationTester.IsIndoors(__instance.gameObject)) { return; }
            // We are in a sub or habitat
            Plantable plantable = __instance.seed;
            if(!plantable.underwater && plantable.aboveWater) { // Not sure why there are redundant bools here, but might as well test both
              //Stillifiers.GrownPlantsBeStill(plantable.currentPlanter.grownPlantsRoot); 
              Stillifiers.BeStill(plantable.linkedGrownPlant.transform); // grown plant
            }
          };
          var myEnumerator = new SimpleEnumerator()
          {
            enumerator = __result,
            postfixAction = postfixAction
          };
          //Debug.Log("PlantBeStill.GrowingPlant_SpawnGrownModelAsync_Patch: Setting Enumerator");
          __result = myEnumerator.GetEnumerator();
        }
    }
}
