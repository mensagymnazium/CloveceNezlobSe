using System.Security.Cryptography;

namespace CloveceNezlobSe.Models
{
	public class KostkaUnsigned : IKostka
	{
		int pocetSten;

		public KostkaUnsigned(int pocetSten)
		{
			this.pocetSten = pocetSten;
		}

		public int Hod()
		{
			var lower = 1;
			var upper = pocetSten + 1;
			var x = RandomNumberGenerator.GetInt32(lower, upper);
			Console.WriteLine($"Kostka hodila {x}.");
			return x;
		}
    }
}
