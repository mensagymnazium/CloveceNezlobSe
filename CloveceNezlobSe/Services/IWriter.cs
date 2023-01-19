using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Services
{
	public interface IWriter
	{
		ConsoleColor GetBackgroundColor();
		ConsoleColor GetForegroundColor();
		void SetBackgroundColor(ConsoleColor black);
		void SetForegroundColor(ConsoleColor black);
		void Write(string v);
		void WriteLine(string? v = null);
	}
}
