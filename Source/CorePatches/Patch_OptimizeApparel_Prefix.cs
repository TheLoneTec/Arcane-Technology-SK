// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.CorePatches.Patch_OptimizeApparel_Prefix
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace DArcaneTechnology.CorePatches
{
  [HarmonyPatch(typeof (JobGiver_OptimizeApparel))]
  [HarmonyPatch("TryGiveJob")]
  internal class Patch_OptimizeApparel_Prefix
  {
    public static bool Prefix(Pawn pawn, ref Verse.AI.Job __result)
    {
      if (pawn.IsQuestLodger() || !DebugViewSettings.debugApparelOptimize && Find.TickManager.TicksGame < pawn.mindState.nextApparelOptimizeTick)
        return true;
      foreach (ThingWithComps targetA in pawn.equipment.AllEquipmentListForReading)
      {
        if (Base.IsResearchLocked(targetA.def, pawn))
        {
          Verse.AI.Job job = JobMaker.MakeJob(JobDefOf.DropEquipment, (LocalTargetInfo) (Thing) targetA);
          __result = job;
          return false;
        }
      }
      List<Apparel> wornApparel = pawn.apparel.WornApparel;
      for (int index = wornApparel.Count - 1; index >= 0; --index)
      {
        if (Base.IsResearchLocked(wornApparel[index].def, pawn))
        {
          Verse.AI.Job job = JobMaker.MakeJob(JobDefOf.RemoveApparel, (LocalTargetInfo) (Thing) wornApparel[index]);
          job.haulDroppedApparel = true;
          __result = job;
          return false;
        }
      }
      return true;
    }
  }
}
