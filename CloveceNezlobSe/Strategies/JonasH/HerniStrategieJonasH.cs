using CloveceNezlobSe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Strategies.JonasH
{
	public class HerniStrategieJonasH : HerniStrategieTahniPrvniMoznouFigurkou
	{
		public HerniStrategieJonasH(Hra hra) : base(hra)
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

					else if (!cilovePolicko.JeDomecek && cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any())
					{
						if (cilovePolicko.ZjistiFigurkyHrace(hrac) != null)
						{
							return figurka;
						}
						else
						{
							var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
							var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => hra.HerniPlan.MuzuTahnout(figurka, hod));
							return figurkyKtereMuzuHrat.First();
						}

					}
				}//Udělat aby se stejné figurky nemohly navzájem vyhodit
			}

			return base.DejFigurkuKterouHrat(hrac, hod);
		}
	}
}
