// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.ExemptResearch
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using System.Collections.Generic;
using System.Xml;
using Verse;

namespace DArcaneTechnology
{
  public class ExemptResearch : PatchOperation
  {
    public List<string> Exemptions;

    protected override bool ApplyWorker(XmlDocument xml)
    {
      foreach (string exemption in this.Exemptions)
      {
        if (!GearAssigner.exemptProjects.Contains(exemption))
          GearAssigner.exemptProjects.Add(exemption);
      }
      return true;
    }
  }
}
