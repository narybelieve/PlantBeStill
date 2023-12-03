using HarmonyLib;
using System;
using UnityEngine;
using System.Collections; // IEnumerator

namespace PlantBeStill
{
  class SimpleEnumerator : IEnumerable // Adapted from https://gist.github.com/pardeike/c873b95e983e4814a8f6eb522329aee5
  {
    public IEnumerator enumerator;
    public Action postfixAction;
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    public IEnumerator GetEnumerator()
    {
      while (enumerator.MoveNext())
      {
        //Debug.Log("PlantBeStill.SimpleEnumerator::GetEnumerator yield returning");
        yield return enumerator.Current;
      }
      //Debug.Log("PlantBeStill.Enumerator: Calling Postfix Action");
      postfixAction();
    }
  }
}
