namespace CloveceNezlobSe
{
	public abstract class TurnajBase
	{

		protected List<HerniStrategieDescriptor> herniStrategie = new();
		public int PocetHer { get; init; } = 5000;
		public int VelikostHernihoPlanu { get; init; } = 40;

		public void PridejStrategii(HerniStrategieDescriptor strategieDescriptor)
		{
			herniStrategie.Add(strategieDescriptor);
		}

		public abstract void Start();
	}
}