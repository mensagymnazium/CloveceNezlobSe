using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CloveceNezlobSe;

public class HerniStrategieTester
{
    public void Test(int pocetTestu, Hra prefabrikatHry)
    {
        var vysledkyHer = new Dictionary<string, int>(); // Key: hráč, Value: počet vítěztví
        
        // vypneme výstup do konzole, abychom urychlili průběh
        var originalConsoleOut = Console.Out;
        Console.SetOut(TextWriter.Null);
        var sw = Stopwatch.StartNew();

        for (int i = 0; i < pocetTestu; i++)
        {
            Hrac vitez = HrajPartii(prefabrikatHry);
            vysledkyHer[vitez.Jmeno] = vysledkyHer.GetValueOrDefault(vitez.Jmeno, 0) + 1;
        }

        // obnovení výstupu do konzole
        Console.SetOut(originalConsoleOut);

        var consoleLogBuilder = new StringBuilder();
        consoleLogBuilder.AppendLine($"Took: {sw.ElapsedMilliseconds:n2} ms");

        foreach (var vysledek in vysledkyHer)
        {
            consoleLogBuilder.AppendLine($"\t{vysledek.Key}: {((double)vysledek.Value / pocetTestu)*100d}%");
        }

        Console.WriteLine(consoleLogBuilder.ToString());
    }



    private Hrac HrajPartii(Hra prefabrikatHry)
    {
        prefabrikatHry.NastavNahodnePoradiHracu();
        prefabrikatHry.Start();
        return prefabrikatHry.Vitezove[0]; // vrátíme vítěze
    }
}