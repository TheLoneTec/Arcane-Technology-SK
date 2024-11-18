// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.Patch_CanWear_Postfix
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using RimWorld;
using Verse;

namespace DArcaneTechnology
{
  internal class Patch_CanWear_Postfix
  {
    private static void Postfix(Pawn pawn, Apparel apparel, ref bool __result) => __result = __result && !Base.IsResearchLocked(apparel.def, pawn);
  }
}
