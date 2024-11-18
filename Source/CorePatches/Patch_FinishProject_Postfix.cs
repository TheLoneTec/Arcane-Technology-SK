// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.CorePatches.Patch_FinishProject_Postfix
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using HarmonyLib;
using RimWorld;

namespace DArcaneTechnology.CorePatches
{
  [HarmonyPatch(typeof (ResearchManager))]
  [HarmonyPatch("FinishProject")]
  internal class Patch_FinishProject_Postfix
  {
    public static void Postfix() => Base.playerTechLevel = Base.GetPlayerTech();
  }
}
