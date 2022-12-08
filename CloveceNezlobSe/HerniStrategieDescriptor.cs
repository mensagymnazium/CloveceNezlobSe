using CloveceNezlobSe;

public class HerniStrategieDescriptor
{
	public string Name { get; private set; }
	public Func<Hra, HerniStrategie> Activator { get; private set; }

	public HerniStrategieDescriptor(string name, Func<Hra, HerniStrategie> activator)
	{
		Activator = activator;
		Name = name;
	}
}