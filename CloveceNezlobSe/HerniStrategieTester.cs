using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CloveceNezlobSe;

public class HerniStrategieTester
{
    public Dictionary<string, int> Test(int pocetTestu, Func<Hra> hraActivator)
    {
        var vysledkyHer = new Dictionary<string, int>(); // Key: hráč, Value: počet vítěztví
       
        // vypneme výstup do konzole, abychom urychlili průběh
        var originalConsoleOut = Console.Out;
        Console.SetOut(TextWriter.Null);
        var sw = Stopwatch.StartNew();

        for (int i = 0; i < pocetTestu; i++)
        {
			var hra = hraActivator();
			var vitez = HrajPartii(hra);
            vysledkyHer[vitez.Jmeno] = vysledkyHer.GetValueOrDefault(vitez.Jmeno, 0) + 1;
        }

        // obnovení výstupu do konzole
        Console.SetOut(originalConsoleOut);

        var consoleLogBuilder = new StringBuilder();
        consoleLogBuilder.AppendLine($"Odehráno ({sw.ElapsedMilliseconds:n2} ms)");

        foreach (var vysledek in vysledkyHer.OrderByDescending(i => i.Value))
        {
            consoleLogBuilder.AppendLine($"\t{vysledek.Key}: {((double)vysledek.Value / pocetTestu):p2}");
        }

        Console.WriteLine(consoleLogBuilder.ToString());

        return vysledkyHer;
    }
    


    private Hrac HrajPartii(Hra hra)
    {
        hra.NastavNahodnePoradiHracu();
        hra.Start();
        return hra.Vitezove[0]; // vrátíme vítěze
    }
}