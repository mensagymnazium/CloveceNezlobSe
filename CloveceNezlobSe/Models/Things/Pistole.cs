using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Models.Things
{
    public class Pistole : Vec
    {
        public Pistole(HerniPlan herniPlan) : base(herniPlan)
        {

        }

        public int Sila { get; private set; } = Random.Shared.Next(1, 4);

        public string Vykresli()
        {
            return "Pis" + Sila;
        }

        public void Pouzij()
        {
            // Vím že tohle je časově neoptimální, ale nchtělo se mi to přepisovat, abych si někde ukládal pozici figurky
            int pozice = -1 * Sila;
            for (int i = 0; i < HerniPlan.Policka.Count; i++)
            {
                if (HerniPlan.Policka[i].ZjistiFigurkyHrace(Vlastnik.Hrac).Contains(Vlastnik)){
                    pozice = i; break;
                }
            }

            int zac = Math.Max(0, pozice - Sila);
            int kon = Math.Min(HerniPlan.Policka.Count - 1, pozice + Sila);

            for (int i = zac; i < kon + 1; i++)
            {
                HerniPlan.VycistiPolicko(HerniPlan.Policka[i]);
            }
        }

    }
}
