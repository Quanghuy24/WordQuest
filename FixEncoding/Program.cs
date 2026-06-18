using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program {
    static void Main() {
        var files = Directory.GetFiles(".", "*.cs", SearchOption.AllDirectories);
        var badLines = new HashSet<string>();
        foreach(var f in files) {
            if(f.Contains("\\obj\\") || f.Contains("\\bin\\")) continue;
            var lines = File.ReadAllLines(f);
            foreach(var line in lines) {
                if (line.Contains("") || line.Contains("A") || line.Contains("A'") || line.Contains("") || line.Contains("-") || line.Contains("A-") || line.Contains("A3")) {
                    badLines.Add(line.Trim());
                }
            }
        }
        File.WriteAllLines("bad_lines.txt", badLines);
    }
}

