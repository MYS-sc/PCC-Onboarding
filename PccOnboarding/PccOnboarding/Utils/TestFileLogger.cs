namespace PccOnboarding;

public static class TestFileLogger
{
    public static string _basePath = @"C:\Users\MosheYehudaSznicer\Desktop\Work\Logs\PccOnboardingTestLogs";
    public static string _currentPath = "";

    public static string _date = DateTime.Now.ToString("yyyy-MM-dd_HH-mm");

    public static void SetPath(string state, string OrgUid, int facId)
    {
        _currentPath = $"{_basePath}\\{state}_{OrgUid}_{facId}_{_date}.json";
    }

    public static void Write(string message)
    {
        StreamWriter file = new StreamWriter(_currentPath, true);
        file.WriteLine(message);
        file.Close();
    }
}
