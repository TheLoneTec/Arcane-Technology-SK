// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.HarmonyPatches
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using DArcaneTechnology.CorePatches;
using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

namespace DArcaneTechnology
{
  public class HarmonyPatches : Mod
  {
    public HarmonyPatches(ModContentPack content)
      : base(content)
    {
      Harmony harmony = new Harmony("io.github.dametri.arcanetechnology");
      Assembly executingAssembly = Assembly.GetExecutingAssembly();
      harmony.PatchAll(executingAssembly);
      Log.Message("Running HarmonyPatches");
      MethodInfo original = AccessTools.Method(typeof (EquipmentUtility), "CanEquip", new System.Type[4]
      {
        typeof (Thing),
        typeof (Pawn),
        typeof (string).MakeByRefType(),
        typeof (bool)
      });
      MethodInfo method = AccessTools.Method(typeof (Patch_CanEquip_Postfix), "Postfix", new System.Type[4]
      {
        typeof (Thing),
        typeof (Pawn),
        typeof (string).MakeByRefType(),
        typeof (bool).MakeByRefType()
      });
      if (!(original != (MethodInfo) null) || !(method != (MethodInfo) null))
        return;
      harmony.Patch((MethodBase) original, postfix: new HarmonyMethod(method));
    }
  }
}
