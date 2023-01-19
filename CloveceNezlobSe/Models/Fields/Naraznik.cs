using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Models;

public class Naraznik : Policko
{
    public Naraznik(HerniPlan herniPlan, bool dovolitViceFigurek = false) : base(herniPlan, dovolitViceFigurek)
    {
    }
	public override void Vykresli()
	{
		//var originalBackgroundColor = Console.BackgroundColor;
		//Console.BackgroundColor = ConsoleColor.DarkYellow;
		//var originalForegroundColor = Console.ForegroundColor;
		//Console.ForegroundColor = ConsoleColor.Black;
		////Console.ForegroundColor = ConsoleColor.White;
		Console.Write("|");
		foreach (var figurka in figurkyNaPolicku)
		{
			Console.Write(figurka.OznaceniFigurky);
		}
		Console.Write("|");
		//Console.ForegroundColor = originalForegroundColor;
		//Console.BackgroundColor = originalBackgroundColor;
	}
}
