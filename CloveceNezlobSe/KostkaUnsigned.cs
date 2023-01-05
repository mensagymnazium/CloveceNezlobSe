using System.Security.Cryptography;

namespace CloveceNezlobSe
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
            var x = RandomNumberGenerator.GetInt32(1, pocetSten + 1);
			Console.WriteLine($"Kostka hodila {x}.");
			return x;
		}
    }
}
