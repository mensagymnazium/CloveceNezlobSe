using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;

namespace CloveceNezlobSe.Strategies.VojtaT
{
	public class HerniStrategieVojtechTvrdik : HerniStrategie
	{
		protected readonly Hra hra;
		public HerniStrategieVojtechTvrdik(Hra hra)
		{
			this.hra = hra;
		}
		public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
		{
			var dohratelneFigurky = hrac.Figurky.Where(f => hra.HerniPlan.ZjistiCilovePolicko(f, hod) != null && hra.HerniPlan.ZjistiCilovePolicko(f, hod).JeDomecek);
			if (dohratelneFigurky.Count() > 0) return new HerniRozhodnuti() { Figurka = dohratelneFigurky.First() };
			var agresivniFigurky = hrac.Figurky.Where(f => hra.HerniPlan.ZjistiCilovePolicko(f, hod) != null && hra.HerniPlan.ZjistiCilovePolicko(f, hod).ZjistiFigurkyProtihracu(hrac).Count() > 0);
			if (agresivniFigurky.Count() > 0) return new HerniRozhodnuti() { Figurka = agresivniFigurky.First() };
			var ohrozeneFigurky = DejOhrozeneFigurky(hrac, hod);
			var hratelneFigurky = DejHratelneFigurky(hrac, hod);
			var figurky = ohrozeneFigurky.Count > 0 ? ohrozeneFigurky : hratelneFigurky;
			if (figurky.Count == 0) return null;
			if (figurky.Count == 1) return new HerniRozhodnuti() { Figurka = figurky.First().Key };
			return new HerniRozhodnuti() { Figurka = figurky.First().Key };
		}
		public Dictionary<Figurka, Policko> DejOhrozeneFigurky(Hrac hrac, int hod)
		{
			Dictionary<Figurka, Policko> figurky = new();
			foreach (var figurka in hrac.Figurky)
			{
				var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
				if (cilovePolicko == null || (cilovePolicko.ZjistiFigurkyHrace(hrac).Count() > 0 && !cilovePolicko.JeDomecek) || (hra.HerniPlan.ZjistiCilovePolicko(figurka, -1) == hra.HerniPlan.ZjistiCilovePolicko(figurka, -0)))
				{
					continue;
				}
				for (int i = 1; i < 7; i++)
				{
					if (hra.HerniPlan.ZjistiCilovePolicko(figurka, -i).ZjistiFigurkyProtihracu(hrac).Count() > 0)
					{
						figurky.Add(figurka, cilovePolicko);
						break;
					}
				}
			}
			return figurky;
		}
		public Dictionary<Figurka, Policko> DejHratelneFigurky(Hrac hrac, int hod)
		{
			Dictionary<Figurka, Policko> figurky = new();
			foreach (var figurka in hrac.Figurky)
			{
				var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
				if (cilovePolicko != null && (cilovePolicko.ZjistiFigurkyHrace(hrac).Count() == 0 || cilovePolicko.JeDomecek))
				{
					figurky.Add(figurka, cilovePolicko);
				}
			}
			return figurky;
		}
	}
}