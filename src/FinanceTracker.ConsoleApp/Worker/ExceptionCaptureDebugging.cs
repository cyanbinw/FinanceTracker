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
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				Console.WriteLine("End");
			}
        }
    }
}
