using CloveceNezlobSe;
using CloveceNezlobSe.VlastniHerniStrategie;


var tester = new HerniStrategieTester();
var prefabrikatHry = new Hra(new LinearniHerniPlan(40));
prefabrikatHry.PridejHrace(new Hrac("Martin:custom",new HerniStrategieMartinF(prefabrikatHry,new HerniStrategieMartinFVahy())));
prefabrikatHry.PridejHrace(new Hrac("Robert:seberJinakPrvni",new HerniStrategiePreferujVyhazovaniJinakPrvniMoznou(prefabrikatHry)));
tester.Test(5000,prefabrikatHry);