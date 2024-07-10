namespace PccOnboarding.Utils;

static class LogFile
{
    public static void Write(string message)
    {
        StreamWriter file = new StreamWriter(@"C:\Users\MosheYehudaSznicer\Desktop\PccOnboardingLog.txt", true);
        file.WriteLine(message);
        Console.WriteLine(message);
        file.Close();
    }
    public static void WriteWithBreak(string message)
    {
        StreamWriter file = new StreamWriter(@"C:\Users\MosheYehudaSznicer\Desktop\PccOnboardingLog.txt", true);
        file.WriteLine(message);
        file.WriteLine("-----------------------------------------------------");
        Console.WriteLine(message);
        Console.WriteLine("-----------------------------------------------------");
        file.Close();
    }
    public static void BreakLine()
    {
        StreamWriter file = new StreamWriter(@"C:\Users\MosheYehudaSznicer\Desktop\PccOnboardingLog.txt", true);
        file.WriteLine("-----------------------------------------------------");
        Console.WriteLine("-----------------------------------------------------");
        file.Close();
    }
}
