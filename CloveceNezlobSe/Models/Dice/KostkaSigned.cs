using System.Security.Cryptography;

namespace CloveceNezlobSe.Models
{
    public class KostkaSigned : IKostka
    {
        int pocetSten;

        public KostkaSigned(int pocetSten)
        {
            this.pocetSten = pocetSten;
        }

        public int Hod()
        {
            var lower = -pocetSten;
            var upper = pocetSten + 1;
            var x = RandomNumberGenerator.GetInt32(lower, upper);
            Console.WriteLine($"Kostka hodila {x}.");
            return x;
        }
    }
}
