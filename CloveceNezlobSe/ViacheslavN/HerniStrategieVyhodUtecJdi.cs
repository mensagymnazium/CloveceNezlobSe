using System.Collections.Generic;
using System.Linq;

namespace CloveceNezlobSe
{
    public class HerniStrategieVyhodUtecJdi : HerniStrategieTahniPrvniMoznouFigurkou
    {
        public HerniStrategieVyhodUtecJdi(Hra hra) : base(hra)
        {
        }
        
        public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
        {
            List<Figurka> kdoMuzeVyhodit = new List<Figurka>();
            foreach (var figurka in hrac.Figurky)
            {
                var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
                if (cilovePolicko != null)
                {
                    if (cilovePolicko.JeDomecek)
                        return figurka;
                    if (cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any())
                        kdoMuzeVyhodit.Add(figurka);
                }
            }

            if (kdoMuzeVyhodit.Any())
                return kdoMuzeVyhodit.MinBy(f => JakDaleko(f));

            List<Figurka> kdoMaUtect = new List<Figurka>();
            foreach (Figurka figurka in hrac.Figurky)
            {
                var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
                if (cilovePolicko != null)
                {
                    for (byte i = 1; i <= 6; i++)
                    {
                        var ohrozujiciPolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, -i);
                        if (ohrozujiciPolicko != null)
                        {
                            if (ohrozujiciPolicko.ZjistiFigurkyProtihracu(hrac).Any() && !(ohrozujiciPolicko is StartovniPolicko))
                            {
                                kdoMaUtect.Add(figurka);
                                break;
                            }
                        }
                    }
                }
            }
            if (kdoMaUtect.Count > 0)
                return kdoMaUtect.MinBy(f => JakDaleko(f));

            var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
            var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => hra.HerniPlan.MuzuTahnout(figurka, hod));
            if (figurkyKtereMuzuHrat.Any())
            {
                return figurkyKtereMuzuHrat.MinBy(f => JakDaleko(f));
            }

            return null;
        }

        private int JakDaleko(Figurka figurka)
        {
            for (int i = 0; true; i++)
            {
                if (hra.HerniPlan.ZjistiCilovePolicko(figurka, i).JeDomecek)
                    return i;
            }
        }
    }
}