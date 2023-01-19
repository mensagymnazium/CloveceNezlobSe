using CloveceNezlobSe.Models;
using CloveceNezlobSe.Services;
using System.Diagnostics;
using System.Text;

namespace CloveceNezlobSe.Strategies.MartinF;

public class HerniStrategieMartinFMutator
{
	public HerniStrategieMartinFMutator(IWriter writer)
	{
		this.writer = writer;
	}

	/// <summary>
	/// Množství testů odehrané v jedné trénovací várce
	/// </summary>
	private const int MNOZSTVI_TESTU = 1000;
	private const string JMENO_HRACE = "Martin:custom";

	public void TrenujStrategii()
	{
		//Vypneme výstup do konzole, abychom urychlili průběh
		var originalConsoleOut = Console.Out;
		Console.SetOut(TextWriter.Null);

		int iterace = 0;
		var historieMutaci = new Dictionary<HerniStrategieMartinFVahy, double>();

		//Zaloz zakladni 2 historie mutaci
		for (int i = 0; i < 2; i++)
		{
			var zakladovaMutace = SkombinujVahay(new HerniStrategieMartinFVahy(), new HerniStrategieMartinFVahy());
			var zakladovyWinRate = ZiskejWinRateVah(zakladovaMutace);
			historieMutaci.Add(zakladovaMutace, zakladovyWinRate);
		}

		double nejlepsiZatim = 0;
		int pocetIteraciBezZlepseni = 0;
		while (true)
		{

			var sw = Stopwatch.StartNew();

			//Nastavení vah strategie
			HerniStrategieMartinFVahy zmutovaneVahy;
			double winRateMutace;

			lock (historieMutaci)
			{
				zmutovaneVahy = ZmutujStrategii(historieMutaci);
				winRateMutace = ZiskejWinRateVah(zmutovaneVahy);
			}

			if (winRateMutace > nejlepsiZatim)
			{

				//Obnovení výstupu do konzole
				Console.SetOut(originalConsoleOut);

				var consoleLogBuilder = new StringBuilder();
				consoleLogBuilder.AppendLine($"Iterace {iterace} zabrala {sw.ElapsedMilliseconds:n2} ms po {pocetIteraciBezZlepseni} iteraci bez zlepseni");
				consoleLogBuilder.AppendLine($"Váhy: {zmutovaneVahy.ToString()}");
				consoleLogBuilder.AppendLine($"Výhra: {winRateMutace * 100}%");
				Console.WriteLine(consoleLogBuilder.ToString());

				Console.SetOut(TextWriter.Null);

				pocetIteraciBezZlepseni = 0;
				nejlepsiZatim = winRateMutace;
			}
			else
			{
				pocetIteraciBezZlepseni++;
				silaMutaceChci = (1 + pocetIteraciBezZlepseni) * NASOBITEL_NEUSPECHU_SILA_MUTACE_CHCI;
				silaMutaceNechci = (1 + pocetIteraciBezZlepseni) * NASOBITEL_NEUSPECHU_SILA_MUTACE_NECHCI;
			}

			//Zápis do historie mutací
			lock (historieMutaci)
			{
				historieMutaci.TryAdd(zmutovaneVahy, winRateMutace);
			}

			iterace++;
		}
		// ReSharper disable once FunctionNeverReturns
	}

	private double ZiskejWinRateVah(HerniStrategieMartinFVahy vahy)
	{
		var vyhraneHry = 0d;
		for (int i = 0; i < MNOZSTVI_TESTU; i++)
		{
			Hrac vitez = HrajPartii(vahy);
			if (vitez.Jmeno == JMENO_HRACE)
				vyhraneHry++;
		}

		return vyhraneHry / MNOZSTVI_TESTU;
	}
	private Hrac HrajPartii(HerniStrategieMartinFVahy vahy)
	{
		var herniPlan = new LinearniHerniPlan(pocetPolicek: 40, writer);

		var hra = new Hra(herniPlan, writer);

		var herniStrategieNaVycvik = new HerniStrategieMartinFOptimized(hra, vahy);
		var herniStrategieDummy = new HerniStrategieMartinF(hra, new HerniStrategieMartinFVahy());

		var hrac1 = new Hrac(JMENO_HRACE, herniStrategieNaVycvik);
		var hrac2 = new Hrac("Dumík", herniStrategieDummy);

		hra.PridejHrace(hrac1);
		hra.PridejHrace(hrac2);
		hra.NastavNahodnePoradiHracu();

		hra.Start(writer);

		return hra.Vitezove[0]; // vrátíme vítěze
	}

	/// <summary></summary>
	/// <param name="historieMutaci">Key: váhy, value: úspěšnost (fitness) v procentech (0.0d - 1.0d)</param>
	/// <returns></returns>
	private HerniStrategieMartinFVahy ZmutujStrategii(IDictionary<HerniStrategieMartinFVahy, double> historieMutaci)
	{
		//Vyber 2 nejlepsi,
		//Skombinuj
		//Vrat

		var nejlepsiGeny = historieMutaci.OrderByDescending(x => x.Value).Take(2).ToArray();
		return SkombinujVahay(nejlepsiGeny[0].Key, nejlepsiGeny[1].Key);
	}

	private double silaMutaceChci = 0.05d;
	private double silaMutaceNechci = 0.005d;

	private const double NASOBITEL_NEUSPECHU_SILA_MUTACE_CHCI = 0.05d;
	private const double NASOBITEL_NEUSPECHU_SILA_MUTACE_NECHCI = 0.005d;
	private readonly IWriter writer;

	private HerniStrategieMartinFVahy SkombinujVahay(HerniStrategieMartinFVahy a, HerniStrategieMartinFVahy b)
	{
		var noveVahy = new HerniStrategieMartinFVahy()
		{
			VahaPreferujiVzdalenost = (a.VahaPreferujiVzdalenost + b.VahaPreferujiVzdalenost) / 2d + NahodnyOffset(silaMutaceChci),
			VahaJduDoDomecku = (a.VahaJduDoDomecku + b.VahaJduDoDomecku) / 2d + NahodnyOffset(silaMutaceChci),
			VahaRizikoJeZamnouProtihrac = (a.VahaRizikoJeZamnouProtihrac + b.VahaRizikoJeZamnouProtihrac) / 2d + NahodnyOffset(silaMutaceChci),
			VahaVyhozeniProtivnika = (a.VahaVyhozeniProtivnika + b.VahaVyhozeniProtivnika) / 2d + NahodnyOffset(silaMutaceChci),
			VahaRizikoVyhozeni = (a.VahaRizikoVyhozeni + b.VahaRizikoVyhozeni) / 2d + NahodnyOffset(silaMutaceNechci)
		};

		return noveVahy;
	}

	/// <summary>
	/// Vrátí číslo od -škála až škála
	/// </summary>
	/// <param name="skala"></param>
	/// <returns></returns>
	private double NahodnyOffset(double skala)
	{
		//0 - 1 -> -0.5 - 0.5 -> -1 - 1 -> -skala - skala
		return (Random.Shared.NextDouble() - 0.5d) * 2 * skala;
	}
}