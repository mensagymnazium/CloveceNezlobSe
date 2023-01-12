using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;
using System.Collections.Generic;
using System.Linq;

namespace CloveceNezlobSe.Strategies.ViacheslavN
{
    public class HerniStrategieVyhodUtecJdi : HerniStrategieTahniPrvniMoznouFigurkou
    {
        public HerniStrategieVyhodUtecJdi(Hra hra) : base(hra)
        {
        }

		public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
		{
			List<Figurka> kdoMuzeVyhodit = new List<Figurka>();
            foreach (var figurka in hrac.Figurky)
            {
                var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
                if (cilovePolicko != null)
                {
                    if (cilovePolicko is Domecek)
                        return new HerniRozhodnuti() { Figurka = figurka };
                    if (cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any())
                        kdoMuzeVyhodit.Add(figurka);
                }
            }

            if (kdoMuzeVyhodit.Any())
                return new HerniRozhodnuti() { Figurka = kdoMuzeVyhodit.MinBy(f => JakDaleko(f)) };

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

            if (kdoMaUtect.Any())
            {
                while (true)
                {
                    if (!kdoMaUtect.Any())
                        break;
                    Figurka figurka = kdoMaUtect.MinBy(f => JakDaleko(f));
                    if (hra.HerniPlan.ZjistiCilovePolicko(figurka, hod).JeObsazeno())
                        kdoMaUtect.Remove(figurka);
                    else
                        return new HerniRozhodnuti() { Figurka = figurka };
                }
            }

            var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
            var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => hra.HerniPlan.MuzuTahnout(figurka, hod));
            if (figurkyKtereMuzuHrat.Any())
            {
                return new HerniRozhodnuti() { Figurka = figurkyKtereMuzuHrat.MinBy(f => JakDaleko(f)) };
            }

            return null;
        }

        private int JakDaleko(Figurka figurka)
        {
            for (int i = 0; true; i++)
            {
                if (hra.HerniPlan.ZjistiCilovePolicko(figurka, i) is Domecek)
                    return i;
            }
        }
    }
}