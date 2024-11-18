// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.DualWieldPatches.Patch_GetEquipOffHandOption_Prefix
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using System;
using Verse;

namespace DArcaneTechnology.DualWieldPatches
{
  internal class Patch_GetEquipOffHandOption_Prefix
  {
    private static bool Prefix(Pawn pawn, ThingWithComps equipment, ref FloatMenuOption __result)
    {
      if (!Base.IsResearchLocked(equipment.def, pawn))
        return true;
      string labelShort = equipment.LabelShort;
      __result = new FloatMenuOption((string) ("CannotEquip".Translate((NamedArgument) labelShort) + " " + "DW_AsOffHand".Translate() + " (" + "DUnknownTechnology".Translate() + ")"), (Action) null);
      return false;
    }
  }
}
