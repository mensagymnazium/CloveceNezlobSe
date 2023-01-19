using CloveceNezlobSe.Models.Figurky;

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

    public override void Vykresli()
    {
        //var originalColor = Console.ForegroundColor;
        //Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("[U(");
        foreach (var figurka in figurkyNaPolicku)
        {
            Console.Write(figurka.OznaceniFigurky);
        }
        Console.Write(")]");
        //Console.ForegroundColor = originalColor;
    }
}