using CloveceNezlobSe.Models.Figurky;

namespace CloveceNezlobSe.Models;

public interface IHerniInformace
{
    public IReadOnlyList<Policko> Policka { get; init; }
    public Policko? ZjistiCilovePolicko(Figurka figurka, int hod);
    public bool MuzuTahnout(Figurka figurka, int hod);
    public int MaximalniPocetHracu { get; init; }
    public IKostka Kostka { get; init; }
    public IReadOnlyList<Hrac> Hraci { get; init; }
}