using CloveceNezlobSe;
using CloveceNezlobSe.VlastniHerniStrategie;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var turnaj = new TurnajStrategii();

turnaj.PridejStrategii(new("HerniStrategieMartinF", hra => new HerniStrategieMartinF(hra, new HerniStrategieMartinFVahy())));
turnaj.PridejStrategii(new("HerniStrategiePreferujVyhazovaniJinakPrvniMoznou", hra => new HerniStrategiePreferujVyhazovaniJinakPrvniMoznou(hra)));
turnaj.PridejStrategii(new("HerniStrategieTahniPrvniMoznouFigurkou", hra => new HerniStrategieTahniPrvniMoznouFigurkou(hra)));

turnaj.Start();