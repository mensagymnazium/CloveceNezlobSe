using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Models
{
	public class StartovniPolicko : Policko
	{
        public StartovniPolicko(HerniPlan herniPlan, bool dovolitViceFigurek = true) : base(herniPlan, dovolitViceFigurek)
        {
        }
    }
}
