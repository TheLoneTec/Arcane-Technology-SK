// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.PatchArmorRackBase
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using DArcaneTechnology.ArmorRackPatches;
using HarmonyLib;
using System;
using System.Reflection;
using Verse;

namespace DArcaneTechnology
{
  [StaticConstructorOnStartup]
  public static class PatchArmorRackBase
  {
    public static System.Type jgul;
    public static System.Type ar;

    static PatchArmorRackBase()
    {
      try
      {
        ((Action) (() =>
        {
          Harmony harmony = new Harmony("io.github.dametri.arcanetechnology");
          if (!LoadedModManager.RunningModsListForReading.Any<ModContentPack>((Predicate<ModContentPack>) (x => x.Name.ToLower() == "armor racks")))
            return;
          Log.Message("Arcane Technology: Armor Racks running, attempting to patch");
          PatchArmorRackBase.jgul = AccessTools.TypeByName("ArmorRacks.Jobs.JobDriver_BaseRackJob");
          MethodInfo original1 = AccessTools.Method(PatchArmorRackBase.jgul, "TryMakePreToilReservations");
          MethodInfo method1 = AccessTools.Method(typeof (Patch_BaseRackJob_Prefix), "Prefix");
          if (original1 != (MethodInfo) null && method1 != (MethodInfo) null)
            harmony.Patch((MethodBase) original1, new HarmonyMethod(method1));
          PatchArmorRackBase.ar = AccessTools.TypeByName("ArmorRacks.Things.ArmorRack");
          MethodInfo original2 = AccessTools.Method(PatchArmorRackBase.ar, "CanStoreWeapon");
          MethodInfo method2 = AccessTools.Method(typeof (Patch_CanStoreWeapon_Postfix), "Postfix");
          if (original2 != (MethodInfo) null && method2 != (MethodInfo) null)
            harmony.Patch((MethodBase) original2, postfix: new HarmonyMethod(method2));
          MethodInfo original3 = AccessTools.Method(PatchArmorRackBase.ar, "CanStoreApparel");
          MethodInfo method3 = AccessTools.Method(typeof (Patch_CanStoreApparel_Postfix), "Postfix");
          if (!(original3 != (MethodInfo) null) || !(method3 != (MethodInfo) null))
            return;
          harmony.Patch((MethodBase) original3, postfix: new HarmonyMethod(method3));
        }))();
      }
      catch (TypeLoadException ex)
      {
        Log.Message(ex.ToString());
      }
    }
  }
}
