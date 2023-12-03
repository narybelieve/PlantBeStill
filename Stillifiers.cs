using HarmonyLib;
using System;
using UnityEngine;
using System.Collections; // IEnumerator

namespace PlantBeStill
{
  static class Stillifiers { 
  
    public static void BeStill(Transform plantModelTransform) 
    {
      if (plantModelTransform == null) return;
      
      // To find all elements with renderers in slots/grownPlants, a search depth of 3 is required
      
      foreach(Transform child in plantModelTransform) {
        Debug.Log("PlantBeStill.Stillifiers.BeStill: Processing "+plantModelTransform.name+"/"+child.name,child);
        
        Renderer renderer = child.GetComponent<Renderer>();
        if (renderer != null) {
          //Debug.Log("PlantBeStill.Stillifiers.BeStill: \tSetting property block for "+plantModelTransform.name+"/"+child.name,child);
          MaterialPropertyBlock p = new MaterialPropertyBlock();
          p.SetColor("_Scale", new Color(0f, 0f, 0f, 0f));
          renderer.SetPropertyBlock(p); // Disable blowing in wind
          //Debug.Log("PlantBeStill.Stillifiers.BeStill: \tSuccessfully set  property block for "+plantModelTransform.name+"/"+child.name,child);
        }
          
        foreach(Transform grandchild in child.transform) {
          //Debug.Log("PlantBeStill.Stillifiers.BeStill: \tProcessing "+plantModelTransform.name+"/"+child.name+"/"+grandchild.name);
          
          renderer = grandchild.GetComponent<Renderer>();
          if (renderer != null) {
            //Debug.Log("PlantBeStill.Stillifiers.BeStill: \t\tSetting property block for "+plantModelTransform.name+"/"+child.name+"/"+grandchild.name);
            MaterialPropertyBlock p = new MaterialPropertyBlock();
            p.SetColor("_Scale", new Color(0f, 0f, 0f, 0f));
            renderer.SetPropertyBlock(p); // Disable blowing in wind
            //Debug.Log("PlantBeStill.Stillifiers.BeStill: \t\tSuccessfully set property block for "+plantModelTransform.name+"/"+child.name+"/"+grandchild.name);
          }
          
          foreach(Transform greatgrandchild in grandchild.transform) {
            //Debug.Log("PlantBeStill.Stillifiers.BeStill: \t\tProcessing "+plantModelTransform.name+"/"+child.name+"/"+grandchild.name+"/"+greatgrandchild.name);
            
            renderer = greatgrandchild.GetComponent<Renderer>();
            if (renderer != null) {
              //Debug.Log("PlantBeStill.Stillifiers.BeStill: \t\t\tSetting property block for "+plantModelTransform.name+"/"+child.name+"/"+grandchild.name+"/"+greatgrandchild.name);
              MaterialPropertyBlock p = new MaterialPropertyBlock();
              p.SetColor("_Scale", new Color(0f, 0f, 0f, 0f));
              renderer.SetPropertyBlock(p); // Disable blowing in wind
              //Debug.Log("PlantBeStill.Stillifiers.BeStill: \t\t\tSuccessfully set property block for "+plantModelTransform.name+"/"+child.name+"/"+grandchild.name+"/"+greatgrandchild.name);
            }
            
            //Debug.Log("PlantBeStill.Stillifiers.BeStill: \t\tSuccessfully processed "+plantModelTransform.name+"/"+child.name+"/"+grandchild.name+"/"+greatgrandchild.name);
          }
          //Debug.Log("PlantBeStill.Stillifiers.BeStill: \tSuccessfully processed "+plantModelTransform.name+"/"+child.name+"/"+grandchild.name);
        }
        //Debug.Log("PlantBeStill.Stillifiers.BeStill: Successfully processed "+plantModelTransform.name+"/"+child.name,child);
      }
    }
  }
}
