using FinanceTracker.ConsoleApp.Worker;

namespace FinanceTracker.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            ExceptionCaptureDebugging data = new ();
            data.Run();
            Console.WriteLine("Main");
        }
    }
}
