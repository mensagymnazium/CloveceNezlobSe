using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Services
{
	public class ConsoleWriter : IWriter
	{
		public ConsoleColor GetBackgroundColor()
		{
			return Console.BackgroundColor;
		}

		public ConsoleColor GetForegroundColor()
		{
			return Console.ForegroundColor;
		}

		public void SetBackgroundColor(ConsoleColor color)
		{
			Console.BackgroundColor = color;
		}

		public void SetForegroundColor(ConsoleColor color)
		{
			Console.ForegroundColor = color;
		}

		public void Write(string v)
		{
			Console.Write(v);
		}

		public void WriteLine(string? v = null)
		{
			Console.WriteLine(v);
		}
	}
}
