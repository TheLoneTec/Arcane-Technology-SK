// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.ArcaneTechnologyMod
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using System;
using UnityEngine;
using Verse;

namespace DArcaneTechnology
{
  internal class ArcaneTechnologyMod : Mod
  {
    private ArcaneTechnologySettings settings;

    public ArcaneTechnologyMod(ModContentPack content)
      : base(content)
    {
      this.settings = this.GetSettings<ArcaneTechnologySettings>();
      LongEventHandler.QueueLongEvent(new Action(Base.Initialize), "DArcaneTech.BuildingDatabase", false, (Action<Exception>) null);
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
      ArcaneTechnologySettings.DrawSettings(inRect);
      base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory() => "Arcane Technology";

    public override void WriteSettings()
    {
      ArcaneTechnologySettings.WriteAll();
      base.WriteSettings();
    }
  }
}
