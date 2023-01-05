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
turnaj.PridejStrategii(new("MarketaP:HerniStrategiePreferujDomecekPakVyhazovani", hra => new CloveceNezlobSe.MarketaP.HerniStrategiePreferujDomecekPakVyhazovani(hra)));
turnaj.PridejStrategii(new("JanS:HerniStrategieHonza", hra => new CloveceNezlobSe.JanS.HerniStrategieHonza(hra)));
turnaj.PridejStrategii(new("BenS:HerniStrategieBenjaminSwart", hra => new CloveceNezlobSe.BenS.HerniStrategieBenjaminSwart(hra)));
turnaj.PridejStrategii(new("MartinF:HerniStrategieMartinF", hra => new CloveceNezlobSe.MartinF.HerniStrategieMartinF(hra, new CloveceNezlobSe.MartinF.HerniStrategieMartinFVahy())));
turnaj.PridejStrategii(new("ViacheslavN:HerniStrategieNahodne", hra => new HerniStrategieNahodne(hra)));
turnaj.PridejStrategii(new("ViacheslavN:HerniStrategieVyhodUtecJdi", hra => new HerniStrategieVyhodUtecJdi(hra)));
turnaj.PridejStrategii(new("Jenda:MojeStrategieJenda", hra => new MojeStrategieJenda(hra)));
turnaj.PridejStrategii(new("Jenda:MojeStrategieJenda2", hra => new MojeStrategieJenda2(hra)));
turnaj.PridejStrategii(new("JonasH:HerniStrategieJonasH", hra => new HerniStrategieJonasH(hra)));
turnaj.PridejStrategii(new("MaxJ:HerniStrategieMaxJAdvanced", hra => new HerniStrategieMaxJAdvanced(hra)));
turnaj.PridejStrategii(new("JanSl:StrategieSliva", hra => new StrategieSliva(hra)));
turnaj.PridejStrategii(new("RisaD:HerniStrategieRisa", hra => new HerniStrategieRisa(hra)));
turnaj.PridejStrategii(new("BenS:DiceMaster", hra => new CloveceNezlobSe.DiceMaster.HerniStrategieDiceMaster(hra)));
turnaj.PridejStrategii(new("KlaraF:HerniStrategieDomecekVyhazujPrvniKlaraF", hra => new HerniStrategieDomecekVyhazujPrvniKlaraF(hra)));
turnaj.PridejStrategii(new("Zbynek:HerniStrategiePreferujVyhru", hra => new HerniStrategiePreferujVyhru(hra)));
turnaj.PridejStrategii(new("Zbynek:HerniStrategiePreferujVyhruAVyhozeniNejlepsihoHrace", hra => new HerniStrategiePreferujVyhruAVyhozeniNejlepsihoHrace(hra)));

//turnaj.PridejStrategii(new("VojtaT:HerniStrategieVojtechTvrdik", hra => new HerniStrategieVojtechTvrdik(hra)));
//turnaj.PridejStrategii(new("Jenda:MojeStrategieJenda3", hra => new Jenda3(hra)));
//turnaj.PridejStrategii(new("Jenda:StrategieShitty", hra => new StrategieShitty(hra)));
//turnaj.PridejStrategii(new("ViacheslavN:HerniStrategiePoradSe", hra => new HerniStrategiePoradSe(hra)));
//turnaj.PridejStrategii(new("MartinF:HerniStrategieMartinF", hra => new CloveceNezlobSe.MartinF.HerniStrategieMartinF(hra, new CloveceNezlobSe.MartinF.HerniStrategieMartinFVahy())));

turnaj.Start();