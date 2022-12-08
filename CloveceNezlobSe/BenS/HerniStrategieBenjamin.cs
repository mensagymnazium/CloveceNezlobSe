namespace CloveceNezlobSe.BenS;

record struct Figurine(
	int Position,
	int? OwnId
)
{
	public bool IsOwn => OwnId != null;
}

record class GameView(List<Figurine> Figurines, int PathLength)
{
	public IEnumerable<Figurine> OwnFigurines => Figurines.Where(f => f.IsOwn);
	public IEnumerable<Figurine> OpponentFigurines => Figurines.Where(f => !f.IsOwn);
	public bool IsPositionValid(int position) => position >= 0 && position < PathLength;
	public bool IsPositionStart(int position) => position == 0;
	public bool IsPositionEnd(int position) => position == PathLength - 1;
	public bool IsPositionExclusive(int position) => !(IsPositionStart(position) || IsPositionEnd(position));
	public bool IsPositionOccupied(int position) => IsPositionExclusive(position) && Figurines.Any(f => f.Position == position);
	public bool IsPositionOccupiedBySelf(int position) => IsPositionExclusive(position) && Figurines.Any(f => f.Position == position && f.IsOwn);
	public bool IsPositionOccupiedByOpponent(int position) => IsPositionExclusive(position) && Figurines.Any(f => f.Position == position && !f.IsOwn);
}

class Strategy
{
	const int MaxHeatDistance = 8;

	public int GetPositionHeat(GameView game, int position)
	{
		return game.OpponentFigurines
			.Select(f => position - f.Position)
			.Where(d => d >= 1)
			.Select(d => MaxHeatDistance - d + 1)
			.Where(h => h >= 0)
			.Sum();
	}

	public int? FindMove(GameView game, int distance)
	{
		Figurine? figurine = game.OwnFigurines
			.Select(f => (from: f.Position, to: f.Position + distance, figurine: f))
			.Where(f => game.IsPositionValid(f.to))
			.Where(f => !game.IsPositionOccupiedBySelf(f.to))
			.OrderByDescending(f => game.IsPositionEnd(f.to))
			.ThenByDescending(f => game.IsPositionOccupiedByOpponent(f.to))
			.ThenBy(f => GetPositionHeat(game, f.to) - GetPositionHeat(game, f.from))
			.ThenByDescending(f => f.to)
			.Select(f => f.figurine as Figurine?)
			.FirstOrDefault();

		if (figurine.HasValue)
			return figurine.Value.OwnId!;
		else
			return null;
	}
}

public class HerniStrategieBenjaminSwart : HerniStrategie
{
	private Hra _hra;
	private Strategy _strategy;

	public HerniStrategieBenjaminSwart(Hra hra)
	{
		_hra = hra;
		_strategy = new Strategy();
	}

	public override Figurka? DejFigurkuKterouHrat(Hrac hrac, int hod)
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
			return hrac.Figurky[targetId.Value];
		}
	}
}
