namespace CloveceNezlobSe.JanS;

public class HerniStrategieHonza : HerniStrategie
{
	protected readonly Hra Hra;

	public HerniStrategieHonza(Hra hra)
	{
		Hra = hra;
	}

	public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
	{
		ZapisFigurkyNeboUpdatniVyhozene(hrac);

		Figurka? f = JdiDoDomecku(hrac, hod) ??
				   Seber(hrac, hod) ??
				   TahniNejrychlejsiFigurku(hrac, hod);

		ZaznamenejTah(f, hod);
		return f;
	}

	private Figurka? JdiDoDomecku(Hrac hrac, int hod)
	{
		foreach (var figurka in hrac.Figurky)
		{
			var cilovePolicko = Hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
			if (cilovePolicko != null && cilovePolicko.JeDomecek) return figurka;
		}

		return null;
	}

	private Figurka? Seber(Hrac hrac, int hod)
	{
		foreach (var figurka in hrac.Figurky)
		{
			var cilovePolicko = Hra.HerniPlan.ZjistiCilovePolicko(figurka, hod);
			if (cilovePolicko != null)
			{
				if (!cilovePolicko.JeDomecek && cilovePolicko.ZjistiFigurkyProtihracu(hrac).Any())
				{
					return figurka;
				}
			}
		}

		return null;
	}

	// O malinko malinko horší než TahniNejrychlejsiFigurku
	private Figurka? TahniJednuFigurku(Hrac hrac, int hod)
	{
		var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
		var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => Hra.HerniPlan.MuzuTahnout(figurka, hod));
		if (figurkyKtereMuzuHrat.Any())
		{
			return figurkyKtereMuzuHrat.First();
		}

		return null;
	}

	private int _dalsiNaTahu;

	// O malinko horší než TahniJednuFigurku
	private Figurka? TahniRovnomerne(Hrac hrac, int hod)
	{
		var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
		var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => Hra.HerniPlan.MuzuTahnout(figurka, hod));
		if (figurkyKtereMuzuHrat.Any())
		{
			_dalsiNaTahu++;
			return figurkyKtereMuzuHrat.ElementAt(_dalsiNaTahu % figurkyKtereMuzuHrat.Count());
		}

		return null;
	}

	Dictionary<Figurka, int> _poziceFigurek = new();
	private Figurka? TahniNejrychlejsiFigurku(Hrac hrac, int hod)
	{
		var figurkyNaCeste = hrac.Figurky.Where(figurka => !figurka.JeVDomecku()).ToList();
		var figurkyKtereMuzuHrat = figurkyNaCeste.Where(figurka => Hra.HerniPlan.MuzuTahnout(figurka, hod));

		var sortedDict = from entry in _poziceFigurek orderby entry.Value descending select entry;

		foreach (var pair in sortedDict)
		{
			if (figurkyKtereMuzuHrat.Contains(pair.Key)) return pair.Key;
		}

		return null;
	}

	private void ZaznamenejTah(Figurka? f, int hod)
	{

		if (f != null) _poziceFigurek[f] += hod;
	}

	private void ZapisFigurkyNeboUpdatniVyhozene(Hrac hrac)
	{
		if (_poziceFigurek.Count() == 0)
		{
			foreach (Figurka figurka in hrac.Figurky)
			{
				_poziceFigurek.Add(figurka, 0);
			}
		}

		foreach (Figurka figurka in hrac.Figurky)
		{
			if (!figurka.Policko.JeObsazeno() && !figurka.Policko.JeDomecek) // Pokud je na startu
			{
				_poziceFigurek[figurka] = 0;
			}
		}
	}
}