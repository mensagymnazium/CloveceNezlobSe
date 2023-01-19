using CloveceNezlobSe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Models
{
	internal class TurnajVsech : TurnajBase
	{
		public override void Start()
		{
			var tester = new HerniStrategieTester(this.Writer);

			var hraActivator = () =>
			{
				var hra = new Hra(new LinearniHerniPlan(this.VelikostHernihoPlanu, this.Writer), this.Writer);

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
