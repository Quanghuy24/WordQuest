using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text.Json;

class Program {
    static void Main() {
        var files = Directory.GetFiles(".", "*.cs", SearchOption.AllDirectories);
        var badLines = new HashSet<string>();
        foreach(var f in files) {
            if(f.Contains("\\obj\\") || f.Contains("\\bin\\")) continue;
            var text = File.ReadAllText(f);
            var matches = Regex.Matches(text, "\"[^\"]*\"");
            foreach(Match m in matches) {
                if (m.Value.Contains("Ã") || m.Value.Contains("Ä") || m.Value.Contains("á") || m.Value.Contains("»") || m.Value.Contains("£") || m.Value.Contains("¿") || m.Value.Contains("¡") || m.Value.Contains("¢")) {
                    badLines.Add(m.Value);
                }
            }
        }
        File.WriteAllText("FixEncoding\\bad_strings.json", JsonSerializer.Serialize(badLines, new JsonSerializerOptions { WriteIndented = true }));
    }
}
