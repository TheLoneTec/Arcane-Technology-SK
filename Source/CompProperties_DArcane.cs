// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.CompProperties_DArcane
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using Verse;

namespace DArcaneTechnology
{
  internal class CompProperties_DArcane : CompProperties
  {
    public ResearchProjectDef project;

    public CompProperties_DArcane(ResearchProjectDef rpd)
    {
      this.compClass = typeof (CompDArcane);
      this.project = rpd;
    }
  }
}
