namespace CloveceNezlobSe.Models;

public class InsaneHerniPlan : LinearniHerniPlan
{
    public InsaneHerniPlan()
    {
        policka = new()
        {
            new StartovniPolicko(this),

            new Policko(this),
            new Naraznik(this),
            new Policko(this),
            new Policko(this),
            new Policko(this),
            new Naraznik(this),

            new Zumpa(this),
            new Policko(this),
            new Naraznik(this),
            new Policko(this),
            new Policko(this),
            new Policko(this),

            new Policko(this),
            new Policko(this),
            new Policko(this),
            new Naraznik(this),
            new Policko(this),
            new Policko(this),

            new Zumpa(this),
            new Policko(this),
            new Policko(this),
            new Naraznik(this),
            new Policko(this),
            new Policko(this),

            new Domecek(this)
        };
    }
}