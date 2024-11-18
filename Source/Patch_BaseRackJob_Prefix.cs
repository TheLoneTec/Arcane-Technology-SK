// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.Patch_BaseRackJob_Prefix
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using Verse;
using Verse.AI;

namespace DArcaneTechnology
{
  internal class Patch_BaseRackJob_Prefix
  {
    private static bool Prefix(JobDriver __instance, ref bool __result)
    {
      Thing thing = __instance.job.GetTarget(TargetIndex.A).Thing;
      if (thing == null || !Base.IsResearchLocked(thing.def))
        return true;
      __result = false;
      return false;
    }
  }
}
