// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.CorePatches.Patch_CanEquip_Postfix
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using Verse;

namespace DArcaneTechnology.CorePatches
{
  internal class Patch_CanEquip_Postfix
  {
    private static void Postfix(Thing thing, Pawn pawn, ref string cantReason, ref bool __result)
    {
      if (!__result || !Base.IsResearchLocked(thing.def, pawn))
        return;
      __result = false;
      cantReason = (string) "DUnknownTechnology".Translate();
    }
  }
}
