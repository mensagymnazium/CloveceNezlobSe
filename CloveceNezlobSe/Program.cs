using CloveceNezlobSe;
using CloveceNezlobSe.JonasH;
using CloveceNezlobSe.VlastniHerniStrategie;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var turnaj = new TurnajDvouher()
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
turnaj.PridejStrategii(new("ViacheslavN:HerniStrategiePoradSe", hra => new HerniStrategiePoradSe(hra)));
turnaj.PridejStrategii(new("Jenda:MojeStrategieJenda", hra => new MojeStrategieJenda(hra)));
turnaj.PridejStrategii(new("Jenda:MojeStrategieJenda2", hra => new MojeStrategieJenda2(hra)));
turnaj.PridejStrategii(new("JonasH:HerniStrategieJonasH", hra => new HerniStrategieJonasH(hra)));
turnaj.PridejStrategii(new("MaxJ:HerniStrategieMaxJAdvanced", hra => new HerniStrategieMaxJAdvanced(hra)));

//turnaj.PridejStrategii(new("Jenda:MojeStrategieJenda3", hra => new Jenda3(hra)));
//turnaj.PridejStrategii(new("Jenda:StrategieShitty", hra => new StrategieShitty(hra)));

turnaj.Start();