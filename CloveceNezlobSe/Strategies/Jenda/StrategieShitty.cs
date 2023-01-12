using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;

namespace CloveceNezlobSe.Strategies.Jenda
{
	public class StrategieShitty : HerniStrategie
	{
		protected readonly Hra hra;

		public StrategieShitty(Hra hra)
		{
			this.hra = hra;
		}

		public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
		{
			return null;
		}
	}
}
