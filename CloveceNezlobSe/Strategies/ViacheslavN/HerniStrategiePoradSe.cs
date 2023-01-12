using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;

namespace CloveceNezlobSe.Strategies.ViacheslavN
{
    public class HerniStrategiePoradSe : HerniStrategieTahniPrvniMoznouFigurkou
    {
        public HerniStrategiePoradSe(Hra hra) : base(hra)
        {
        }

		public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
		{
			HerniStrategie strategie = new CloveceNezlobSe.Strategies.MartinF.HerniStrategieMartinF(hra, new CloveceNezlobSe.Strategies.MartinF.HerniStrategieMartinFVahy());
            return strategie.DejHerniRozhodnuti(hrac, hod, informace);
        }
    }
}