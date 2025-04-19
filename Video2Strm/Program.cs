// See https://aka.ms/new-console-template for more information


using System.Diagnostics;

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

        var srcDir = args[0];
        var strmDestDir = args[1];
        var videoDestDir = args[2];

        // var files = Directory.GetFiles(srcDir, "*.*", SearchOption.AllDirectories);
        // var fileList = new List<string>();
        // foreach (var file in files)
        // {
        //     var suffix = Path.GetExtension(file).TrimStart('.').ToLower();
        //     if (VIDEO_FILE_SUFFIXS.Contains(suffix))
        //     {
        //         fileList.Add(file);
        //     }
        // }
        //
        //
        // foreach (var file in fileList)
        // {
        //     var fileName = Path.GetFileName(file);
        //     //替换后缀名
        //     var newFileName = Path.ChangeExtension(fileName, ".strm");
        //     var dir = Path.GetDirectoryName(file).Replace(srcDir, destDir);
        //
        //     var destFile = Path.Combine(dir, newFileName);
        //     if (File.Exists(destFile))
        //         continue;
        //     if (!Directory.Exists(dir))
        //     {
        //         Directory.CreateDirectory(dir);
        //     }
        //
        //     File.WriteAllText(destFile, file);
        //     Console.WriteLine($"create -> {destFile}");
        // }
        HandleFile(srcDir, strmDestDir, videoDestDir, srcDir);
    }

    private static void HandleFile(string srcDir, string strmDestDir, string videoDestDir, string curDir)
    {
        var files = Directory.GetFiles(curDir);
        foreach (var file in files)
        {
            if (!VIDEO_FILE_SUFFIXS.Contains(Path.GetExtension(file).TrimStart('.').ToLower()))
            {
                File.Delete(file);
                continue;
            }

            //替换后缀名
            var strmDestFile = Path.ChangeExtension(file, ".strm").Replace(srcDir, strmDestDir);
            var videoDestFile = file.Replace(srcDir, videoDestDir);

            var dir = Path.GetDirectoryName(strmDestFile);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            dir = Path.GetDirectoryName(videoDestFile);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);


            if (!File.Exists(videoDestFile))
                File.Move(file, videoDestFile);
            File.WriteAllText(strmDestFile, videoDestFile);
            Console.WriteLine($"create -> {strmDestFile}");
        }

        Thread.Sleep(10);

        var subDirs = Directory.GetDirectories(curDir);
        foreach (var subDir in subDirs)
        {
            HandleFile(srcDir, strmDestDir, videoDestDir, subDir);
        }

        if (curDir != srcDir)
            Directory.Delete(curDir, false);
    }
}