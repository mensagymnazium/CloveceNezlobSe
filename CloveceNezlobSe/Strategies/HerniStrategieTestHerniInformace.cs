using System.Diagnostics;
using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;

namespace CloveceNezlobSe.Strategies;

public class HerniStrategieTestHerniInformace : HerniStrategie
{
    public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
    {

        var figurkaNaHrani = hrac.Figurky.First(x => informace.MuzuTahnout(x, hod));
        if(informace.ZjistiCilovePolicko(figurkaNaHrani,hod) is Zumpa)
        {
            //Nechci do žumpy :(
            Debugger.Break();
        }
        return new HerniRozhodnuti() {Figurka = hrac.Figurky.First()};
    }
}