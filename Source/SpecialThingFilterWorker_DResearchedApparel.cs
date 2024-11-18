// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.SpecialThingFilterWorker_DResearchedApparel
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using Verse;

namespace DArcaneTechnology
{
  internal class SpecialThingFilterWorker_DResearchedApparel : SpecialThingFilterWorker
  {
    public override bool Matches(Thing t) => t.def.IsApparel && !Base.IsResearchLocked(t.def);

    public override bool CanEverMatch(ThingDef def) => def.IsApparel && Base.thingDic.ContainsKey(def);
  }
}
