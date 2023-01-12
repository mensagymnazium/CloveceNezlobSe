using CloveceNezlobSe.Models.Boards;

namespace CloveceNezlobSe.Models;

/// <summary>
/// Herní plán je tvořen rovnou řadou políček. Všichni hráči začínají na stejném startovním políčku.
/// </summary>
public class LinearniHerniPlan : HerniPlan
{
	protected List<Policko> policka = new();
	public override IReadOnlyList<Policko> Policka => policka.AsReadOnly();

	public override int MaximalniPocetHracu => int.MaxValue;

	public LinearniHerniPlan(int pocetPolicek)
	{
		// startovní políčko
		policka.Add(new StartovniPolicko(this));

		// ostatní políčka
		for (int i = 1; i < pocetPolicek - 1; i++)
		{
			policka.Add(new Policko(this));
		}

		// cílové políčko
		policka.Add(new Domecek(this));
	}
	
	protected LinearniHerniPlan()
	{
		// inicializaci necháme na potomkovi
	}

	public override void DejFigurkuNaStartovniPolicko(Figurka figurka)
	{
		policka[0].PolozFigurku(figurka);
	}

	public override void HrajTahHrace(Hrac hrac, IKostka kostka)
	{
		var hozeneCislo = kostka.Hod();

		HerniRozhodnuti? herniRozhodnuti;
		try
		{
			herniRozhodnuti = hrac.DejHerniRozhodnuti(hozeneCislo, new HerniInformace()); // TODO HerniInformace
		}
		catch
		{
			herniRozhodnuti = null;
		}

		if (herniRozhodnuti == null)
		{
			Console.WriteLine($"Hráč {hrac.Jmeno} nebude táhnout.");
			return;
		}

		if (herniRozhodnuti?.Figurka == null)
		{
			return;
		}

		// TODO níže je základní herní algoritmus pro 
		
		Policko? stavajiciPolicko = herniRozhodnuti.Figurka.Policko;
		if (stavajiciPolicko is null)
		{
			throw new InvalidOperationException("Figurka není na žádném políčku.");
		}

		int indexStavajicihoPolicka = policka.IndexOf(stavajiciPolicko);
		int indexCile = (indexStavajicihoPolicka + hozeneCislo);

		if (indexCile < 0)
		{
			// figurka se vrací na začátek
			indexCile = 0;
		}

		if (indexCile >= policka.Count)
		{
			Console.WriteLine($"Figurka {herniRozhodnuti.Figurka.OznaceniFigurky} vyjela z herní plochy, cíl je potřeba trefit přesně.");
			return;
		}
		Policko cilovePolicko = policka[indexCile];

		// posun figurky na novou pozici
		stavajiciPolicko.ZvedniFigurku(herniRozhodnuti.Figurka);
		Console.WriteLine($"Posouvám figurku {herniRozhodnuti.Figurka.OznaceniFigurky} z pozice {indexStavajicihoPolicka} na pozici {indexCile}.");
		if (cilovePolicko.JeObsazeno())
		{
			Figurka vyhozenaFigurka = cilovePolicko.ZvedniJedinouFigurku();
			Console.WriteLine($"Vyhazuji figurku {vyhozenaFigurka.OznaceniFigurky} hráče: {vyhozenaFigurka.Hrac.Jmeno}");
			policka[0].PolozFigurku(vyhozenaFigurka);
		}
		cilovePolicko.PolozFigurku(herniRozhodnuti.Figurka);
	}

	public override Policko? ZjistiCilovePolicko(Figurka figurka, int hod)
	{
		if (figurka.Policko == null)
		{
			return null; // figurka není na herní ploše
		}

		int indexStavajicihoPolicka = policka.IndexOf(figurka.Policko);
		int indexCile = (indexStavajicihoPolicka + hod);

		if (indexCile < 0)
		{
			// figurka se vrací na začátek
			indexCile = 0;
		}

		if (indexCile >= policka.Count)
		{
			return null;
		}

		return policka[indexCile];
	}

	public override bool MuzuTahnout(Figurka figurka, int hod)
	{
		if (figurka.Policko == null)
		{
			return false; // figurka není na herní ploše
		}

		int indexStavajicihoPolicka = policka.IndexOf(figurka.Policko);
		if (indexStavajicihoPolicka > policka.Count - hod - 1)
		{
			return false; // figurka by vyjela z herní plochy
		}
		return true;
	}

	public override void Vykresli()
	{
		foreach (var policko in policka)
		{
			policko.Vykresli();
		}
		Console.WriteLine();
	}
}