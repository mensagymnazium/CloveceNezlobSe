using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Models.Figurky
{
	public class Tank : Figurka
	{
		public Tank(Hrac hrac, string oznaceniFigurky) : base(hrac, oznaceniFigurky)
		{
			this.OznaceniFigurky = oznaceniFigurky + "T";
			Detectable = false;
		}
	}
}
