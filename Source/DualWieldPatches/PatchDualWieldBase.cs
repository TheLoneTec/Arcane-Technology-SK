// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.DualWieldPatches.PatchDualWieldBase
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using HarmonyLib;
using System;
using System.Reflection;
using Verse;

namespace DArcaneTechnology.DualWieldPatches
{
  [StaticConstructorOnStartup]
  public static class PatchDualWieldBase
  {
    public static System.Type aou;

    static PatchDualWieldBase()
    {
      try
      {
        ((Action) (() =>
        {
          Harmony harmony = new Harmony("io.github.dametri.arcanetechnology");
          if (!LoadedModManager.RunningModsListForReading.Any<ModContentPack>((Predicate<ModContentPack>) (x => x.Name.ToLower() == "dual wield")))
            return;
          Log.Message("Arcane Technology: Dual wield running, attempting to patch");
          string name = "DualWield.Harmony.FloatMenuMakerMap_AddHumanlikeOrders";
          PatchDualWieldBase.aou = AccessTools.TypeByName(name);
          MethodInfo original = AccessTools.Method(PatchDualWieldBase.aou, "GetEquipOffHandOption");
          MethodInfo method = AccessTools.Method(typeof (Patch_GetEquipOffHandOption_Prefix), "Prefix");
          if (original != (MethodInfo) null && method != (MethodInfo) null)
          {
            harmony.Patch((MethodBase) original, new HarmonyMethod(method));
            Log.Message("Arcane Technology: Dual wield patched");
          }
          else
            Log.Message("Arcane Technology: Dual wield target method was not found (" + name + ")");
        }))();
      }
      catch (TypeLoadException ex)
      {
        Log.Message(ex.ToString());
      }
    }
  }
}
