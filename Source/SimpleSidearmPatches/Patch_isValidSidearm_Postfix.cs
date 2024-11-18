// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.SimpleSidearmPatches.Patch_isValidSidearm_Postfix
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using SimpleSidearms.rimworld;
using System;
using Verse;

namespace DArcaneTechnology.SimpleSidearmPatches
{
  internal class Patch_isValidSidearm_Postfix
  {
    private static void Postfix(ThingDefStuffDefPair sidearm, ref bool __result, ref string errString)
    {
      if (!__result)
        return;
      try
      {
        if (!Base.IsResearchLocked(sidearm.thing))
          return;
        __result = false;
        errString = (string) "DUnknownTechnology".Translate();
      }
      catch (Exception ex)
      {
        Log.ErrorOnce("Error in Arcane Technology simple sidearms postfix, exception is " + ex?.ToString(), 6969420);
      }
    }
  }
    internal class Patch_canUseSidearmInstance_Postfix
    {
        private static void Postfix(ThingWithComps sidearmThing, Pawn pawn, ref bool __result, ref string errString)
        {
            if (!__result)
                return;
            try
            {
                if (sidearmThing == null)
                    return;
                if (!Base.IsResearchLocked(sidearmThing.def))
                    return;
                __result = false;
                errString = (string)"DUnknownTechnology".Translate();
            }
            catch (Exception ex)
            {
                Log.ErrorOnce("Error in Arcane Technology simple sidearms postfix, exception is " + ex?.ToString(), 6969420);
            }
        }
    }
}
