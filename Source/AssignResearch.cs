// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.AssignResearch
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using System.Collections.Generic;
using System.Xml;
using Verse;

namespace DArcaneTechnology
{
  public class AssignResearch : PatchOperation
  {
    public List<Assignment> Assignments;

    protected override bool ApplyWorker(XmlDocument xml)
    {
      foreach (Assignment assignment in this.Assignments)
        GearAssigner.hardAssignment.SetOrAdd<string, string>(assignment.thingDef, assignment.researchDefName);
      return true;
    }
  }
}
