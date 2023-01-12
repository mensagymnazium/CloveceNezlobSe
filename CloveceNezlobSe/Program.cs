using CloveceNezlobSe.Models;
using CloveceNezlobSe.Strategies;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var turnaj = new TurnajDvouher()
{
    VelikostHernihoPlanu = 30,
    PocetHer = 100,
	DisableConsoleDuringGame = true
};

turnaj.PridejStrategii(new("HerniStrategieTahniPrvniMoznouFigurkou", hra => new HerniStrategieTahniPrvniMoznouFigurkou(hra)));
turnaj.PridejStrategii(new("HerniStrategiePreferujVyhazovaniJinakPrvniMoznou", hra => new HerniStrategiePreferujVyhazovaniJinakPrvniMoznou(hra)));
turnaj.PridejStrategii(new("MarketaP:HerniStrategiePreferujDomecekPakVyhazovani", hra => new CloveceNezlobSe.Strategies.MarketaP.HerniStrategiePreferujDomecekPakVyhazovani(hra)));
turnaj.PridejStrategii(new("JanS:HerniStrategieHonza", hra => new CloveceNezlobSe.Strategies.JanS.HerniStrategieHonza(hra)));
turnaj.PridejStrategii(new("BenS:HerniStrategieBenjaminSwart", hra => new CloveceNezlobSe.Strategies.BenS.HerniStrategieBenjaminSwart(hra)));
turnaj.PridejStrategii(new("MartinF:HerniStrategieMartinF", hra => new CloveceNezlobSe.Strategies.MartinF.HerniStrategieMartinF(hra, new CloveceNezlobSe.Strategies.MartinF.HerniStrategieMartinFVahy())));
turnaj.PridejStrategii(new("ViacheslavN:HerniStrategieNahodne", hra => new CloveceNezlobSe.Strategies.ViacheslavN.HerniStrategieNahodne(hra)));
turnaj.PridejStrategii(new("ViacheslavN:HerniStrategieVyhodUtecJdi", hra => new CloveceNezlobSe.Strategies.ViacheslavN.HerniStrategieVyhodUtecJdi(hra)));
turnaj.PridejStrategii(new("Jenda:MojeStrategieJenda", hra => new CloveceNezlobSe.Strategies.Jenda.MojeStrategieJenda(hra)));
turnaj.PridejStrategii(new("Jenda:MojeStrategieJenda2", hra => new CloveceNezlobSe.Strategies.Jenda.MojeStrategieJenda2(hra)));
turnaj.PridejStrategii(new("JonasH:HerniStrategieJonasH", hra => new CloveceNezlobSe.Strategies.JonasH.HerniStrategieJonasH(hra)));
turnaj.PridejStrategii(new("MaxJ:HerniStrategieMaxJAdvanced", hra => new CloveceNezlobSe.Strategies.MaxJ.HerniStrategieMaxJAdvanced(hra)));
turnaj.PridejStrategii(new("JanSl:StrategieSliva", hra => new CloveceNezlobSe.Strategies.JanSl.StrategieSliva(hra)));
turnaj.PridejStrategii(new("RisaD:HerniStrategieRisa", hra => new CloveceNezlobSe.Strategies.RisaD.HerniStrategieRisa(hra)));
turnaj.PridejStrategii(new("BenS:DiceMaster", hra => new CloveceNezlobSe.Strategies.DiceMaster.HerniStrategieDiceMaster(hra)));
turnaj.PridejStrategii(new("KlaraF:HerniStrategieDomecekVyhazujPrvniKlaraF", hra => new CloveceNezlobSe.Strategies.KlaraF.HerniStrategieDomecekVyhazujPrvniKlaraF(hra)));
turnaj.PridejStrategii(new("Zbynek:HerniStrategiePreferujVyhruAVyhozeniNejlepsihoHrace", hra => new CloveceNezlobSe.Strategies.Zbynek.HerniStrategiePreferujVyhruAVyhozeniNejlepsihoHrace(hra)));
turnaj.PridejStrategii(new("VojtaT:HerniStrategieVojtechTvrdik", hra => new CloveceNezlobSe.Strategies.VojtaT.HerniStrategieVojtechTvrdik(hra)));

//turnaj.PridejStrategii(new("Zbynek:HerniStrategiePreferujVyhru", hra => new HerniStrategiePreferujVyhru(hra)));
//turnaj.PridejStrategii(new("Jenda:MojeStrategieJenda3", hra => new Jenda3(hra)));
//turnaj.PridejStrategii(new("Jenda:StrategieShitty", hra => new StrategieShitty(hra)));
//turnaj.PridejStrategii(new("ViacheslavN:HerniStrategiePoradSe", hra => new HerniStrategiePoradSe(hra)));
//turnaj.PridejStrategii(new("MartinF:HerniStrategieMartinF", hra => new CloveceNezlobSe.MartinF.HerniStrategieMartinF(hra, new CloveceNezlobSe.MartinF.HerniStrategieMartinFVahy())));

turnaj.Start();