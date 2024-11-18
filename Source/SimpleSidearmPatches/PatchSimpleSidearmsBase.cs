// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.SimpleSidearmPatches.PatchSimpleSidearmsBase
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using HarmonyLib;
using System;
using System.Reflection;
using Verse;

namespace DArcaneTechnology.SimpleSidearmPatches
{
  [StaticConstructorOnStartup]
  public static class PatchSimpleSidearmsBase
  {
    public static System.Type aou;

    static PatchSimpleSidearmsBase()
    {
      try
      {
        ((Action) (() =>
        {
          Harmony harmony = new Harmony("io.github.dametri.arcanetechnology");
          if (!LoadedModManager.RunningModsListForReading.Any<ModContentPack>((Predicate<ModContentPack>) (x => x.Name.ToLower() == "simple sidearms")))
            return;
          Log.Message("Arcane Technology: Simple sidearms running, attempting to patch");

          string logMessage = "Arcane Technology: ";

          string name = "PeteTimesSix.SimpleSidearms.Utilities.StatCalculator";
          PatchSimpleSidearmsBase.aou = AccessTools.TypeByName(name);
          MethodInfo original = AccessTools.Method(PatchSimpleSidearmsBase.aou, "isValidSidearm");
          MethodInfo method = AccessTools.Method(typeof (Patch_isValidSidearm_Postfix), "Postfix");
          if (original != (MethodInfo) null && method != (MethodInfo) null)
          {
            harmony.Patch((MethodBase) original, postfix: new HarmonyMethod(method));
            logMessage += "Simple sidearms isValidSidearm patched: true";
          }
          else
          {
              logMessage += "Simple sidearms isValidSidearm patched: false";
              Log.Message("Arcane Technology: Simple sidearms target method was not found (" + name + ")");
          }

          string name2 = "PeteTimesSix.SimpleSidearms.Utilities.StatCalculator";
          PatchSimpleSidearmsBase.aou = AccessTools.TypeByName(name2);
          MethodInfo original2 = AccessTools.Method(PatchSimpleSidearmsBase.aou, "canUseSidearmInstance");
          MethodInfo method2 = AccessTools.Method(typeof(Patch_canUseSidearmInstance_Postfix), "Postfix");
          if (original != (MethodInfo)null && method2 != (MethodInfo)null)
          {
              harmony.Patch((MethodBase)original2, postfix: new HarmonyMethod(method2));
              logMessage += ", canUseSidearmInstance patched: true";
          }
          else
          {
              logMessage += ", canUseSidearmInstance patched: false";
              Log.Message("Arcane Technology: Simple sidearms target method was not found (" + name2 + ")");
          }

          Log.Message(logMessage);
        }))();
      }
      catch (TypeLoadException ex)
      {
        Log.Message(ex.ToString());
      }
    }
  }
}
