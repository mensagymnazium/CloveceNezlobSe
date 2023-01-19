namespace CloveceNezlobSe.Strategies.MartinF;

public record struct HerniStrategieMartinFVahyTurboSmurf
{
    //{ VahaVyhozeniProtivnika = 2.168093189399041, VahaJduDoDomecku = 3.576040055451859, VahaRizikoVyhozeni = 0.1252135910797434, VahaPreferujiVzdalenost = 2.7129007748022995, VahaJeZamnouProtihrac = 0.3511830509461851, VahaJduNaNaraznik = 0.4171353045047547, ThresholdPodKteryNehraju = 0.282276113908418 }
    //{ VahaVyhozeniProtivnika = 3.69919747431525, VahaJduDoDomecku = 4.531963965779127, VahaRizikoVyhozeni = 0.2764019305968857, VahaPreferujiVzdalenost = 3.955984914490582, VahaJeZamnouProtihrac = 0.9618506213237696, VahaJduNaNaraznik = 0.3449522701085842, ThresholdPodKteryNehraju = 0.30207347672459955 }
    public HerniStrategieMartinFVahyTurboSmurf()
	{
	}

	/// <summary>
	/// Místo kam jdu, vyhodím protivníka
	/// </summary>
	public double VahaVyhozeniProtivnika { get; set; } = 2.086;

	/// <summary>
	/// Místo kam jdu, je domeček
	/// </summary>
	public double VahaJduDoDomecku { get; set; } = 3.803;

	/// <summary>
	/// Riziko, že se dostanu do pozice, kde jse snadné mě vyhodit
	/// </summary>
	public double VahaRizikoVyhozeni { get; set; } = 0.0588d;

	///// <summary>
	///// Figurka je nejdále na poli
	///// </summary>
	//public double VahaFigurkaJePrvni  {get; set;} = 2.6d;

	/// <summary>
	/// Násobitel vzdálenosti
	/// </summary>
	public double VahaPreferujiVzdalenost { get; set; } = 2.246d;

	/// <summary>
	/// Riziko, že budu vyhozen, pokud se nepohnu
	/// </summary>
	public double VahaJeZamnouProtihrac { get; set; } = 0.403d;

    /// <summary>
	/// Místo kam jdu, je nárazník
	/// </summary>
	public double VahaJduNaNaraznik { get; set; } = 0.403d;

    /// <summary>
    /// Pokud má figurka váhu pod tento threshold, nemela by být započtena jako hratelná
    /// </summary>
    public double ThresholdPodKteryNehraju { get; set; } = 0.2d;

}