using HarmonyLib;
using System;
using UnityEngine;
using System.Collections; // IEnumerator

namespace PlantBeStill
{
    // Triggered when a seed is planted
    
    [HarmonyPatch(typeof(Planter), "AddItem", new Type[] {typeof(Plantable), typeof(int)})]
    class Planter_AddItem_Patch
    {
        // Handle plants as they are added
        static void Postfix(Planter __instance, Plantable plantable, int slotID)
        {
          if(plantable.underwater || !LocationTester.IsIndoors(__instance.gameObject)) {
            return;
          }
          // We are in a sub or habitat and not underwater
          Stillifiers.BeStill(__instance.GetSlotByID(slotID).plantModel.transform);
        }
    }
    
    // Triggered on load
    
    [HarmonyPatch(typeof(Planter), "DeserializeAsync")]
    class Planter_DeserializeAsync_Patch
    {
        // Handle plants as the planter is loaded from save
        static void Postfix(Planter __instance, ref IEnumerator __result)
        {
          Action postfixAction = () => {
            //Debug.Log("Planter_DeserializeAsync_Patch postfix action");
            if(!LocationTester.IsIndoors(__instance.gameObject)) { return; }
            // We are in a sub or habitat
            if(__instance.smallPlantSlots == null) { return; } // smallPlantSlots not initialized yet
            foreach(Planter.PlantSlot ps in __instance.smallPlantSlots) {
              if(ps.plantable == null) { break; }
              if(!ps.plantable.underwater && ps.plantable.aboveWater) { // Not sure why there are redundant bools here, but might as well test both
                Stillifiers.BeStill(ps.slot.transform); // growing plant (and grown plant in some cases)
                if(ps.plantable.linkedGrownPlant != null) {
                  Stillifiers.BeStill(ps.plantable.linkedGrownPlant.transform); // grown plant
                }
              }
            }
            if(__instance.bigPlantSlots == null) { return; } // bigPlantSlots not initialized yet
            foreach(Planter.PlantSlot ps in __instance.bigPlantSlots) {
              if(ps.plantable == null) { break; }
              if(!ps.plantable.underwater && ps.plantable.aboveWater) { // Not sure why there are redundant bools here, but might as well test both
                Stillifiers.BeStill(ps.slot.transform); // growing plant (and grown plant in some cases)
                if(ps.plantable.linkedGrownPlant != null) {
                  Stillifiers.BeStill(ps.plantable.linkedGrownPlant.transform); // grown plant
                }
              }
            }
            //Debug.Log("Planter_DeserializeAsync_Patch Complete");
          };
          var myEnumerator = new SimpleEnumerator()
          {
            enumerator = __result,
            postfixAction = postfixAction
          };
          //Debug.Log("Planter_DeserializeAsync_Patch setting Enumerator");
          __result = myEnumerator.GetEnumerator();
        }
    }
}
