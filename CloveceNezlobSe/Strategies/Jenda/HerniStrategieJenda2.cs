using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;

namespace CloveceNezlobSe.Strategies.Jenda
{
	public class MojeStrategieJenda2 : HerniStrategie
	{
		protected readonly Hra hra;
		private string[] mutipliers = new string[358];
		private string[] Adders = new string[35];

		public MojeStrategieJenda2(Hra hra)
		{
			this.hra = hra;
		}
		public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
		{
			const int vahaDojdi = 10;
			const int vahaDelDojdi = 4;
			const int vyhozeniSebe = 50;
			const int vyhozeniSoupere = 10;

			var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
			List<Figurka> figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => hra.HerniPlan.MuzuTahnout(figurka, hod)).ToList();

			List<int> hodnoty = new List<int>();

			for (int i = 0; i < figurkyKtereMuzuHrat.Count; i++)
			{
				hodnoty.Add(0);
			}

			if (hodnoty.Count == 0)
				return new HerniRozhodnuti() { Figurka = figurkyKtereMuzuHrat.FirstOrDefault() }	;

			for (int i = 0; i < hodnoty.Count; i++)
			{
				if (hra.HerniPlan.ZjistiCilovePolicko(figurkyKtereMuzuHrat[i], hod) is Domecek)
					hodnoty[i] += vahaDojdi;

				if (hra.HerniPlan.ZjistiCilovePolicko(figurkyKtereMuzuHrat[i], hod + 1) is Domecek)
					hodnoty[i] += (int)Math.Floor((decimal)vahaDojdi / vahaDelDojdi);
				if (hra.HerniPlan.ZjistiCilovePolicko(figurkyKtereMuzuHrat[i], hod + 2) is Domecek)
					hodnoty[i] += (int)Math.Floor((decimal)vahaDojdi / vahaDelDojdi);
				if (hra.HerniPlan.ZjistiCilovePolicko(figurkyKtereMuzuHrat[i], hod + 3) is Domecek)
					hodnoty[i] += (int)Math.Floor((decimal)vahaDojdi / vahaDelDojdi);
				if (hra.HerniPlan.ZjistiCilovePolicko(figurkyKtereMuzuHrat[i], hod + 4) is Domecek)
					hodnoty[i] += (int)Math.Floor((decimal)vahaDojdi / vahaDelDojdi);
				if (hra.HerniPlan.ZjistiCilovePolicko(figurkyKtereMuzuHrat[i], hod + 5) is Domecek)
					hodnoty[i] += (int)Math.Floor((decimal)vahaDojdi / vahaDelDojdi);
				if (hra.HerniPlan.ZjistiCilovePolicko(figurkyKtereMuzuHrat[i], hod + 6) is Domecek)
					hodnoty[i] += (int)Math.Floor((decimal)vahaDojdi / vahaDelDojdi);

			}


			int nejlepsi = int.MinValue;
			var vyhod = figurkyKtereMuzuHrat[0];

			for (int i = 0; i < hodnoty.Count; i++)
			{
				if (hodnoty[i] > nejlepsi)
				{
					nejlepsi = hodnoty[i];
					vyhod = figurkyKtereMuzuHrat[i];
				}
			}

			return new HerniRozhodnuti() { Figurka = vyhod };


		}
	}
}