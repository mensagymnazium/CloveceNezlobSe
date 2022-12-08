using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CloveceNezlobSe
{
	public class HerniStrategieRisa : HerniStrategie
	{
		private readonly Hra hra;

		public HerniStrategieRisa(Hra hra)
		{
			this.hra = hra;
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
				List<Figurka> toMove = new List<Figurka>();
				if (cilovePolicko != null)
				{
					if (JeOhrozena(figurka) && MuzeUtect(figurka, hod))
					{
						// na cílovém políčku je figurka protihráče, která by se dala vyhodit
						// proto vyberu příslušnou svoji figurku
						toMove.Add(figurka);
					}
					if (toMove.Any())
					{
						for (int i = (hra.HerniPlan.Policka.Count) - 1; i >= 0; i--)
						{
							foreach (Figurka f in toMove)
							{
								if (hra.HerniPlan.Policka[i] == f.Policko)
								{
									return f;
								}
							}
						}
					}
				}
			}

			var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
			var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => hra.HerniPlan.MuzuTahnout(figurka, hod));
			if (figurkyKtereMuzuHrat.Any())
			{
				for (int i = (hra.HerniPlan.Policka.Count) - 1; i >= 0; i--)
				{
					foreach (Figurka f in figurkyKtereMuzuHrat)
					{
						if (hra.HerniPlan.Policka[i] == f.Policko)
						{
							return f;
						}
					}

				}


			}
			return null;
		}

		public bool JeOhrozena(Figurka figurka)
		{
			List<Policko> polickaZa = new List<Policko>();
			for (int i = -1; i > -7; i--)
			{
				var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, i);
				polickaZa.Add(cilovePolicko);
			}
			foreach (Policko policko in polickaZa)
			{
				if (policko.ZjistiFigurkyProtihracu(figurka.Hrac).Count() != 0)
				{
					return true;
				}
			}
			return false;
		}

		public bool MuzeUtect(Figurka figurka, int hod)
		{
			List<Policko> polickaZa = new List<Policko>();
			for (int i = hod - 1; i > hod - 7; i--)
			{
				var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, i);
				if (cilovePolicko != null)
				{
					polickaZa.Add(cilovePolicko);
				}
			}
			foreach (Policko policko in polickaZa)
			{
				if (policko.ZjistiFigurkyProtihracu(figurka.Hrac).Count() != 0)
				{
					return false;
				}
			}
			return true;
		}
	}
}
