// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.AwesomeInventoryPatches.PatchAwesomeInventoryBase
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using HarmonyLib;
using System;
using System.Reflection;
using Verse;

namespace DArcaneTechnology.AwesomeInventoryPatches
{
  [StaticConstructorOnStartup]
  public static class PatchAwesomeInventoryBase
  {
    public static System.Type aou;

    static PatchAwesomeInventoryBase()
    {
      try
      {
        ((Action) (() =>
        {
          Harmony harmony = new Harmony("io.github.dametri.arcanetechnology");
          if (!LoadedModManager.RunningModsListForReading.Any<ModContentPack>((Predicate<ModContentPack>) (x => x.Name.ToLower() == "awesome inventory" || x.Name.ToLower() == "awesome inventory (unofficial)")))
            return;
          Log.Message("Arcane Technology: Awesome Inventory running, attempting to patch");
          string name = "AwesomeInventory.ApparelOptionUtility";
          PatchAwesomeInventoryBase.aou = AccessTools.TypeByName(name);
          MethodInfo original = AccessTools.Method(PatchAwesomeInventoryBase.aou, "CanWear");
          MethodInfo method = AccessTools.Method(typeof (Patch_CanWear_Postfix), "Postfix");
          if (original != (MethodInfo) null && method != (MethodInfo) null)
          {
            harmony.Patch((MethodBase) original, postfix: new HarmonyMethod(method));
            Log.Message("Arcane Technology: Awesome Inventory patched");
          }
          else
            Log.Message("Arcane Technology: Awesome Inventory target method was not found (" + name + ")");
        }))();
      }
      catch (TypeLoadException ex)
      {
        Log.Message(ex.ToString());
      }
    }
  }
}
