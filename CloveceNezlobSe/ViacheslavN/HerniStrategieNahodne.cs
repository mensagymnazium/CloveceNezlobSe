using System;
using System.Linq;

namespace CloveceNezlobSe
{
    public class HerniStrategieNahodne : HerniStrategie
    {
        private readonly Hra hra;
        
        public HerniStrategieNahodne(Hra hra)
        {
            this.hra = hra;
        }

        public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
        {
            var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
            var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => hra.HerniPlan.MuzuTahnout(figurka, hod)).ToList();
            foreach (Figurka figurka in figurkyKtereMuzuHrat)
            {
                var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
                if (cilovePolicko == null)
                    figurkyKtereMuzuHrat.Remove(figurka);
                else if (cilovePolicko.JeDomecek)
                    return figurka;
            }
            if (figurkyKtereMuzuHrat.Any())
            {
                return figurkyKtereMuzuHrat[Random.Shared.Next(0, figurkyKtereMuzuHrat.Count - 1)];
            }

            return null;
        }
    }
}