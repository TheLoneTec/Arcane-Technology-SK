// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.CompDArcane
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using Verse;

namespace DArcaneTechnology
{
  internal class CompDArcane : ThingComp
  {
    public virtual CompProperties_DArcane PropsArcane => (CompProperties_DArcane) this.props;

    public override string CompInspectStringExtra() => Base.IsResearchLocked(this.parent.def) ? (string) ("Unknown technology (" + this.PropsArcane.project.LabelCap + ")") : base.CompInspectStringExtra();
  }
}
