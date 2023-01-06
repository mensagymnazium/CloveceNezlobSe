namespace CloveceNezlobSe.Strategies.MartinF;

public record struct HerniStrategieMartinFVahy
{

	//{
	//VahaVyhozeniProtivnika = 2,0865463404797597,
	//VahaJduDoDomecku = 3,8039747321260746,
	//VahaRizikoVyhozeni = 0,058843773926126525,
	//VahaPreferujiVzdalenost = 2,2465957473694846,
	//VahaRizikoJeZamnouProtihrac = 0,40350433007934633
	//}
	public HerniStrategieMartinFVahy()
	{
	}

	/// <summary>
	/// Místo kam jdu, vyhodím protivníka
	/// Chci jít
	/// </summary>
	public double VahaVyhozeniProtivnika { get; set; } = 2.086;

	/// <summary>
	/// Místo kam jdu, je domeček
	/// Chci jít
	/// </summary>
	public double VahaJduDoDomecku { get; set; } = 3.803;

	/// <summary>
	/// Riziko, že se dostanu do pozice, kde jse snadné mě vyhodit
	/// Nechci jít
	/// </summary>
	public double VahaRizikoVyhozeni { get; set; } = 0.0588d;

	///// <summary>
	///// Figurka je nejdále na poli
	///// Chci jít
	///// </summary>
	//public double VahaFigurkaJePrvni  {get; set;} = 2.6d;

	/// <summary>
	/// Exponent násobitele vzdálenosti
	/// Chci jít
	/// </summary>
	public double VahaPreferujiVzdalenost { get; set; } = 2.246d;

	/// <summary>
	/// Riziko, že budu vyhozen, pokud se nepohnu
	/// Chci jít?
	/// </summary>
	public double VahaRizikoJeZamnouProtihrac { get; set; } = 0.403d;
}