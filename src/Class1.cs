using System;

namespace ezx6tools
{
	public class Svc1
	{
		public Svc1()
		{
		}
		public async Task Run()
        {
            while (true)
            {
                Console.WriteLine("Svc1 is running..."); // placeholder
                await Task.Delay(5000);
            }
        }
    }
}
