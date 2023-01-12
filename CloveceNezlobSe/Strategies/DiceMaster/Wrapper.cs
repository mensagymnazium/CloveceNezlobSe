using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;

namespace CloveceNezlobSe.Strategies.DiceMaster;

public class HerniStrategieDiceMaster : HerniStrategie
{
    private Hra _hra;
    private Strategy _strategy;

    public HerniStrategieDiceMaster(Hra hra)
    {
        _hra = hra;
        _strategy = new Strategy();
    }

	public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
	{
		if (hrac.Figurky.Count <= 0)
        {
            return null;
        }

        Figurka figurine = hrac.Figurky[0];
        Policko middle = figurine.Policko!;

        List<Policko> fields = new();

        Policko lastField = middle;
        for (int offset = 1; _hra.HerniPlan.ZjistiCilovePolicko(figurine, -offset) != lastField; offset++)
        {
            Policko field = _hra.HerniPlan.ZjistiCilovePolicko(figurine, -offset)!;
            fields.Add(field);
            lastField = field;
        }

        fields.Reverse();

        for (int offset = 0; _hra.HerniPlan.ZjistiCilovePolicko(figurine, offset) != null; offset++)
        {
            Policko field = _hra.HerniPlan.ZjistiCilovePolicko(figurine, offset)!;
            fields.Add(field);
        }

        List<Figurine> figurines = new();

        for (int id = 0; id < hrac.Figurky.Count; id++)
        {
            figurines.Add(new Figurine(fields.IndexOf(hrac.Figurky[id].Policko!), id));
        }

        for (int position = 0; position < fields.Count; position++)
        {
            foreach (Figurka opponent in fields[position].ZjistiFigurkyProtihracu(hrac))
            {
                figurines.Add(new Figurine(position, null));
            }
        }

        int? targetId = _strategy.FindMove(new GameView(figurines, fields.Count), hod);

        if (!targetId.HasValue)
        {
            return null;
        }
        else
        {
            return new HerniRozhodnuti() { Figurka = hrac.Figurky[targetId.Value] };
        }
    }
}
