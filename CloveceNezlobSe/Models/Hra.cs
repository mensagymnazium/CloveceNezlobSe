namespace CloveceNezlobSe.Models
{
	public class Hra
	{
		public HerniPlan HerniPlan { get; private set; }

		public IReadOnlyList<Hrac> Vitezove => vitezove.AsReadOnly();
		private List<Hrac> vitezove = new();

		private List<Hrac> hraci = new();
        public IReadOnlyList<Hrac> Hraci => hraci.AsReadOnly();

		public IKostka Kostka;

		public Hra(HerniPlan herniPlan)
		{
			this.HerniPlan = herniPlan;
            herniPlan.Hra = this;

            this.HerniPlan.Hra = this;
			this.Kostka = new KostkaUnsigned(pocetSten: 6);
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
			// TODO Kontrola vstupních podmínek pro zahájení hry.
			foreach (var hrac in hraci)
			{
				foreach (var figurka in hrac.Figurky)
				{
					HerniPlan.DejFigurkuNaStartovniPolicko(figurka);
				}
			}

			// Hra končí s prvním vítězným hráčem, úpravou podmínky lze nechat dohrát všechny hráče
			while (!Vitezove.Any())
			{
				foreach (var hrac in hraci)
				{
					if (Vitezove.Contains(hrac))
					{
						continue;
					}

					HerniPlan.Vykresli();

					Console.WriteLine($"Hraje hráč {hrac.Jmeno}.");

					HerniPlan.HrajTahHrace(hrac, Kostka);

					if (hrac.MaVsechnyFigurkyVDomecku())
					{
						vitezove.Add(hrac);
					}
				}
			}
			Console.WriteLine("Hra skončila.");

			Console.WriteLine("Výsledky hry:");
			for (int i = 0; i < Vitezove.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {Vitezove[i].Jmeno}");
			}
		}
	}
}
