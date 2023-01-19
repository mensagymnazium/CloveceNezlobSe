using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Services
{
	public class NullWriter : IWriter
	{
		public ConsoleColor GetBackgroundColor()
		{
			return default;
		}

		public ConsoleColor GetForegroundColor()
		{
			return default;
		}

		public void SetBackgroundColor(ConsoleColor black)
		{
			// NOOP
		}

		public void SetForegroundColor(ConsoleColor black)
		{
			// NOOP
		}

		public void Write(string v)
		{
			// NOOP
		}

		public void WriteLine(string? v = null)
		{
			// NOOP
		}
	}
}
