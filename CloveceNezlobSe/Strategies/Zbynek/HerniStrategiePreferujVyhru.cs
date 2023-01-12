using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;
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

		public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
		{
			foreach (var figurka in hrac.Figurky)
			{
				var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
				if (cilovePolicko != null)
				{
					if (cilovePolicko is Domecek)
					{
						return new HerniRozhodnuti() { Figurka = figurka };
					}
				}
			}
			foreach (var figurka in hrac.Figurky)
			{
				var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
				if (cilovePolicko != null)
				{
					if ((cilovePolicko is not Domecek) && cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any())
					{
						// na cílovém políčku je figurka protihráče, která by se dala vyhodit
						// proto vyberu příslušnou svoji figurku
						return new HerniRozhodnuti() { Figurka = figurka };
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
					return new HerniRozhodnuti() { Figurka = figurkyKtereMuzuHrat.First() };
				}
			}
			return null;
		}
	}
}