// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.Base
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace DArcaneTechnology
{
  [StaticConstructorOnStartup]
  public static class Base
  {
    public static Dictionary<ThingDef, ResearchProjectDef> thingDic = (Dictionary<ThingDef, ResearchProjectDef>) null;
    public static Dictionary<ResearchProjectDef, List<ThingDef>> researchDic = (Dictionary<ResearchProjectDef, List<ThingDef>>) null;
    public static Dictionary<TechLevel, List<ResearchProjectDef>> strataDic = new Dictionary<TechLevel, List<ResearchProjectDef>>();
    private static TechLevel cachedTechLevel = TechLevel.Undefined;

    public static TechLevel playerTechLevel
    {
      get
      {
        if (Base.cachedTechLevel == TechLevel.Undefined)
          Base.cachedTechLevel = Base.GetPlayerTech();
        return Base.cachedTechLevel;
      }
      set
      {
        bool flag = false;
        if (Base.cachedTechLevel == TechLevel.Undefined)
          flag = true;
        Base.cachedTechLevel = value;
        int num = flag ? 1 : 0;
      }
    }

    public static void Initialize()
    {
      foreach (ResearchProjectDef researchProjectDef in DefDatabase<ResearchProjectDef>.AllDefsListForReading)
      {
        if (!Base.strataDic.ContainsKey(researchProjectDef.techLevel))
          Base.strataDic.Add(researchProjectDef.techLevel, new List<ResearchProjectDef>());
        if (!Base.strataDic[researchProjectDef.techLevel].Contains(researchProjectDef))
          Base.strataDic[researchProjectDef.techLevel].Add(researchProjectDef);
      }
      Base.MakeDictionaries();
    }

    public static void MakeDictionaries()
    {
      Base.thingDic = new Dictionary<ThingDef, ResearchProjectDef>();
      Base.researchDic = new Dictionary<ResearchProjectDef, List<ThingDef>>();
      foreach (RecipeDef recipe in DefDatabase<RecipeDef>.AllDefsListForReading)
      {
        ResearchProjectDef bestRpdForRecipe = Base.GetBestRPDForRecipe(recipe);
        if (bestRpdForRecipe != null && recipe.ProducedThingDef != null)
        {
          ThingDef producedThingDef = recipe.ProducedThingDef;
          if (producedThingDef.GetCompProperties<CompProperties_DArcane>() == null)
            producedThingDef.comps.Add((CompProperties) new CompProperties_DArcane(bestRpdForRecipe));
          Base.thingDic.SetOrAdd<ThingDef, ResearchProjectDef>(producedThingDef, bestRpdForRecipe);
          List<ThingDef> thingDefList;
          if (Base.researchDic.TryGetValue(bestRpdForRecipe, out thingDefList))
            thingDefList.Add(producedThingDef);
          else
            Base.researchDic.Add(bestRpdForRecipe, new List<ThingDef>()
            {
              producedThingDef
            });
        }
      }
      GearAssigner.HardAssign(ref Base.thingDic, ref Base.researchDic);
      GearAssigner.OverrideAssign(ref Base.thingDic, ref Base.researchDic);
    }

    public static ResearchProjectDef GetBestRPDForRecipe(RecipeDef recipe)
    {
      ThingDef producedThingDef = recipe.ProducedThingDef;
      if (producedThingDef == null)
        return (ResearchProjectDef) null;
      ResearchProjectDef rpd;
      if (GearAssigner.GetOverrideAssignment(producedThingDef, out rpd))
        return rpd;
      if (producedThingDef.category == ThingCategory.Building || !producedThingDef.IsWeapon && !producedThingDef.IsApparel)
        return (ResearchProjectDef) null;
      if (recipe.researchPrerequisite != null)
        return recipe.researchPrerequisite;
      if (recipe.researchPrerequisites != null && recipe.researchPrerequisites.Count > 0)
        return recipe.researchPrerequisites[0];
      if (recipe.recipeUsers != null)
      {
        float num = 99999f;
        TechLevel techLevel = TechLevel.Archotech;
        ThingDef thingDef = (ThingDef) null;
        foreach (ThingDef recipeUser in recipe.recipeUsers)
        {
          if (recipeUser.researchPrerequisites == null || recipeUser.researchPrerequisites.Count <= 0)
            return (ResearchProjectDef) null;
          float statValueAbstract = recipeUser.GetStatValueAbstract(StatDefOf.WorkTableWorkSpeedFactor);
          if ((double) statValueAbstract <= (double) num && recipeUser.researchPrerequisites[0].techLevel <= techLevel)
          {
            thingDef = recipeUser;
            num = statValueAbstract;
            techLevel = recipeUser.researchPrerequisites[0].techLevel;
          }
        }
        if (thingDef != null)
          return thingDef.researchPrerequisites[0];
      }
      return (ResearchProjectDef) null;
    }

    public static bool InLockedTechRange(ResearchProjectDef rpd) => ArcaneTechnologySettings.restrictOnTechLevel ? rpd.techLevel > Base.playerTechLevel + (byte) ArcaneTechnologySettings.howManyTechLevelsAheadOfYours : rpd.techLevel >= ArcaneTechnologySettings.minToRestrict;

    public static bool Locked(ResearchProjectDef rpd) => rpd != null && DefDatabase<ResearchProjectDef>.AllDefs.Contains<ResearchProjectDef>(rpd) && !GearAssigner.ProjectIsExempt(rpd) && Base.InLockedTechRange(rpd) && (!rpd.IsFinished || ArcaneTechnologySettings.evenResearched);

    public static bool IsResearchLocked(ThingDef thingDef, Pawn pawn = null)
    {
      ResearchProjectDef rpd;
      return (pawn == null || pawn.IsColonist) && Base.thingDic.TryGetValue(thingDef, out rpd) && Base.Locked(rpd);
    }

    public static TechLevel GetPlayerTech()
    {
      if (ArcaneTechnologySettings.useHighestResearched)
      {
        for (int key = 7; key > 0; --key)
        {
          if (Base.strataDic.ContainsKey((TechLevel) key))
          {
            foreach (ResearchProjectDef researchProjectDef in Base.strataDic[(TechLevel) key])
            {
              if (researchProjectDef.IsFinished)
                return (TechLevel) key;
            }
          }
        }
        return TechLevel.Animal;
      }
      if (!ArcaneTechnologySettings.usePercentResearched)
        return Faction.OfPlayer.def.techLevel;
      int num = 0;
      for (int key = 7; key > 0; --key)
      {
        if (Base.strataDic.ContainsKey((TechLevel) key))
        {
          foreach (ResearchProjectDef researchProjectDef in Base.strataDic[(TechLevel) key])
          {
            if (researchProjectDef.IsFinished)
              ++num;
          }
          if ((double) num / (double) Base.strataDic[(TechLevel) key].Count >= (double) ArcaneTechnologySettings.percentResearchNeeded)
            return (TechLevel) key;
        }
      }
      return TechLevel.Animal;
    }
  }
}
