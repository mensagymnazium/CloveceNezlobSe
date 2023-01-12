namespace CloveceNezlobSe.Models;

public class Zumpa : Policko
{
    public Zumpa(HerniPlan herniPlan, bool dovolitViceFigurek = false) : base(herniPlan, dovolitViceFigurek)
    {
    }

	// TODO Je potřeba přesunout logiku do herního plánu.
	//public override void PolozFigurku(Figurka figurka, int hod)
 //   {
 //       herniPlan.DejFigurkuNaStartovniPolicko(figurka);
 //   }

    public override void Vykresli()
    {
        Console.Write("< Žumpa >");
    }
}