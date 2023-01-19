using CloveceNezlobSe.Models.Figurky;
using CloveceNezlobSe.Services;

namespace CloveceNezlobSe.Models;

public class Policko
{
    protected HashSet<Figurka> figurkyNaPolicku = new();
    protected bool dovolitViceFigurek;
    protected HerniPlan herniPlan;

    public Policko(HerniPlan herniPlan, bool dovolitViceFigurek = false)
    {
        this.dovolitViceFigurek = dovolitViceFigurek;
        this.herniPlan = herniPlan;
    }

    public virtual void PolozFigurku(Figurka figurka)
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
        HashSet<Figurka> viditelneFigurky = figurkyNaPolicku.Where(f => f.Detectable).ToHashSet();
        if (viditelneFigurky.Count != 1)
        {
            throw new InvalidOperationException("Na políčku není jediná figurka.");
        }
			
        var figurka = viditelneFigurky.First();
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

    public IEnumerable<Figurka> ZjistiFigurkyProtihracu(Hrac? hrac)
    {
        return figurkyNaPolicku.Where(figurka => figurka.Hrac != hrac);
    }

    public bool JeObsazeno()
    {
        HashSet<Figurka> viditelneFigurky = figurkyNaPolicku.Where(f => f.Detectable).ToHashSet();
        return !dovolitViceFigurek && (viditelneFigurky.Count != 0);
    }

    public virtual void Vykresli(IWriter writer)
    {
        writer.SetBackgroundColor(ConsoleColor.Black);
		writer.Write("[");
        foreach (var figurka in figurkyNaPolicku)
        {
			writer.Write(figurka.OznaceniFigurky);
        }
		writer.Write("]");
    }
}