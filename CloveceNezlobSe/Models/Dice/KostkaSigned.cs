using System.Security.Cryptography;
using CloveceNezlobSe.Services;

namespace CloveceNezlobSe.Models
{
    public class KostkaSigned : IKostka
    {
        int pocetSten;
		private readonly IWriter writer;

		public KostkaSigned(int pocetSten, IWriter writer)
        {
            this.pocetSten = pocetSten;
			this.writer = writer;
		}

        public int Hod()
        {
            var lower = -pocetSten;
            var upper = pocetSten + 1;
            var x = RandomNumberGenerator.GetInt32(lower, upper);
            writer.WriteLine($"Kostka hodila {x}.");
            return x;
        }
    }
}
