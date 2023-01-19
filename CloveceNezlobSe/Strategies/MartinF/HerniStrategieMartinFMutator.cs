using CloveceNezlobSe.Models;
using CloveceNezlobSe.Services;
using System.Diagnostics;
using System.Text;
using CloveceNezlobSe.Strategies.MartinT;

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
		var historieMutaci = new Dictionary<HerniStrategieMartinFVahyTurboSmurf, double>();

		//Zaloz zakladni 2 historie mutaci
		for (int i = 0; i < 2; i++)
        {
            var zakladoveVahy = new HerniStrategieMartinFVahyTurboSmurf()
            {
                VahaVyhozeniProtivnika = 2.168093189399041, VahaJduDoDomecku = 3.576040055451859,
                VahaRizikoVyhozeni = 0.1252135910797434, VahaPreferujiVzdalenost = 2.7129007748022995,
                VahaJeZamnouProtihrac = 0.3511830509461851, VahaJduNaNaraznik = 0.4171353045047547,
                ThresholdPodKteryNehraju = 0.282276113908418
            };

            var zakladovaMutace = ZmutujVahy(zakladoveVahy);
			var zakladovyWinRate = ZiskejWinRateVah(zakladovaMutace);
			historieMutaci.Add(zakladovaMutace, zakladovyWinRate);
		}

		double nejlepsiZatim = 0;
		int pocetIteraciBezZlepseni = 0;
		while (true)
		{
            var sw = Stopwatch.StartNew();

            //Nastavení vah strategie
            HerniStrategieMartinFVahyTurboSmurf zmutovaneVahy;
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
				Console.SetOut(originalConsoleOut);

				Console.SetCursorPosition(0,Console.GetCursorPosition().Top-1);
                Console.WriteLine($"Hry bez zlepšení - {pocetIteraciBezZlepseni}");

                Console.SetOut(TextWriter.Null);
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

	private double ZiskejWinRateVah(HerniStrategieMartinFVahyTurboSmurf vahy)
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
	private Hrac HrajPartii(HerniStrategieMartinFVahyTurboSmurf vahy)
	{
		var herniPlan = new InsaneHerniPlan(writer);

		var hra = new Hra(herniPlan, writer);

		var herniStrategieNaVycvik = new HerniStrategieMartinFTurboSmurf(vahy);
		var herniStrategieDummy = new HerniStrategieMartinT(hra);

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
	private HerniStrategieMartinFVahyTurboSmurf ZmutujStrategii(IDictionary<HerniStrategieMartinFVahyTurboSmurf, double> historieMutaci)
	{
		//Vyber 2 nejlepsi,
		//Skombinuj
		//Vrat

		var nejlepsiStrategie = historieMutaci.MaxBy(x => x.Value);
		return ZmutujVahy(nejlepsiStrategie.Key);
	}

	private double silaMutaceChci = 0.05d;
	private double silaMutaceNechci = 0.005d;

	private const double NASOBITEL_NEUSPECHU_SILA_MUTACE_CHCI = 0.05d;
	private const double NASOBITEL_NEUSPECHU_SILA_MUTACE_NECHCI = 0.005d;
	private readonly IWriter writer;

	private HerniStrategieMartinFVahyTurboSmurf ZmutujVahy(HerniStrategieMartinFVahyTurboSmurf a)
	{
		var noveVahy = new HerniStrategieMartinFVahyTurboSmurf()
		{
			VahaPreferujiVzdalenost = a.VahaPreferujiVzdalenost + NahodnyOffset(silaMutaceChci),
			VahaJduDoDomecku = a.VahaJduDoDomecku + NahodnyOffset(silaMutaceChci),
			VahaJeZamnouProtihrac = a.VahaJeZamnouProtihrac + NahodnyOffset(silaMutaceChci),
			VahaVyhozeniProtivnika = a.VahaVyhozeniProtivnika + NahodnyOffset(silaMutaceChci),
			VahaRizikoVyhozeni = a.VahaRizikoVyhozeni + NahodnyOffset(silaMutaceNechci),
			VahaJduNaNaraznik = a.VahaJduNaNaraznik + NahodnyOffset(silaMutaceNechci),
			ThresholdPodKteryNehraju = a.VahaJduNaNaraznik + NahodnyOffset(silaMutaceNechci)
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