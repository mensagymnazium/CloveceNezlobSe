using CloveceNezlobSe.Models;
using CloveceNezlobSe.Services;
using CloveceNezlobSe.Strategies;
using CloveceNezlobSe.Strategies.Ai;

var writer = new ConsoleWriter();
var hra = new Hra(
    new LinearniHerniPlan(30, writer),
    writer);

hra.PridejHrace(new ("AI", new Gpt4SlopStrategy(hra)));
hra.PridejHrace(new ("Prvni moznou", new HerniStrategieTahniPrvniMoznouFigurkou(hra)));

hra.NastavNahodnePoradiHracu();
hra.Start(writer);