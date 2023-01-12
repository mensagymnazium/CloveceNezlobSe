using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Models;

public class Naraznik : Policko
{
    public Naraznik(HerniPlan herniPlan, bool dovolitViceFigurek = false) : base(herniPlan, dovolitViceFigurek)
    {
    }
	
	// TODO Je potřeba přesunout logiku do HerniPlan
	//public override void PolozFigurku(Figurka figurka, int hod)
	//{
	//	int posun = hod / 2 * (-1);
	//	if (posun == 0)
	//	{
	//		base.PolozFigurku(figurka, hod);
	//	}
	//	herniPlan.PosunFigurku(figurka, posun);
	//}
}
