// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.CorePatches.Patch_WealthWatcher_CalculateWealthItems_Transpiler
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace DArcaneTechnology.CorePatches
{
  [HarmonyPatch(typeof (WealthWatcher))]
  [HarmonyPatch("CalculateWealthItems")]
  public static class Patch_WealthWatcher_CalculateWealthItems_Transpiler
  {
    private static IEnumerable<CodeInstruction> Transpiler(
      IEnumerable<CodeInstruction> instructions)
    {
      MethodInfo methodInfo = AccessTools.FirstMethod(typeof (ThingOwnerUtility), (Func<MethodInfo, bool>) (m => m.Name == "GetAllThingsRecursively" && m.GetParameters().Length >= 6));
      MethodInfo methodToInject = AccessTools.Method(typeof (Patch_WealthWatcher_CalculateWealthItems_Transpiler), "ExtraItemsFilter");
      FieldInfo tmpThingsField = typeof (WealthWatcher).GetField("tmpThings", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      if (methodInfo == (MethodInfo) null)
        D.Debug("Can't find method ThingOwnerUtility::GetAllThingsRecursively. Please report it to mod developer");
      if (methodToInject == (MethodInfo) null)
        D.Debug("Can't find method Patch_WealthWatcher_CalculateWealthItems_Transpiler::ExtraItemsFilter. Please report it to mod developer");
      if (tmpThingsField == (FieldInfo) null)
        D.Debug("Can't find data structure WealthWatcher.tmpThings. Please report it to mod developer");
      foreach (CodeInstruction code in instructions)
      {
        yield return code;
        if (code.opcode == OpCodes.Call && ((MemberInfo) code.operand).Name == "GetAllThingsRecursively")
        {
          yield return new CodeInstruction(OpCodes.Ldarg_0);
          yield return new CodeInstruction(OpCodes.Ldflda, (object) tmpThingsField);
          yield return new CodeInstruction(OpCodes.Call, (object) methodToInject);
        }
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
