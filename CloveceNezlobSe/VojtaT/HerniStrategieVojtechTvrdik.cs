namespace CloveceNezlobSe
{
	public class HerniStrategieVojtechTvrdik : HerniStrategie
	{
		protected readonly Hra hra;
		public HerniStrategieVojtechTvrdik(Hra hra)
		{
			this.hra = hra;
		}
		public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
		{
            Console.WriteLine("Vy jste si mysleli, že tu hru dohrajem? Oops, asi byc měl mít omezenej čas na to, abych domyslel strategii.");
			var i = 0;
            while (true)
            {
				i++;
            }
			return null;
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