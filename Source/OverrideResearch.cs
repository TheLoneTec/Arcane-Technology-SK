// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.OverrideResearch
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using System.Collections.Generic;
using System.Xml;
using Verse;

namespace DArcaneTechnology
{
  public class OverrideResearch : PatchOperation
  {
    public List<Override> Overrides;

    protected override bool ApplyWorker(XmlDocument xml)
    {
      foreach (Override @override in this.Overrides)
        GearAssigner.overrideAssignment.SetOrAdd<string, string>(@override.thingDef, @override.researchDefName);
      return true;
    }
  }
}
