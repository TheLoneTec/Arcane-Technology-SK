// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.StatWorker_ResearchTech
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using RimWorld;
using System.Linq;
using Verse;

namespace DArcaneTechnology
{
  internal class StatWorker_ResearchTech : StatWorker
  {
    public override float GetValueUnfinalized(StatRequest req, bool applyPostProcess = true) => req.HasThing && Base.thingDic.ContainsKey(req.Thing.def) ? 0.0f : -1f;

    public override string GetExplanationUnfinalized(
      StatRequest req,
      ToStringNumberSense numberSense)
    {
      return "";
    }

    public override string GetExplanationFinalizePart(
      StatRequest req,
      ToStringNumberSense numberSense,
      float finalVal)
    {
      return "";
    }

    public override string GetStatDrawEntryLabel(
      StatDef stat,
      float value,
      ToStringNumberSense numberSense,
      StatRequest optionalReq,
      bool finalized = true)
    {
      return optionalReq.HasThing && Base.thingDic.ContainsKey(optionalReq.Thing.def) && DefDatabase<ResearchProjectDef>.AllDefs.Contains<ResearchProjectDef>(Base.thingDic[optionalReq.Thing.def]) ? (string) Base.thingDic[optionalReq.Thing.def].LabelCap : "None";
    }
  }
}
