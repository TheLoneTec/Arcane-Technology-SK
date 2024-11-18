// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.PatchCombatExtendedBase
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using HarmonyLib;
using System;
using System.Reflection;
using Verse;

namespace DArcaneTechnology
{
  [StaticConstructorOnStartup]
  public static class PatchCombatExtendedBase
  {
    public static System.Type jgul;

    static PatchCombatExtendedBase()
    {
      try
      {
        ((Action) (() =>
        {
          Harmony harmony = new Harmony("io.github.dametri.arcanetechnology");
          if (!LoadedModManager.RunningModsListForReading.Any<ModContentPack>((Predicate<ModContentPack>) (x => x.Name.ToLower() == "combat extended")))
            return;
          Log.Message("Arcane Technology: Combat Extended running, attempting to patch");
          PatchCombatExtendedBase.jgul = AccessTools.TypeByName("CombatExtended.JobGiver_UpdateLoadout");
          MethodInfo original = AccessTools.Method(PatchCombatExtendedBase.jgul, "AllowedByBiocode");
          MethodInfo method = AccessTools.Method(typeof (Patch_AllowedByBiocode_Postfix), "Postfix");
          if (!(original != (MethodInfo) null) || !(method != (MethodInfo) null))
            return;
          harmony.Patch((MethodBase) original, postfix: new HarmonyMethod(method));
        }))();
      }
      catch (TypeLoadException ex)
      {
        Log.Message(ex.ToString());
      }
    }
  }
}
