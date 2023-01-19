using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;
using CloveceNezlobSe.Models.Figurky;

namespace CloveceNezlobSe.Strategies.MartinT
{
	public class HerniStrategieMartinT : HerniStrategie
	{
		protected readonly Hra hra;

		public HerniStrategieMartinT(Hra hra)
		{
			this.hra = hra;
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
						// pokud je cílové políčko domeček tak se přesunu do toho domečku
						return new HerniRozhodnuti() { Figurka = figurka };
					}
					if ((cilovePolicko is not Domecek) && cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any())
					{
						// na cílovém políčku je figurka protihráče, která by se dala vyhodit
						// proto vyberu příslušnou svoji figurku
						return new HerniRozhodnuti() { Figurka = figurka };
					}
				}
			}

			var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
			var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => hra.HerniPlan.MuzuTahnout(figurka, hod));
			var figurkyKtereChciHrat = figurkyKtereMuzuHrat.Where(figurka => !hra.HerniPlan.ZjistiCilovePolicko(figurka, hod).ZjistiFigurkyHrace(hrac).Any());
			if (figurkyKtereChciHrat.Any())
			{
				foreach (var figurka in figurkyKtereChciHrat)
				{
					bool touhle = true;
					for (int i = 1; i <= hod; i++)
					{
						var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, i);
						if (cilovePolicko != null)
						{
							if ((cilovePolicko is not Domecek) && cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any())
							{
								touhle = false;
							}
						}
					}
					if (touhle == true)
					{
						// pokud bych tahem předběhl figurku protihráče tak ten tah radši neudělám
						return new HerniRozhodnuti() { Figurka = figurka };
					}
				}
				// pokud jsem nevymyslel nic lepšího tak se alespoň první z mých fihurek přiblížím cíli
				return new HerniRozhodnuti() { Figurka = figurkyKtereChciHrat.First() };
			}

			return null;
		}
	}
}
