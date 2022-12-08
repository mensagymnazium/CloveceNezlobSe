// ReSharper disable CommentTypo

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Text;

namespace CloveceNezlobSe.MartinF;
public class HerniStrategieMartinFOptimized : HerniStrategie
{
	private readonly Hra hra;
	private HerniStrategieMartinFVahy vahy;

	public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
    {
        var antiBenRand = new Random();

        //Inicializace slovníku key: figurka, value: váha = 1
		var vahyVyberu = new Dictionary<Figurka, double>();
		foreach (var figurka in hrac.Figurky.Where(x => !x.JeVDomecku()))
		{
			vahyVyberu.Add(figurka, 1d);
		}
		
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

        // ReSharper disable once ForCanBeConvertedToForeach
        for (var figurkaI = 0; figurkaI < hrac.Figurky.Count; figurkaI++)
        {
            var figurka = hrac.Figurky[figurkaI];
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
            
            //Vyhazuju si svoji figurku?
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
                if (policko is StartovniPolicko)
                    break;
                //Stojí zamnou protihráč
                if (policko.ZjistiFigurkyProtihracu(hrac).Any())
                    vahyVyberu[figurka] *= vahy.VahaRizikoJeZamnouProtihrac;
            }
        }

        //Vyber figurku s nejvetsi vahou
        var nejlepsiFigurkaPair = vahyVyberu.MaxBy(x => x.Value);

        //Radši nehnu, než si dělat paseku
        return nejlepsiFigurkaPair.Value == 0 ? null : nejlepsiFigurkaPair.Key;
    }

    private static Thread? AntiPredpovedNahodnostiVlakno;
    
    public HerniStrategieMartinFOptimized(Hra hra, HerniStrategieMartinFVahy vahy)
	{
		this.hra = hra;
		this.vahy = vahy;
        if (AntiPredpovedNahodnostiVlakno is null)
        {
            AntiPredpovedNahodnostiVlakno = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(TimeSpan.FromTicks(1000));
                    Random.Shared.Next();
                }
            })
            {
                IsBackground = true
            };
            AntiPredpovedNahodnostiVlakno.Start();
        }
    }
}