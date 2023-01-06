using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Models
{
	public class Domecek : Policko
	{
        public Domecek(HerniPlan herniPlan, bool dovolitViceFigurek = true) : base(herniPlan, dovolitViceFigurek)
        {
        }
    }
}
