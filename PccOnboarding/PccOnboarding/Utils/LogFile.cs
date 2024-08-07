namespace PccOnboarding.Utils;

static class LogFile
{
    private static string _basePath = @"C:\Users\MosheYehudaSznicer\Desktop\Work\Logs\PccOnboardinglogs";
    private static string _currentPath = "";

    private static string _date = DateTime.Now.ToString("yyyy-MM-dd_HH-mm");

    public static void SetPath(string state, string OrgUid, int facId)
    {
        Console.WriteLine(_date);
        _currentPath = $"{_basePath}\\{state}_{OrgUid}_{facId}_{_date}.txt";
    }
    public static void Write(string message)
    {
        StreamWriter file = new StreamWriter(_currentPath, true);
        file.WriteLine(message);
        Console.WriteLine(message);
        file.Close();
    }
    public static void WriteWithBreak(string message)
    {
        StreamWriter file = new StreamWriter(_currentPath, true);
        file.WriteLine(message);
        file.WriteLine("-----------------------------------------------------");
        Console.WriteLine(message);
        Console.WriteLine("-----------------------------------------------------");
        file.Close();
    }
    public static void BreakLine()
    {
        StreamWriter file = new StreamWriter(_currentPath, true);
        file.WriteLine("-----------------------------------------------------");
        Console.WriteLine("-----------------------------------------------------");
        file.Close();
    }
}
