using System;
using System.IO;
using System.Text;

public class Program {
    public static void Main() {
        var text = File.ReadAllText(""WordQuest\\BUS\\LevelBUS.cs"", Encoding.UTF8);
        byte[] bytes = Encoding.GetEncoding(1252).GetBytes(text);
        string fixedText = Encoding.UTF8.GetString(bytes);
        File.WriteAllText(""test_fixed.cs"", fixedText, Encoding.UTF8);
    }
}
