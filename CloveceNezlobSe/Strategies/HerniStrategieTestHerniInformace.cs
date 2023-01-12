using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;

namespace CloveceNezlobSe.Strategies;

public class HerniStrategieTestHerniInformace : HerniStrategie
{
    public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
    {
        return new HerniRozhodnuti() {Figurka = hrac.Figurky.First()};
    }
}