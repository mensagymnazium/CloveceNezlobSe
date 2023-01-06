using CloveceNezlobSe.Models;

namespace CloveceNezlobSe.Strategies.Jenda
{
	public class StrategieShitty : HerniStrategie
	{
		protected readonly Hra hra;

		public StrategieShitty(Hra hra)
		{
			this.hra = hra;
		}

		public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
		{
			return null;
		}
	}
}
