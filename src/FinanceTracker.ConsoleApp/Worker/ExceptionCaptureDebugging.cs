using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.ConsoleApp.Worker
{
    internal class ExceptionCaptureDebugging
    {
        public void Run()
        {
			try
			{
				Console.WriteLine("Start");
				throw new Exception("error");
			}
			catch (Exception ex)
			{
                Console.WriteLine("捕获到异常: " + ex.Message);
            }
			finally
			{
				Console.WriteLine("End");
			}
        }
    }
}
