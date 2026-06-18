using System;
using System.Text;

public class Program {
    public static void Main() {
        string badString = "ID chủ đá»  không hợp lệ!";
        // 28591 is iso-8859-1
        byte[] bytes = Encoding.GetEncoding(28591).GetBytes(badString);
        string goodString = Encoding.UTF8.GetString(bytes);
        Console.WriteLine(goodString);
    }
}

