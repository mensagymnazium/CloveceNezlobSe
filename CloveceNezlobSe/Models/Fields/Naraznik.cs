using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloveceNezlobSe.Services;

namespace CloveceNezlobSe.Models;

public class Naraznik : Policko
{
    public Naraznik(HerniPlan herniPlan, bool dovolitViceFigurek = false) : base(herniPlan, dovolitViceFigurek)
    {
    }
	public override void Vykresli(IWriter writer)
	{
		ConsoleColor originalBackgroundColor = writer.GetBackgroundColor();
		writer.SetBackgroundColor(ConsoleColor.DarkYellow);
		ConsoleColor originalForegroundColor = writer.GetForegroundColor();
		writer.SetForegroundColor(ConsoleColor.Black);
		writer.Write("[");
		foreach (var figurka in figurkyNaPolicku)
		{
			writer.Write(figurka.OznaceniFigurky);
		}
		writer.Write("]");
		writer.SetForegroundColor(originalForegroundColor);
		writer.SetBackgroundColor(originalBackgroundColor);
	}
}
