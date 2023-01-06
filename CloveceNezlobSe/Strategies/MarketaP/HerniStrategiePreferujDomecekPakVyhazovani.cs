using CloveceNezlobSe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Strategies.MarketaP
{
	public class HerniStrategiePreferujDomecekPakVyhazovani : HerniStrategieTahniPrvniMoznouFigurkou
	{
		public HerniStrategiePreferujDomecekPakVyhazovani(Hra hra) : base(hra)
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
						// na cílovém políčku je figurka protihráče, která by se dala vyhodit
						// proto vyberu příslušnou svoji figurku

						return figurka;
					}
				}//Udělat aby se stejné figurky nemohly navzájem vyhodit
			}

			return base.DejFigurkuKterouHrat(hrac, hod);
		}
	}
}
