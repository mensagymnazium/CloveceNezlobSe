using System.Security.Cryptography;
using CloveceNezlobSe.Services;

namespace CloveceNezlobSe.Models
{
    public class KostkaOddUnsigned : IKostka
    {
        int pocetSten;
		private readonly IWriter writer;

		public KostkaOddUnsigned(int pocetSten, IWriter writer)
        {
            this.pocetSten = pocetSten;
			this.writer = writer;
		}

        public int Hod()
        {
            var lower = 1;
            var upper = pocetSten / 2 + pocetSten % 2 + 1;
            var x = RandomNumberGenerator.GetInt32(lower, upper);
            x = 2 * x - 1;
            writer.WriteLine($"Kostka hodila {x}.");
            return x;
        }
    }
}
