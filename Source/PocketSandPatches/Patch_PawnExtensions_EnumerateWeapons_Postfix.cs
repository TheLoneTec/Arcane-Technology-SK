// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.PocketSandPatches.Patch_PawnExtensions_EnumerateWeapons_Postfix
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using System.Collections.Generic;
using Verse;

namespace DArcaneTechnology.PocketSandPatches
{
  public static class Patch_PawnExtensions_EnumerateWeapons_Postfix
  {
    private static IEnumerable<ThingWithComps> Postfix(
      IEnumerable<ThingWithComps> __result)
    {
      foreach (ThingWithComps thingWithComps in __result)
      {
        if (!Base.IsResearchLocked(thingWithComps.def))
          yield return thingWithComps;
      }
    }

    public static void ExtraItemsFilter(ref List<Thing> tmpThings)
    {
      if (!ArcaneTechnologySettings.exemptFromWealthCalculation)
        return;
      List<Thing> collection = new List<Thing>();
      foreach (Thing thing in tmpThings)
      {
        if (!Base.IsResearchLocked(thing.def))
          collection.Add(thing);
      }
      tmpThings = new List<Thing>((IEnumerable<Thing>) collection);
    }
  }
}
