// ReSharper disable CommentTypo

using CloveceNezlobSe.Models;

namespace CloveceNezlobSe.Strategies.MartinF;
public class HerniStrategieMartinF : HerniStrategie
{
	private readonly Hra hra;
	private HerniStrategieMartinFVahy vahy;


	public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
	{
		//Inicializace slovníku key: figurka, value: váha = 1
		var vahyVyberu = new Dictionary<Figurka, double>();
		foreach (var figurka in hrac.Figurky.Where(x => !x.JeVDomecku()))
		{
			vahyVyberu.Add(figurka, 1d);
		}

		#region deprecated
		////Zjisti, která figurka je nejdále na poli
		////-1, nekoukej na domecek
		//for (int i = hra.HerniPlan.Policka.Count - 1 - 1; i >= 0; i--)
		//{
		//    var policko = hra.HerniPlan.Policka[i];
		//    //if(policko.JeDomecek)
		//    //    continue;
		//    var naseFigurkyNaPolicku = policko.ZjistiFigurkyHrace(hrac);
		//    //Cast na array, proti multiciplitní enumeraci
		//    var figurkyNaPolicku = naseFigurkyNaPolicku as Figurka[] ?? naseFigurkyNaPolicku.ToArray();
		//    if (figurkyNaPolicku.Any())
		//    {
		//        vahyVyberu[figurkyNaPolicku.First()] *= VahaFigurkaJePrvni;
		//        break;
		//    }
		//}



		#endregion
		//Spočítej vzdálenosti figurek od cíle
		//i je (vzdálenost od cíle) - (2)
		//-1, nekoukej na domecek
		for (int i = hra.HerniPlan.Policka.Count - 1 - 1; i >= 0; i--)
		{
			var policko = hra.HerniPlan.Policka[i];
			//if(policko.JeDomecek)
			//    continue;
			var naseFigurkyNaPolicku = policko.ZjistiFigurkyHrace(hrac);
			foreach (var figurka in naseFigurkyNaPolicku)
			{
				double vzdalenostOdDomecku = i + 2;
				//Mezi 0-1
				double vzdalenostNormalizovana = vzdalenostOdDomecku / hra.HerniPlan.Policka.Count;
				var vahaVzdalenosti = 1 + vzdalenostNormalizovana * vahy.VahaPreferujiVzdalenost;
				vahyVyberu[figurka] *= vahaVzdalenosti;
			}
		}
		foreach (var figurka in hrac.Figurky)
		{
			var polickoHod = hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
			//Policko kam jdu neni, asi jsem prestrelil, rozhodne nehni figurkou
			if (polickoHod is null)
			{
				vahyVyberu[figurka] = 0;
				continue;
			}
			//Policko do ktereho jdu je domecek
			if (polickoHod is Domecek)
				vahyVyberu[figurka] *= vahy.VahaJduDoDomecku;
			//Na policku, kam jde figurka, je protihrac; vyhod ho
			else if (polickoHod.ZjistiFigurkyProtihracu(hrac).Any())
				vahyVyberu[figurka] *= vahy.VahaVyhozeniProtivnika;
			else if (polickoHod.ZjistiFigurkyHrace(hrac).Any())
			{
				//Vyhodím si svojí figurku, rozhodně nehni figurkou
				vahyVyberu[figurka] = 0;
				continue;
			}
			//Zkontroluj všechny políčka od sebe do cíle hodu
			for (int i = 1; i < hod; i++)
			{
				var policko = hra.HerniPlan.ZjistiCilovePolicko(figurka, i);
				if (policko is null)
					break;
				if (policko.ZjistiFigurkyProtihracu(hrac).Any())
				{
					//Protihracova figurka na ceste mezi mnou a cilovym hodem
					vahyVyberu[figurka] *= vahy.VahaRizikoVyhozeni;
				}
			}
			//Zkontroluj políčka za sebou o maximální počet hod kostky
			for (int i = 1; i <= 6; i++)
			{
				var policko = hra.HerniPlan.ZjistiCilovePolicko(figurka, -i);
				//Jsme za startem
				if (policko is null)
					break;
				//Policko je startovni policko
				if (policko.ZjistiFigurkyProtihracu(hrac).Any() && policko.ZjistiFigurkyHrace(hrac).Any())
					break;
				//Stojí zamnou protihráč
				if (policko.ZjistiFigurkyProtihracu(hrac).Any())
					vahyVyberu[figurka] *= vahy.VahaRizikoJeZamnouProtihrac;
			}
		}
		//Vyber figurku s nejvetsi vahou
		return vahyVyberu.MaxBy(x => x.Value).Key;
	}
	public HerniStrategieMartinF(Hra hra, HerniStrategieMartinFVahy vahy)
	{
		this.hra = hra;
		this.vahy = vahy;
	}
	//public HerniStrategieMartinF()
	//{
	//}
}