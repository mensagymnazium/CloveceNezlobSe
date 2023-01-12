using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;

namespace CloveceNezlobSe.Strategies
{
    public class HerniStrategieTahniPrvniMoznouFigurkou : HerniStrategie
    {
        protected readonly Hra hra;

        public HerniStrategieTahniPrvniMoznouFigurkou(Hra hra)
        {
            this.hra = hra;
        }

		public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
		{
			var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
            var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => hra.HerniPlan.MuzuTahnout(figurka, hod));
            if (figurkyKtereMuzuHrat.Any())
            {
                return new HerniRozhodnuti() { Figurka = figurkyKtereMuzuHrat.First() };
            }

            return null;
        }
    }
}
