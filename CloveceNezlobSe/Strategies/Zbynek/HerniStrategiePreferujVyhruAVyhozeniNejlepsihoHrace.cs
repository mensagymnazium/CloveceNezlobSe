using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Strategies.Zbynek
{
	internal class HerniStrategiePreferujVyhruAVyhozeniNejlepsihoHrace : HerniStrategieTahniPrvniMoznouFigurkou
	{
		public HerniStrategiePreferujVyhruAVyhozeniNejlepsihoHrace(Hra hra) : base(hra)
		{
		}
		public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
		{
			int pocetfigurekvdomecku = 0;
			foreach (var figurka in hrac.Figurky)
			{

				if (figurka.JeVDomecku())
				{
					pocetfigurekvdomecku++;
				}

			}


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

			return base.DejHerniRozhodnuti(hrac, hod, informace);
		}
	}

}