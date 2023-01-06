using CloveceNezlobSe.Models;

namespace CloveceNezlobSe.Strategies
{
    public abstract class HerniStrategie
    {
        public abstract Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod);
    }
}