namespace CloveceNezlobSe.Strategies.DiceMaster;

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
    public int? FindMove(GameView game, int distance)
    {
        Figurine? figurine = game.OwnFigurines
            .Select(f => (from: f.Position, to: f.Position + distance, figurine: f))
            .Where(f => game.IsPositionValid(f.to))
            .Where(f => !game.IsPositionOccupiedBySelf(f.to))
            .OrderByDescending(f => game.IsPositionEnd(f.to))
            .ThenByDescending(f => f.to)
            .Select(f => f.figurine as Figurine?)
            .FirstOrDefault();

        if (figurine.HasValue)
        {
            Figurine figurineValue = figurine.Value;
            figurineValue.Position += distance;
            game.Figurines[figurine.Value.OwnId!.Value] = figurineValue;
        }

        Figurine? nextFigurine = game.OwnFigurines
            .Where(f => !game.IsPositionEnd(f.Position))
            .OrderByDescending(f => f.Position)
            .FirstOrDefault();

        int nextDistance = 6;

        if (nextFigurine.HasValue)
        {
            nextDistance = game.PathLength - nextFigurine.Value.Position - 1;
            if (nextDistance > 6)
            {
                nextDistance = 6;
            }
        }

        InfluenceRandom(new List<int>() { 1, nextDistance });

        if (figurine.HasValue)
            return figurine.Value.OwnId!;
        else
            return null;
    }

    private void InfluenceRandom(List<int> target)
    {
        Xoshiro random = RandomReverser.CopySharedRandom();
        int count = 0;

        List<int> values = new();

        while (!values.SequenceEqual(target))
        {
            values.Add(random.Next(1, 7));
            count++;

            if (values.Count > target.Count)
            {
                values.RemoveAt(0);
            }
        }

        count -= target.Count;

        for (int i = 0; i < count; i++)
        {
            Random.Shared.Next(1, 7);
        }
    }
}
