using CloveceNezlobSe.Services;

namespace CloveceNezlobSe.Models;

public class InsaneHerniPlan : LinearniHerniPlan
{
    public InsaneHerniPlan(IWriter writer)
        : base(writer)
    {
        policka = new()
        {
            new StartovniPolicko(this),

            new Upgrade(this),
            new Policko(this),
            new Naraznik(this),
            new Policko(this),
            new Policko(this),
            new Naraznik(this),

            new Zumpa(this),
            new Policko(this),
            new Naraznik(this),
            new Policko(this),
            new Policko(this),
            new Upgrade(this),

            new Policko(this),
            new Policko(this),
            new Upgrade(this),
            new Naraznik(this),
            new Policko(this),
            new Policko(this),

            new Zumpa(this),
            new Policko(this),
            new Policko(this),
            new Naraznik(this),
            new Policko(this),
            new Upgrade(this),

            new Domecek(this)
        };
    }
}