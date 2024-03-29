﻿using System.Diagnostics.Contracts;
using CloveceNezlobSe.Models.Boards;
using CloveceNezlobSe.Models.Figurky;
using CloveceNezlobSe.Services;

namespace CloveceNezlobSe.Models;

/// <summary>
/// Herní plán je tvořen rovnou řadou políček. Všichni hráči začínají na stejném startovním políčku.
/// </summary>
public class LinearniHerniPlan : HerniPlan
{
	protected List<Policko> policka = new();

	public override IReadOnlyList<Policko> Policka => policka.AsReadOnly();

	public override int MaximalniPocetHracu => int.MaxValue;

    public LinearniHerniPlan(int pocetPolicek, IWriter writer) : base(writer)
    {
        // startovní políčko
        policka.Add(new StartovniPolicko(this));

        // ostatní políčka
        for (int i = 1; i<pocetPolicek - 1; i++)
        {
            policka.Add(new Policko(this));
        }

        // cílové políčko
        policka.Add(new Domecek(this));
    }

	protected LinearniHerniPlan(IWriter writer) : base(writer) 
	{
		// inicializaci necháme na potomkovi
	}

	public override void DejFigurkuNaStartovniPolicko(Figurka figurka)
	{
		policka[0].PolozFigurku(figurka);
	}

	public override void HrajTahHrace(Hrac hrac, IKostka kostka)
	{
		Contract.Assert(Hra is not null);

		bool hracJeNaTahu = true;
		while (hracJeNaTahu)
		{
			var hozeneCislo = kostka.Hod();

			HerniRozhodnuti? herniRozhodnuti;
			try
            {
                var herniInformace = new HerniInformace(this, Hra);
                herniRozhodnuti = hrac.DejHerniRozhodnuti(hozeneCislo, herniInformace); // TODO HerniInformace
			}
			catch
			{
				herniRozhodnuti = null;
			}

			hracJeNaTahu = OdehrajRozhodnutiHrace(hrac, hozeneCislo, herniRozhodnuti);
		}
	}

	/// <summary>
	/// Realizuje herní rozhodnutí hráče, provede individuální tah.
	/// </summary>
	/// <returns><c>true</c>, pokud má hráč hrát znovu, jinak <c>false</c></returns>
	private bool OdehrajRozhodnutiHrace(Hrac hrac, int hozeneCislo, HerniRozhodnuti? herniRozhodnuti)
	{
		if (herniRozhodnuti?.Figurka?.Policko == null)
		{
			writer.WriteLine($"Hráč {hrac.Jmeno} nebude táhnout.");
			return false;
		}

		Policko? stavajiciPolicko = herniRozhodnuti.Figurka.Policko;
		Policko? cilovePolicko = ZjistiCilovePolicko(herniRozhodnuti.Figurka, hozeneCislo);

		if (cilovePolicko is null)
		{
			writer.WriteLine($"Figurka {herniRozhodnuti.Figurka.OznaceniFigurky} vyjela z herní plochy, tah končí.");
			return false;
		}

		// posun figurky na novou pozici
		switch (cilovePolicko)
		{
			case Naraznik:
				// nárazník vrátí figurku zpět o poloviční počet políček
				PosunFigurku(herniRozhodnuti.Figurka, stavajiciPolicko, cilovePolicko);
				int posun = hozeneCislo / 2 * (-1);
				if (posun == 0)
				{
					break;
				}
				writer.WriteLine($"Figurka {herniRozhodnuti.Figurka.OznaceniFigurky} se odrazila od nárazníku a je jím posunuta o {posun}");
				OdehrajRozhodnutiHrace(hrac, posun, herniRozhodnuti);
				break;
			case Zumpa:
				// žumpa přesune figurku na start
				writer.WriteLine("Spadnul si do žumpy, přesouvám na start.");
				var startovniPolicko = policka[0];
				PosunFigurku(herniRozhodnuti.Figurka, stavajiciPolicko, startovniPolicko);
				break;
			default:
				PosunFigurku(herniRozhodnuti.Figurka, stavajiciPolicko, cilovePolicko);
				break;
		}

		return (hozeneCislo == 6); // pokud hodí šestku a odehrál, hraje znovu
	}

	private void PosunFigurku(Figurka figurka, Policko stavajiciPolicko, Policko cilovePolicko)
	{
        switch (figurka)
        {
			case Tank:
				int indexStavajicihoPolicka = policka.IndexOf(stavajiciPolicko);
				int indexCile = policka.IndexOf(cilovePolicko);

				stavajiciPolicko.ZvedniFigurku(figurka);
				writer.WriteLine($"Přesouvám figurku {figurka.OznaceniFigurky} z pozice {policka.IndexOf(stavajiciPolicko)} na pozici {policka.IndexOf(cilovePolicko)}.");
				Policko prejetePolicko = policka[indexStavajicihoPolicka];
				
				for(int i = indexStavajicihoPolicka+1; i < indexCile; i++)
                {
					prejetePolicko = policka[i];
					if (prejetePolicko.JeObsazeno())
					{
						Figurka vyhozenaFigurka = prejetePolicko.ZvedniJedinouFigurku();
						writer.WriteLine($"Vyhazuji figurku {vyhozenaFigurka.OznaceniFigurky} hráče: {vyhozenaFigurka.Hrac.Jmeno}");
						policka[0].PolozFigurku(vyhozenaFigurka);
					}
				}
				if (cilovePolicko.JeObsazeno())
				{
					Figurka vyhozenaFigurka = cilovePolicko.ZvedniJedinouFigurku();
					writer.WriteLine($"Vyhazuji figurku {vyhozenaFigurka.OznaceniFigurky} hráče: {vyhozenaFigurka.Hrac.Jmeno}");
					policka[0].PolozFigurku(vyhozenaFigurka);
				}
				cilovePolicko.PolozFigurku(figurka);
				break;



			default:
				stavajiciPolicko.ZvedniFigurku(figurka);
				writer.WriteLine($"Přesouvám figurku {figurka.OznaceniFigurky} z pozice {policka.IndexOf(stavajiciPolicko)} na pozici {policka.IndexOf(cilovePolicko)}.");
				if (cilovePolicko.JeObsazeno())
				{
					Figurka vyhozenaFigurka = cilovePolicko.ZvedniJedinouFigurku();
					writer.WriteLine($"Vyhazuji figurku {vyhozenaFigurka.OznaceniFigurky} hráče: {vyhozenaFigurka.Hrac.Jmeno}");
					policka[0].PolozFigurku(vyhozenaFigurka);
				}
				cilovePolicko.PolozFigurku(figurka);
				break;

		}
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
			policko.Vykresli(writer);
		}
		writer.WriteLine();
	}
}