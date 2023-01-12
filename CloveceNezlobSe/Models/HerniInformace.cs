using CloveceNezlobSe.Models.Figurky;

namespace CloveceNezlobSe.Models;

/// <summary>
/// Třída umožňuje přístup k nynějšímu stavu hry, který může hráč využít pro tvorbu jeho herní 
/// </summary>
public class HerniInformace : IHerniInformace
{
    private readonly HerniPlan herniPlan;

    public IReadOnlyList<Policko> Policka { get; init; }
    public int MaximalniPocetHracu { get; init; }
    public IKostka Kostka { get; init; }
    public IReadOnlyList<Hrac> Hraci { get; init; }

    public HerniInformace(HerniPlan herniPlan, Hra hra)
    {
        this.herniPlan = herniPlan;
        MaximalniPocetHracu = herniPlan.MaximalniPocetHracu;
        Policka = herniPlan.Policka;
        Kostka = hra.Kostka;
        Hraci = hra.Hraci;
    }


    public Policko? ZjistiCilovePolicko(Figurka figurka, int hod)
        => herniPlan.ZjistiCilovePolicko(figurka, hod);

    public bool MuzuTahnout(Figurka figurka, int hod)
        => herniPlan.MuzuTahnout(figurka, hod);

}