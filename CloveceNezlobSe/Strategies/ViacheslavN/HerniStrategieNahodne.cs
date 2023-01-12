using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;
using CloveceNezlobSe.Models.Figurky;
using System;
using System.Linq;

namespace CloveceNezlobSe.Strategies.ViacheslavN
{
    public class HerniStrategieNahodne : HerniStrategie
    {
        private readonly Hra hra;
        
        public HerniStrategieNahodne(Hra hra)
        {
            this.hra = hra;
        }

		public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
		{
			var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
            var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => hra.HerniPlan.MuzuTahnout(figurka, hod)).ToList();
            foreach (Figurka figurka in figurkyKtereMuzuHrat)
            {
                var cilovePolicko = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
                if (cilovePolicko == null)
                    figurkyKtereMuzuHrat.Remove(figurka);
                else if (cilovePolicko is Domecek)
                    return new HerniRozhodnuti() { Figurka = figurka };
            }
            if (figurkyKtereMuzuHrat.Any())
            {
                return new HerniRozhodnuti() { Figurka = figurkyKtereMuzuHrat[Random.Shared.Next(0, figurkyKtereMuzuHrat.Count - 1)] };
            }

            return null;
        }
    }
}