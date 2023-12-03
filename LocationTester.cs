using HarmonyLib;
using System;
using UnityEngine;
using System.Collections; // IEnumerator

namespace PlantBeStill
{
  static class LocationTester { 
    // Test whether an object is in a sub or habitat. Planter.isIndoor cannot be trusted: it's just for the game to determine what kind of plants can be planted and mod planters (like Decorations Mod's Long Planter) can set it to true for outdoor planters
    
    public static bool IsIndoors(GameObject go) 
    {
      // Adapted from BaseLightToggle.cs in BaseLightSwitch
      if (go.GetComponentInParent<SubRoot>() != null) return true; // in a sub
      if (go.gameObject?.transform?.parent?.GetComponent<SubRoot>() != null) return true; // in a sub
      if (go.GetComponentInParent<BaseRoot>() != null) return true; // in a base
      if (go.gameObject?.transform?.parent?.GetComponent<BaseRoot>()) return true; // in a base
      return false;
    }
  }
}
