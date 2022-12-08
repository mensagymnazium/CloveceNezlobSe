using CloveceNezlobSe;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var turnaj = new TurnajStrategii()
{
	VelikostHernihoPlanu = 40,
	PocetHer = 1000
};

turnaj.PridejStrategii(new("HerniStrategieTahniPrvniMoznouFigurkou", hra => new HerniStrategieTahniPrvniMoznouFigurkou(hra)));
turnaj.PridejStrategii(new("HerniStrategiePreferujVyhazovaniJinakPrvniMoznou", hra => new HerniStrategiePreferujVyhazovaniJinakPrvniMoznou(hra)));
turnaj.PridejStrategii(new("MartinF:HerniStrategieMartinF", hra => new CloveceNezlobSe.MartinF.HerniStrategieMartinF(hra, new CloveceNezlobSe.MartinF.HerniStrategieMartinFVahy())));
turnaj.PridejStrategii(new("MarketaP:HerniStrategiePreferujDomecekPakVyhazovani", hra => new CloveceNezlobSe.MarketaP.HerniStrategiePreferujDomecekPakVyhazovani(hra)));
turnaj.PridejStrategii(new("JanS:HerniStrategieHonza", hra => new CloveceNezlobSe.JanS.HerniStrategieHonza(hra)));
turnaj.PridejStrategii(new("BenS:HerniStrategieBenjaminSwart", hra => new CloveceNezlobSe.BenS.HerniStrategieBenjaminSwart(hra)));
turnaj.PridejStrategii(new("MartinF:HerniStrategieMartinFOptimized", hra => new CloveceNezlobSe.MartinF.HerniStrategieMartinFOptimized(hra, new CloveceNezlobSe.MartinF.HerniStrategieMartinFVahy())));
turnaj.PridejStrategii(new("ViacheslavN:HerniStrategieNahodne", hra => new HerniStrategieNahodne(hra)));
turnaj.PridejStrategii(new("ViacheslavN:HerniStrategieVyhodUtecJdi", hra => new HerniStrategieVyhodUtecJdi(hra)));

turnaj.Start();