﻿using CloveceNezlobSe.Models.Figurky;
using CloveceNezlobSe.Services;

namespace CloveceNezlobSe.Models;

public abstract class HerniPlan
{
    protected HerniPlan(IWriter writer)
    {
		this.writer = writer;
	}

    public abstract IReadOnlyList<Policko> Policka { get; }
		
    public abstract int MaximalniPocetHracu { get; }

    public abstract void DejFigurkuNaStartovniPolicko(Figurka figurka);
		
    public abstract Policko? ZjistiCilovePolicko(Figurka figurka, int hod);

    public abstract bool MuzuTahnout(Figurka figurka, int hod);

    public abstract void HrajTahHrace(Hrac hrac, IKostka kostka);

    public abstract void Vykresli();

    private Hra? hra;
	protected readonly IWriter writer;

	/// <summary>
	/// Hra může přečtena
	/// Nastavena může být pouze jednou (to při inicializaci herniho planu)
	/// </summary>
	public Hra? Hra
    {
        get => hra;
        set => hra ??= value;
    }
}