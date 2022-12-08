namespace CloveceNezlobSe.VlastniHerniStrategie;



public class HerniStrategieMaxJAdvanced : HerniStrategie
{
	Hra hra;
	public HerniStrategieMaxJAdvanced(Hra hra)
	{
		this.hra = hra;
	}



	public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
	{
		List<Figurka> hratelneFigurky = hrac.Figurky.Where(a => a.Policko is Domecek == false && hra.HerniPlan.MuzuTahnout(a, hod)).ToList();
		int[] vaha = new int[hratelneFigurky.Count];
		if (hratelneFigurky.Count == 0)
		{
			Console.WriteLine("Ted");
		}
		int nejlepsi = 0;
		int nejlepsiHodnota = int.MinValue;



		//Chces vyhazovat
		for (int i = 0; i < hratelneFigurky.Count; i++)
		{
			var kamDojdu = hra.HerniPlan.ZjistiCilovePolicko(hratelneFigurky[i], hod);
			if (kamDojdu.ZjistiFigurkyProtihracu(hrac).Any())
			{
				vaha[i] += 8;
			}
		}



		//Chces byt tak, aby te nemohli vyhodit
		for (int i = 0; i < hratelneFigurky.Count; i++)
		{
			Policko posledniNajitePolicko = new Policko();
			for (int a = 1; a < 7; a++)
			{
				var kamDojdu = hra.HerniPlan.ZjistiCilovePolicko(hratelneFigurky[i], -a + hod);
				if (kamDojdu.ZjistiFigurkyProtihracu(hrac).Any() && posledniNajitePolicko != kamDojdu)
				{
					vaha[i] -= 4;
				}
				posledniNajitePolicko = kamDojdu;
			}
		}



		//Chces utect ostatnim
		for (int i = 0; i < hratelneFigurky.Count; i++)
		{
			Policko posledniNajitePolicko = new Policko();
			for (int a = 1; a < 7; a++)
			{
				var kamDojdu = hra.HerniPlan.ZjistiCilovePolicko(hratelneFigurky[i], -a);
				if (kamDojdu.ZjistiFigurkyProtihracu(hrac).Any() && posledniNajitePolicko != kamDojdu)
				{
					vaha[i] += 4;
				}
				posledniNajitePolicko = kamDojdu;
			}
		}



		//Nechces si vyhodit vlastni
		for (int i = 0; i < hratelneFigurky.Count; i++)
		{
			var kamDojdu = hra.HerniPlan.ZjistiCilovePolicko(hratelneFigurky[i], hod);
			if (kamDojdu.ZjistiFigurkyHrace(hrac).Any() && kamDojdu is not Domecek)
			{
				vaha[i] -= 1000;
			}
		}




		//Chces se dostat do domecku
		for (int i = 0; i < hratelneFigurky.Count; i++)
		{
			for (int x = 1; x < 7; x++)
			{
				var kamDojdu = hra.HerniPlan.ZjistiCilovePolicko(hratelneFigurky[i], x);
				if (kamDojdu is Domecek)
				{
					vaha[i] += 200;
				}
			}



		}



		for (int i = 0; i < hratelneFigurky.Count; i++)
		{
			if (vaha[i] > nejlepsiHodnota)
			{
				nejlepsiHodnota = vaha[i];
				nejlepsi = i;
			}
		}



		if (hratelneFigurky.Count > 0)
		{
			return hratelneFigurky[nejlepsi];
		}



		return hratelneFigurky.FirstOrDefault();
	}
}