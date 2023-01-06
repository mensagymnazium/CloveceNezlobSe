using CloveceNezlobSe.Models;

namespace CloveceNezlobSe.Strategies.ViacheslavN
{
    public class HerniStrategiePoradSe : HerniStrategieTahniPrvniMoznouFigurkou
    {
        public HerniStrategiePoradSe(Hra hra) : base(hra)
        {
        }
        
        public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
        {
            HerniStrategie strategie = new CloveceNezlobSe.Strategies.MartinF.HerniStrategieMartinF(hra, new CloveceNezlobSe.Strategies.MartinF.HerniStrategieMartinFVahy());
            return strategie.DejFigurkuKterouHrat(hrac, hod);
        }
    }
}