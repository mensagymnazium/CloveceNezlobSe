using System.Security.Cryptography;
using CloveceNezlobSe.Services;

namespace CloveceNezlobSe.Models
{
    public class KostkaHighRisk : IKostka
    {
        int pocetSten;
		private readonly IWriter writer;

		public KostkaHighRisk(int pocetSten, IWriter writer)
        {
            this.pocetSten = pocetSten;
			this.writer = writer;
		}

        public int Hod()
        {
            int[] values = new int[pocetSten];
            if (pocetSten % 2 == 0)
            {
                for (int i = 1; i <= pocetSten; i++)
                {
                    values[i - 1] = (i <= pocetSten / 2) ? -i : i;
                }
            }
            else
            {
                for (int i = 0; i < pocetSten; i++)
                {
                    values[i] = (i <= pocetSten / 2) ? -i : i;
                }
            }
            var x = RandomNumberGenerator.GetInt32(0, pocetSten);
            var hod = values[x];
			writer.WriteLine($"Kostka hodila {hod}.");
            return hod;
        }
    }
}
