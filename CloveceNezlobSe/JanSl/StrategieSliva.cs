﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe
{
	public class StrategieSliva : HerniStrategie
	{
		protected readonly Hra hra;

		List<Policko> herniPlan;

		public StrategieSliva(Hra hra)
		{
			this.hra = hra;

			this.herniPlan = new List<Policko>();
			foreach (var pole in hra.HerniPlan.Policka) herniPlan.Add(pole);
		}

		public int dejPorady(Figurka figurka)
		{
			return herniPlan.IndexOf(herniPlan.FirstOrDefault(pole => pole.ZjistiFigurkyProtihracu(null).Contains(figurka)));
		}



		public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
		{

			var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).Where(fig => dejPorady(fig) < herniPlan.Count - hod)
				.OrderBy(fig => dejPorady(fig)).ToList();

			var poziceFigurek = figurkyNaCeste.Select(figurka => herniPlan.IndexOf(herniPlan.FirstOrDefault(pole => pole.ZjistiFigurkyProtihracu(null).Contains(figurka))));

			//var parametry = new Dictionary<string, double>();

			//foreach (var pole in herniPlan)
			//         {

			//         }


			if (figurkyNaCeste.Any())
			{
				return figurkyNaCeste.Last();
			}

			return null;
		}
	}
}