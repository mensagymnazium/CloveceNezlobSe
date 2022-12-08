using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe
{
	public class TurnajStrategii
	{
		public int PocetHer { get; init; } = 5000;
		public int VelikostHernihoPlanu { get; init; } = 40;

		private List<HerniStrategieDescriptor> herniStrategie = new();

		public void PridejStrategii(HerniStrategieDescriptor strategieDescriptor)
		{
			herniStrategie.Add(strategieDescriptor);
		}

		public void Start()
		{
			var celkoveVysledky = herniStrategie.ToDictionary(i => i.Name, i => new TurnajovyVysledek());

			for (int i = 0; i < herniStrategie.Count; i++)
			{
				for (int j = i + 1; j < herniStrategie.Count; j++)
				{
					var strategie1 = herniStrategie[i];
					var strategie2 = herniStrategie[j];

					var tester = new HerniStrategieTester();
					var prefabrikatHry = new Hra(new LinearniHerniPlan(this.VelikostHernihoPlanu));
					prefabrikatHry.PridejHrace(new Hrac(strategie1.Name, strategie1.Activator(prefabrikatHry)));
					prefabrikatHry.PridejHrace(new Hrac(strategie2.Name, strategie2.Activator(prefabrikatHry)));

					var vysledky = tester.Test(this.PocetHer, prefabrikatHry);
					var vitez = vysledky.OrderByDescending(i => i.Value).First();

					celkoveVysledky[vitez.Key].PocetVitezstvi++;
					foreach (var vysledek in vysledky)
					{
						celkoveVysledky[vysledek.Key].VyhranychHer = celkoveVysledky[vysledek.Key].VyhranychHer + vysledek.Value;
					}					
				}
			}

			Console.WriteLine("CELKOVÉ VÝSLEDKY:");
			foreach (var vysledek in celkoveVysledky.OrderByDescending(i => i.Value.PocetVitezstvi).ThenByDescending(i => i.Value.VyhranychHer))
			{
				Console.WriteLine($"\t{vysledek.Key}: {vysledek.Value.PocetVitezstvi} vítězství ({vysledek.Value.VyhranychHer} vyhraných her)");
			}
		}

		public class TurnajovyVysledek
		{
			public int PocetVitezstvi { get; set; }
			public int VyhranychHer { get; set; }
		}
	}
}
