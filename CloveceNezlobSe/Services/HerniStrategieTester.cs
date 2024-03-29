﻿using CloveceNezlobSe.Models;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CloveceNezlobSe.Services;

public class HerniStrategieTester
{
	private readonly IWriter writer;

	public HerniStrategieTester(IWriter writer)
    {
		this.writer = writer;
	}

	public Dictionary<string, int> Test(int pocetTestu, Func<Hra> hraActivator)
    {
        var vysledkyHer = new Dictionary<string, int>(); // Key: hráč, Value: počet vítěztví

        var sw = Stopwatch.StartNew();

        for (int i = 0; i < pocetTestu; i++)
        {
			var hra = hraActivator();
			var vitez = HrajPartii(hra);
            vysledkyHer[vitez.Jmeno] = vysledkyHer.GetValueOrDefault(vitez.Jmeno, 0) + 1;
        }

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
        hra.Start(writer);
        return hra.Vitezove[0]; // vrátíme vítěze
    }
}