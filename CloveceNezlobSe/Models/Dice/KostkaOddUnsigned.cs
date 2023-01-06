using System.Security.Cryptography;

namespace CloveceNezlobSe.Models
{
    public class KostkaOddUnsigned : IKostka
    {
        int pocetSten;

        public KostkaOddUnsigned(int pocetSten)
        {
            this.pocetSten = pocetSten;
        }

        public int Hod()
        {
            var lower = 1;
            var upper = pocetSten / 2 + pocetSten % 2 + 1;
            var x = RandomNumberGenerator.GetInt32(lower, upper);
            x = 2 * x - 1;
            Console.WriteLine($"Kostka hodila {x}.");
            return x;
        }
    }
}
