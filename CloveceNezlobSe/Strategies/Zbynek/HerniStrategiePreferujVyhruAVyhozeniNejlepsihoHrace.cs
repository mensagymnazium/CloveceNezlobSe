using CloveceNezlobSe.Models;
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
		public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
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

			return base.DejFigurkuKterouHrat(hrac, hod);
		}
	}

}