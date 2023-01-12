using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Strategies
{
    public class HerniStrategiePreferujVyhazovaniJinakPrvniMoznou : HerniStrategieTahniPrvniMoznouFigurkou
    {
        public HerniStrategiePreferujVyhazovaniJinakPrvniMoznou(Hra hra) : base(hra)
        {
        }

		public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
		{
			foreach (var figurka in hrac.Figurky)
            {
                var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
                if (cilovePolicko != null)
                {
                    if (!(cilovePolicko is Domecek) && cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any())
                    {
                        // na cílovém políčku je figurka protihráče, která by se dala vyhodit
                        // proto vyberu příslušnou svoji figurku
                        return new HerniRozhodnuti() { Figurka = figurka };
                    }
                }
            }

            return base.DejHerniRozhodnuti(hrac, hod, informace);
        }
    }
}
