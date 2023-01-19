using CloveceNezlobSe.Models.Figurky;
using CloveceNezlobSe.Services;

namespace CloveceNezlobSe.Models;

public class Upgrade : Policko
{
    public Upgrade(HerniPlan herniPlan, bool dovolitViceFigurek = false) : base(herniPlan, dovolitViceFigurek)
    {
    }

    public override void PolozFigurku(Figurka figurka)
    {
        var tank = figurka is not Tank ? new Tank(figurka.Hrac, figurka.OznaceniFigurky) : figurka;
        base.PolozFigurku(tank);
        figurka.Hrac.ZamenFigurku(figurka, tank);
    }

    public override void Vykresli(IWriter writer)
    {
		//var originalColor = Console.ForegroundColor;
		//Console.ForegroundColor = ConsoleColor.Magenta;
		writer.Write("[U(");
        foreach (var figurka in figurkyNaPolicku)
        {
            Console.Write(figurka.OznaceniFigurky);
        }
		writer.Write(")]");
        //Console.ForegroundColor = originalColor;
    }
}