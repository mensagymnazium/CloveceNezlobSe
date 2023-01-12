using CloveceNezlobSe.Services;

namespace CloveceNezlobSe.Models
{
	public class TurnajTrojher : TurnajBase
	{
		public override void Start()
		{
			var celkoveVysledky = herniStrategie.ToDictionary(i => i.Name, i => new TurnajovyVysledek());

			for (int i = 0; i < herniStrategie.Count; i++)
			{
				for (int j = i + 1; j < herniStrategie.Count; j++)
				{
					for (int k = j + 1; k < herniStrategie.Count; k++)
					{
						var strategie1 = herniStrategie[i];
						var strategie2 = herniStrategie[j];
						var strategie3 = herniStrategie[k];

						var tester = new HerniStrategieTester(this.DisableConsoleDuringGame);
						var hraActivator = () =>
						{
							var hra = new Hra(new LinearniHerniPlan(this.VelikostHernihoPlanu));
							hra.PridejHrace(new Hrac(strategie1.Name, strategie1.Activator(hra)));
							hra.PridejHrace(new Hrac(strategie2.Name, strategie2.Activator(hra)));
							hra.PridejHrace(new Hrac(strategie3.Name, strategie3.Activator(hra)));
							return hra;
						};

						var vysledky = tester.Test(this.PocetHer, hraActivator);
						var vitez = vysledky.OrderByDescending(i => i.Value).First();

						celkoveVysledky[vitez.Key].PocetVitezstvi++;
						foreach (var vysledek in vysledky)
						{
							celkoveVysledky[vysledek.Key].VyhranychHer =
								celkoveVysledky[vysledek.Key].VyhranychHer + vysledek.Value;
						}
					}
				}
			}

			Console.WriteLine("CELKOVÉ VÝSLEDKY:");
			foreach (var vysledek in celkoveVysledky.OrderByDescending(i => i.Value.PocetVitezstvi).ThenByDescending(i => i.Value.VyhranychHer))
			{
				Console.WriteLine($"\t{vysledek.Key}: {vysledek.Value.PocetVitezstvi} vítězství ({vysledek.Value.VyhranychHer} vyhraných her)");
			}
		}
	}
}
