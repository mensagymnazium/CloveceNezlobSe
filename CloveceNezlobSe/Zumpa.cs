namespace CloveceNezlobSe;

public class Zumpa : Policko
{
    public Zumpa(HerniPlan herniPlan, bool dovolitViceFigurek = false) : base(herniPlan, dovolitViceFigurek)
    {
    }

    public override void PolozFigurku(Figurka figurka)
    {
        herniPlan.DejFigurkuNaStartovniPolicko(figurka);
    }

    public override void Vykresli()
    {
        Console.Write("< Žumpa >:( >");
    }
}