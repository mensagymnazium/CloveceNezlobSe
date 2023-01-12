using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloveceNezlobSe.Models.Things
{
    public abstract class Vec
    {
        public Vec(HerniPlan herniPlan)
        {
            HerniPlan = herniPlan;
            PridejNaHerniPlan();
        }

        public HerniPlan HerniPlan { get; set; }

        public Figurka? Vlastnik { get; set; } = null;

        private void PridejNaHerniPlan()
        {
            var vyber = new List<Policko>();

            foreach (var pol in HerniPlan.Policka)
            {
                if (pol.JeVolneProVec())
                {
                    vyber.Add(pol);
                }
            }

            var ind = Random.Shared.Next(vyber.Count);
            Policko pole = vyber[ind];
            pole.PridejVec(this);
        }

        public virtual void NastavVlastnika(Figurka fig)
        {
            Vlastnik = fig;
        }

        public abstract void Pouzij();
    }
}
