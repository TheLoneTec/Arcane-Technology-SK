// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.GearAssigner
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using System.Collections.Generic;
using Verse;

namespace DArcaneTechnology
{
  public static class GearAssigner
  {
    public static HashSet<string> exemptProjects = new HashSet<string>();
    public static Dictionary<string, string> overrideAssignment = new Dictionary<string, string>();
    public static Dictionary<string, string> hardAssignment = new Dictionary<string, string>();

    public static void HardAssign(
      ref Dictionary<ThingDef, ResearchProjectDef> thingDic,
      ref Dictionary<ResearchProjectDef, List<ThingDef>> researchDic)
    {
      foreach (string key in GearAssigner.hardAssignment.Keys)
      {
        if (!GearAssigner.overrideAssignment.ContainsKey(key))
        {
          ThingDef namedSilentFail1 = DefDatabase<ThingDef>.GetNamedSilentFail(key);
          if (namedSilentFail1 != null)
          {
            ResearchProjectDef namedSilentFail2 = DefDatabase<ResearchProjectDef>.GetNamedSilentFail(GearAssigner.hardAssignment[key]);
            if (namedSilentFail2 != null)
            {
              if (namedSilentFail1.GetCompProperties<CompProperties_DArcane>() == null)
                namedSilentFail1.comps.Add((CompProperties) new CompProperties_DArcane(namedSilentFail2));
              if (!thingDic.ContainsKey(namedSilentFail1))
                thingDic.Add(namedSilentFail1, namedSilentFail2);
              if (!researchDic.ContainsKey(namedSilentFail2))
                researchDic.Add(namedSilentFail2, new List<ThingDef>()
                {
                  namedSilentFail1
                });
              else if (!researchDic[namedSilentFail2].Contains(namedSilentFail1))
                researchDic[namedSilentFail2].Add(namedSilentFail1);
            }
          }
        }
      }
    }

    public static void OverrideAssign(
      ref Dictionary<ThingDef, ResearchProjectDef> thingDic,
      ref Dictionary<ResearchProjectDef, List<ThingDef>> researchDic)
    {
      foreach (string key in GearAssigner.overrideAssignment.Keys)
      {
        ThingDef namedSilentFail1 = DefDatabase<ThingDef>.GetNamedSilentFail(key);
        if (namedSilentFail1 != null)
        {
          ResearchProjectDef namedSilentFail2 = DefDatabase<ResearchProjectDef>.GetNamedSilentFail(GearAssigner.overrideAssignment[key]);
          if (namedSilentFail2 != null)
          {
            if (namedSilentFail1.GetCompProperties<CompProperties_DArcane>() == null)
              namedSilentFail1.comps.Add((CompProperties) new CompProperties_DArcane(namedSilentFail2));
            thingDic.SetOrAdd<ThingDef, ResearchProjectDef>(namedSilentFail1, namedSilentFail2);
            if (!researchDic.ContainsKey(namedSilentFail2))
              researchDic.Add(namedSilentFail2, new List<ThingDef>()
              {
                namedSilentFail1
              });
            else if (!researchDic[namedSilentFail2].Contains(namedSilentFail1))
              researchDic[namedSilentFail2].Add(namedSilentFail1);
          }
        }
      }
    }

    public static bool GetOverrideAssignment(ThingDef thing, out ResearchProjectDef rpd)
    {
      string defName;
      if (GearAssigner.overrideAssignment.TryGetValue(thing.defName, out defName))
      {
        if (defName == "None")
        {
          rpd = (ResearchProjectDef) null;
          return true;
        }
        rpd = DefDatabase<ResearchProjectDef>.GetNamedSilentFail(defName);
        if (rpd != null)
          return true;
      }
      rpd = (ResearchProjectDef) null;
      return false;
    }

    public static bool ProjectIsExempt(ResearchProjectDef rpd) => ArcaneTechnologySettings.exemptClothing && GearAssigner.exemptProjects.Contains(rpd.defName);
  }
}
