using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Models.Figurky
{
	public class Figurka
	{
		public string OznaceniFigurky { get; protected set; }
		public Hrac Hrac { get; protected set; }
		public Policko? Policko { get; protected set; }

		public bool detectable { get; protected set; }

		public Figurka(Hrac hrac, string oznaceniFigurky)
		{
			this.Hrac = hrac;
			this.OznaceniFigurky = oznaceniFigurky;
			detectable = true;
		}

		public void NastavPolicko(Policko? policko)
		{
			this.Policko = policko;
		}

		public bool JeVDomecku()
		{
			return (this.Policko != null) && (this.Policko is Domecek);
		}
	}
}
