using CloveceNezlobSe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Strategies.Zbynek
{
	internal class HerniStrategiePreferujVyhru : HerniStrategieTahniPrvniMoznouFigurkou
	{
		public HerniStrategiePreferujVyhru(Hra hra) : base(hra)
		{
		}

		public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
		{
			foreach (var figurka in hrac.Figurky)
			{
				var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
				if (cilovePolicko != null)
				{
					if (cilovePolicko.JeDomecek)
					{
						return figurka;
					}
				}
			}
			foreach (var figurka in hrac.Figurky)
			{
				var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
				if (cilovePolicko != null)
				{
					if (!cilovePolicko.JeDomecek && cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any())
					{
						// na cílovém políčku je figurka protihráče, která by se dala vyhodit
						// proto vyberu příslušnou svoji figurku
						return figurka;
					}
				}
			}
			foreach (var figurka in hrac.Figurky)
			{

				var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
				var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
				var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => hra.HerniPlan.MuzuTahnout(figurka, hod) && cilovePolicko.ZjistiFigurkyHrace(hrac).Any());
				if (figurkyKtereMuzuHrat.Any())
				{
					return figurkyKtereMuzuHrat.First();
				}
			}
			return null;
		}
	}
}