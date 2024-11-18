// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.ArcaneTechnologySettings
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace DArcaneTechnology
{
  internal class ArcaneTechnologySettings : ModSettings
  {
    public static bool useHighestResearched = false;
    public static bool usePercentResearched = true;
    public static float percentResearchNeeded = 0.25f;
    public static bool useActualTechLevel = false;
    public static TechLevel minToRestrict = TechLevel.Spacer;
    public static bool restrictOnTechLevel = false;
    public static bool evenResearched = false;
    public static bool exemptClothing = true;
    public static int howManyTechLevelsAheadOfYours = 0;
    public static bool exemptFromWealthCalculation = false;

    public override void ExposeData()
    {
      Scribe_Values.Look<bool>(ref ArcaneTechnologySettings.useHighestResearched, "useHighestResearched");
      Scribe_Values.Look<bool>(ref ArcaneTechnologySettings.usePercentResearched, "usePercentResearched", true);
      Scribe_Values.Look<float>(ref ArcaneTechnologySettings.percentResearchNeeded, "percentResearchNeeded", 0.25f);
      Scribe_Values.Look<bool>(ref ArcaneTechnologySettings.useActualTechLevel, "useActualTechLevel");
      Scribe_Values.Look<TechLevel>(ref ArcaneTechnologySettings.minToRestrict, "minToRestrict", TechLevel.Spacer);
      Scribe_Values.Look<bool>(ref ArcaneTechnologySettings.restrictOnTechLevel, "restrictOnTechLevel");
      Scribe_Values.Look<bool>(ref ArcaneTechnologySettings.evenResearched, "evenResearched");
      Scribe_Values.Look<bool>(ref ArcaneTechnologySettings.exemptClothing, "exemptClothing", true);
      Scribe_Values.Look<int>(ref ArcaneTechnologySettings.howManyTechLevelsAheadOfYours, "howManyTechLevelsAheadOfYours");
      Scribe_Values.Look<bool>(ref ArcaneTechnologySettings.exemptFromWealthCalculation, "exemptFromWealthCalculation");
      base.ExposeData();
    }

    public static void WriteAll()
    {
      if (ArcaneTechnologySettings.useHighestResearched)
      {
        ArcaneTechnologySettings.usePercentResearched = false;
        ArcaneTechnologySettings.useActualTechLevel = false;
      }
      else if (ArcaneTechnologySettings.usePercentResearched)
      {
        ArcaneTechnologySettings.useHighestResearched = false;
        ArcaneTechnologySettings.useActualTechLevel = false;
      }
      else if (ArcaneTechnologySettings.useActualTechLevel)
      {
        ArcaneTechnologySettings.useHighestResearched = false;
        ArcaneTechnologySettings.usePercentResearched = false;
      }
      if (Current.Game == null)
        return;
      Base.playerTechLevel = Base.GetPlayerTech();
    }

    public static void DrawSettings(Rect rect)
    {
      Listing_Standard listingStandard = new Listing_Standard(GameFont.Small);
      listingStandard.ColumnWidth = rect.width - 30f;
      listingStandard.Begin(rect);
      listingStandard.Gap();
      string str = "Your calculated tech level: ";
      string label = ArcaneTechnologySettings.restrictOnTechLevel ? (Current.Game == null ? str + "Not in game" : str + Enum.GetName(typeof (TechLevel), (object) Base.playerTechLevel)) : str + "N/A (fixed tech level)";
      float height = listingStandard.Label(label).height;
      listingStandard.GapLine();
      listingStandard.CheckboxLabeled("Try to exempt clothing research options", ref ArcaneTechnologySettings.exemptClothing, "Refers to a list of clothing research projects and exempts their products from restriction.");
      listingStandard.GapLine();
      listingStandard.CheckboxLabeled("Restrict items based on the colony's current tech level", ref ArcaneTechnologySettings.restrictOnTechLevel, "Instead of manually setting the tech level above which to restrict tech, you can automatically use the colony's tech level.");
      listingStandard.Gap();
      listingStandard.GapLine();
      if (ArcaneTechnologySettings.restrictOnTechLevel)
      {
        listingStandard.Label("Method by which this mod will calculate your tech level:");
        listingStandard.Gap();
        bool flag1 = listingStandard.RadioButton("Highest tech researched", ArcaneTechnologySettings.useHighestResearched, tooltip: "If you have even one tech in a tech level researched, you will be considered that tech for the purpose of raids.");
        listingStandard.Gap();
        bool flag2 = listingStandard.RadioButton("Tech completion of a certain percent", ArcaneTechnologySettings.usePercentResearched, tooltip: "Once you research a certain % of a tech level's available technologies, you will be considered that tech level for the purpose of raids.");
        if (ArcaneTechnologySettings.usePercentResearched)
        {
          ArcaneTechnologySettings.percentResearchNeeded = Mathf.Clamp(ArcaneTechnologySettings.percentResearchNeeded, 0.05f, 1f);
          ArcaneTechnologySettings.percentResearchNeeded = Widgets.HorizontalSlider(listingStandard.GetRect(height).LeftPartPixels(450f), ArcaneTechnologySettings.percentResearchNeeded, 0.05f, 1f, label: (Mathf.RoundToInt(ArcaneTechnologySettings.percentResearchNeeded * 100f).ToString() + "%"), roundTo: 0.05f);
        }
        else
          listingStandard.Gap();
        listingStandard.Gap();
        bool flag3 = listingStandard.RadioButton("Actual colonist tech level", ArcaneTechnologySettings.useActualTechLevel, tooltip: "Not recommended unless you have a mod to increase your tech level somehow.");
        listingStandard.Gap();
        listingStandard.Label(string.Format("You can use items of {0} tech level(s) ahead of yours", (object) ArcaneTechnologySettings.howManyTechLevelsAheadOfYours), tooltip: "E.g. when it's set to 1, tribals would be able to use Medieval items.");
        ArcaneTechnologySettings.howManyTechLevelsAheadOfYours = (int) Widgets.HorizontalSlider(listingStandard.GetRect(height).LeftPartPixels(450f), (float) ArcaneTechnologySettings.howManyTechLevelsAheadOfYours, 0.0f, 2f, roundTo: 1f);
        listingStandard.Gap();
        if (flag1 && flag1 != ArcaneTechnologySettings.useHighestResearched)
        {
          ArcaneTechnologySettings.useHighestResearched = true;
          ArcaneTechnologySettings.usePercentResearched = false;
          ArcaneTechnologySettings.useActualTechLevel = false;
          if (Current.Game != null)
            Base.playerTechLevel = Base.GetPlayerTech();
        }
        else if (flag2 && flag2 != ArcaneTechnologySettings.usePercentResearched)
        {
          ArcaneTechnologySettings.useHighestResearched = false;
          ArcaneTechnologySettings.usePercentResearched = true;
          ArcaneTechnologySettings.useActualTechLevel = false;
          if (Current.Game != null)
            Base.playerTechLevel = Base.GetPlayerTech();
        }
        else if (flag3 && flag3 != ArcaneTechnologySettings.useActualTechLevel)
        {
          ArcaneTechnologySettings.useHighestResearched = false;
          ArcaneTechnologySettings.usePercentResearched = false;
          ArcaneTechnologySettings.useActualTechLevel = true;
          if (Current.Game != null)
            Base.playerTechLevel = Base.GetPlayerTech();
        }
      }
      else
      {
        Rect rect1 = listingStandard.GetRect(Text.LineHeight);
        Rect rect2 = rect1.LeftPartPixels(300f);
        Rect rect3 = rect1.RightPartPixels(rect1.width - 300f);
        Widgets.Label(rect2, "Minimum tech level to restrict items");
        double minToRestrict = (double) ArcaneTechnologySettings.minToRestrict;
        string name = Enum.GetName(typeof (TechLevel), (object) (int) ArcaneTechnologySettings.minToRestrict);
        ArcaneTechnologySettings.minToRestrict = (TechLevel) Mathf.RoundToInt(Widgets.HorizontalSlider(rect3, (float) minToRestrict, 1f, 7f, label: name, roundTo: 1f));
      }
      listingStandard.GapLine();
      listingStandard.CheckboxLabeled("Restrict even if the item is researched", ref ArcaneTechnologySettings.evenResearched, "Warning: without a mod to let your colony advance in tech, you will NEVER be able to use certain items.");
      listingStandard.GapLine();
      listingStandard.CheckboxLabeled("Exclude restricted items from colony wealth calculation", ref ArcaneTechnologySettings.exemptFromWealthCalculation);
      listingStandard.Gap();
      listingStandard.Gap();
      listingStandard.End();
    }
  }
}
