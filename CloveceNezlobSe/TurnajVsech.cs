﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe
{
	internal class TurnajVsech : TurnajBase
	{
		public override void Start()
		{
			var tester = new HerniStrategieTester();

			var hraActivator = () =>
			{
				var hra = new Hra(new LinearniHerniPlan(this.VelikostHernihoPlanu));

				foreach (var item in herniStrategie)
				{
					hra.PridejHrace(new Hrac(item.Name, item.Activator(hra)));
				}

				return hra;
			};

			tester.Test(this.PocetHer, hraActivator);
		}
	}
}
