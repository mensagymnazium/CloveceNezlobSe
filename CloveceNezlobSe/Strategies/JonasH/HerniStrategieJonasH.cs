using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;
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

		public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
		{
			foreach (var figurka in hrac.Figurky)
			{
				var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
				if (cilovePolicko != null)
				{
					if (cilovePolicko.JeDomecek)
					{
						return new HerniRozhodnuti() { Figurka = figurka };

					}

					else if (!cilovePolicko.JeDomecek && cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any())
					{
						if (cilovePolicko.ZjistiFigurkyHrace(hrac) != null)
						{
							return new HerniRozhodnuti() { Figurka = figurka };
						}
						else
						{
							var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
							var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => hra.HerniPlan.MuzuTahnout(figurka, hod));
							return new HerniRozhodnuti() { Figurka = figurkyKtereMuzuHrat.First() };
						}

					}
				}//Udělat aby se stejné figurky nemohly navzájem vyhodit
			}

			return base.DejHerniRozhodnuti(hrac, hod, informace);
		}
	}
}
