﻿namespace CloveceNezlobSe;

public class Policko
{
    protected HashSet<Figurka> figurkyNaPolicku = new();
    protected bool dovolitViceFigurek;
    protected HerniPlan herniPlan;

    [Obsolete("Použij (policko is Domecek)")]
    public bool JeDomecek => this is Domecek;

    public Policko(HerniPlan herniPlan, bool dovolitViceFigurek = false)
    {
        this.dovolitViceFigurek = dovolitViceFigurek;
        this.herniPlan = herniPlan;
    }

    public virtual void PolozFigurku(Figurka figurka, int hod)
    {
        if (JeObsazeno())
        {
            throw new InvalidOperationException("Na políčku je již figurka a políčko nedovoluje více figurek.");
        }
        figurkyNaPolicku.Add(figurka);
        figurka.NastavPolicko(this);
    }

    public Figurka ZvedniJedinouFigurku()
    {
        if (figurkyNaPolicku.Count != 1)
        {
            throw new InvalidOperationException("Na políčku není jediná figurka.");
        }
			
        var figurka = figurkyNaPolicku.First();
        ZvedniFigurku(figurka);

        return figurka;
    }

    public void ZvedniFigurku(Figurka figurka)
    {
        if (!figurkyNaPolicku.Contains(figurka))
        {
            throw new InvalidOperationException("Na políčku figurka není.");
        }

        figurkyNaPolicku.Remove(figurka);
        figurka.NastavPolicko(null);
    }

    public IEnumerable<Figurka> ZjistiFigurkyHrace(Hrac hrac)
    {
        return figurkyNaPolicku.Where(figurka => figurka.Hrac == hrac);
    }

    public IEnumerable<Figurka> ZjistiFigurkyProtihracu(Hrac hrac)
    {
        return figurkyNaPolicku.Where(figurka => figurka.Hrac != hrac);
    }

    public bool JeObsazeno()
    {
        return !dovolitViceFigurek && (figurkyNaPolicku.Count != 0);
    }

    public virtual void Vykresli()
    {
        Console.Write("[");
        foreach (var figurka in figurkyNaPolicku)
        {
            Console.Write(figurka.OznaceniFigurky);
        }
        Console.Write("]");
    }
}