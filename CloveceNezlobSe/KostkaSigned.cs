using System.Security.Cryptography;

namespace CloveceNezlobSe
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
            var x = RandomNumberGenerator.GetInt32(-pocetSten, pocetSten + 1);
            Console.WriteLine($"Kostka hodila {x}.");
            return x;
        }
    }
}
