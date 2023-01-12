using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;

namespace CloveceNezlobSe.Strategies.KlaraF;

public class HerniStrategieDomecekVyhazujPrvniKlaraF : HerniStrategieTahniPrvniMoznouFigurkou
{
	public HerniStrategieDomecekVyhazujPrvniKlaraF(Hra hra) : base(hra)
	{
	}

	public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
	{
		foreach (var figurka in hrac.Figurky)
		{
			var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
			if (cilovePolicko != null)
			{
				if ((!cilovePolicko.JeDomecek && cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any()) || cilovePolicko.JeDomecek)
				{
					return new HerniRozhodnuti() { Figurka = figurka };
				}
				else
				{
					return new HerniRozhodnuti() { Figurka = hrac.Figurky.First() };
				}
			}
		}

		return base.DejHerniRozhodnuti(hrac, hod, informace);
	}
}