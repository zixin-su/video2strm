// See https://aka.ms/new-console-template for more information

public class Entry
{
    private static readonly HashSet<string> VIDEO_FILE_SUFFIXS =
    [
        "3gp", "asf", "avi", "divx", "dv", "f4v", "flv", "m2ts", "m4v", "mkv", "mov", "mp4", "mpeg", "mpg", "ogv", "rm",
        "rmvb", "swf", "ts", "vob", "webm", "wmv", "iso"
    ];

    public static void Main(string[] args)
    {
        // args = new string[]
        // {
        //     @"\\192.168.31.210\WinNas\Cloud\CloudDrive\115open\Test\3bodySrc",
        //     @"\\192.168.31.210\WinNas\Cloud\CloudDrive\115open\Test\3bodyStrm",
        //     @"\\192.168.31.210\WinNas\Cloud\CloudDrive\115open\Test\3bodyVideo"
        // };

        var rootDir = @"\\192.168.31.210\重要数据池\VideoLibrary";
        var files = Directory.GetFiles(rootDir, "*.strm", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var str = File.ReadAllLines(file).FirstOrDefault();
            str = str.Replace(@"F:\Cloud", @"\\192.168.31.210\WinNas\Cloud");
            File.WriteAllLines(file, new[] { str });
        }

        Console.WriteLine(files.Length);
    }
}