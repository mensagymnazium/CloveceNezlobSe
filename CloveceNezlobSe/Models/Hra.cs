namespace CloveceNezlobSe.Models
{
	public class Hra
	{
		public HerniPlan HerniPlan { get; private set; }

		private List<Hrac> vitezove = new();
		public IReadOnlyList<Hrac> Vitezove => vitezove.AsReadOnly();

		List<Hrac> hraci = new();

		public Hra(HerniPlan herniPlan)
		{
			this.HerniPlan = herniPlan;
		}

		public void PridejHrace(Hrac hrac)
		{
			if (hraci.Count == HerniPlan.MaximalniPocetHracu)
			{
				throw new Exception("Hra je plná, maximální počet hráčů herního plánu byl překročen.");
			}
			hraci.Add(hrac);
		}

		public void NastavNahodnePoradiHracu()
		{
			hraci = hraci.OrderBy(hrac => Guid.NewGuid()).ToList();
		}

		public void Start()
		{
			//Aby se hra dala spusti vícekrát
			vitezove.Clear();

			// TODO Kontrola vstupních podmínek pro zahájení hry.
			foreach (var hrac in hraci)
			{
				foreach (var figurka in hrac.Figurky)
				{
					HerniPlan.DejFigurkuNaStartovniPolicko(figurka);
				}
			}

			IKostka kostka = new KostkaUnsigned(pocetSten: 6);

			while (true)
			{
				foreach (var hrac in hraci)
				{
					if (Vitezove.Contains(hrac))
					{
						continue;
					}

					HerniPlan.Vykresli();

					Console.WriteLine($"Hraje hráč {hrac.Jmeno}.");

					HerniPlan.Hraj(hrac, kostka);

					if (hrac.MaVsechnyFigurkyVDomecku())
					{
						vitezove.Add(hrac);
					}
				}

				if (Vitezove.Count > 0)
				{
					Console.WriteLine("Hra skončila.");
					break;
				}
			}

			Console.WriteLine("Výsledky hry:");
			for (int i = 0; i < Vitezove.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {Vitezove[i].Jmeno}");
			}
		}

	}
}
