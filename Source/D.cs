// Decompiled with JetBrains decompiler
// Type: DArcaneTechnology.D
// Assembly: Arcane Technology, Version=1.4.0.3, Culture=neutral, PublicKeyToken=null
// MVID: 18B45D7F-D96F-435C-A827-0518647BFEDB
// Assembly location: E:\SteamLibrary\steamapps\workshop\content\294100\2554469600\1.4\Assemblies\Arcane Technology.dll

using System;
using System.Collections.Generic;
using Verse;

namespace DArcaneTechnology
{
  internal static class D
  {
    private static bool verbose = true;

    public static void Text(string line, int depth = 0) => Log.Message(line);

    public static void Debug(string title) => D.Text("ArcaneTechnology: " + title);

    public static void Verbose(string title)
    {
      if (!D.verbose)
        return;
      D.Text("ArcaneTechnology: " + title);
    }

    public static void List<T>(string title, IEnumerable<T> list)
    {
      if (!D.verbose)
        return;
      D.Text("ArcaneTechnology: ===== LIST - " + title + " =====");
      try
      {
        foreach (T obj in list)
          D.Text("     " + obj?.ToString());
      }
      catch (Exception ex)
      {
        D.Text("ArcaneTechnology: Error printing list: " + ex.Message);
      }
    }
  }
}
