using CloveceNezlobSe.Models;

namespace CloveceNezlobSe.Strategies.KlaraF;

public class HerniStrategieDomecekVyhazujPrvniKlaraF : HerniStrategieTahniPrvniMoznouFigurkou
{
	public HerniStrategieDomecekVyhazujPrvniKlaraF(Hra hra) : base(hra)
	{
	}

	public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
	{
		foreach (var figurka in hrac.Figurky)
		{
			var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
			if (cilovePolicko != null)
			{
				if ((!cilovePolicko.JeDomecek && cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any()) || cilovePolicko.JeDomecek)
				{
					return figurka;
				}
				else
				{
					return hrac.Figurky.First();
				}
			}
		}

		return base.DejFigurkuKterouHrat(hrac, hod);
	}
}