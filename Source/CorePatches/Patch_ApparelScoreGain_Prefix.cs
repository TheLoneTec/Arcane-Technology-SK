// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.CorePatches.Patch_ApparelScoreGain_Prefix
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using HarmonyLib;
using RimWorld;
using Verse;

namespace DArcaneTechnology.CorePatches
{
  [HarmonyPatch(typeof (JobGiver_OptimizeApparel))]
  [HarmonyPatch("ApparelScoreGain")]
  internal class Patch_ApparelScoreGain_Prefix
  {
    public static bool Prefix(Pawn pawn, Apparel ap, ref float __result)
    {
      if (!Base.IsResearchLocked(ap.def, pawn))
        return true;
      __result = -5000f;
      return false;
    }
  }
}
