using STAL.Core;
using System;
using System.Diagnostics;

namespace STALExample
{
	class Program
	{
		static void Main(string[] args)
		{
			Engine engine = new Engine();
			Stopwatch timer = Stopwatch.StartNew();
			engine.LoadFile("Test1.STAL");
			timer.Stop();
			Console.WriteLine(timer.ElapsedMilliseconds);
			Console.ReadKey();
		}
	}
}
