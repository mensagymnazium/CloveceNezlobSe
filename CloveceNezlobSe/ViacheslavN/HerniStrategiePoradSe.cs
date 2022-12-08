namespace CloveceNezlobSe
{
    public class HerniStrategiePoradSe : HerniStrategieTahniPrvniMoznouFigurkou
    {
        public HerniStrategiePoradSe(Hra hra) : base(hra)
        {
        }
        
        public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
        {
            HerniStrategie strategie = new CloveceNezlobSe.MartinF.HerniStrategieMartinF(hra, new CloveceNezlobSe.MartinF.HerniStrategieMartinFVahy())
            return strategie.DejFigurkuKterouHrat(hrac, hod);
        }
    }
}