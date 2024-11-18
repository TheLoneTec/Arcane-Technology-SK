// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.PocketSandPatches.PatchPocketSandBase
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using HarmonyLib;
using System;
using System.Reflection;
using Verse;

namespace DArcaneTechnology.PocketSandPatches
{
  [StaticConstructorOnStartup]
  public static class PatchPocketSandBase
  {
    public static System.Type aou;

    static PatchPocketSandBase()
    {
      try
      {
        ((Action) (() =>
        {
          Harmony harmony = new Harmony("io.github.dametri.arcanetechnology");
          if (!LoadedModManager.RunningModsListForReading.Any<ModContentPack>((Predicate<ModContentPack>) (x => x.Name.ToLower() == "pocket sand")))
            return;
          Log.Message("Arcane Technology: Pocket Sand is running, attempting to patch");
          string name = "PocketSand.PawnExtensions";
          PatchPocketSandBase.aou = AccessTools.TypeByName(name);
          MethodInfo original = AccessTools.Method(PatchPocketSandBase.aou, "EnumerateWeapons");
          MethodInfo method = AccessTools.Method(typeof (Patch_PawnExtensions_EnumerateWeapons_Postfix), "Postfix");
          if (original != (MethodInfo) null && method != (MethodInfo) null)
          {
            harmony.Patch((MethodBase) original, postfix: new HarmonyMethod(method));
            Log.Message("Arcane Technology: Pocket Sand patched");
          }
          else
            Log.Message("Arcane Technology: Pocket Sand target method was not found (" + name + ")");
        }))();
      }
      catch (TypeLoadException ex)
      {
        Log.Message(ex.ToString());
      }
    }
  }
}
