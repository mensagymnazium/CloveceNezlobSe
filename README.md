# Člověče, nezlob se! 

Dnes si (asi naposledy) pohrajeme s *Člověče, nezlob se!*
Máme posbíraných mnoho strategií (dnes se omezme na ty konvenční, vynecháme "no-limit" řešení z poslední předvánoční hodiny).

Cílem dnešního dne je udělat celou hru zajímavější, pohrát si s dalšími možnostmi oživení hry, popř. otestovat v různých situacích připravené strategie (optimalizovat strategie pro nové situace).

## Implementujte nové typy figurek:
1. **Tank** - co přejede, to je mrtvé (vyhazuje i přeskakováním). Tank nemůže být vyhozen ze hry.
1. **Vrtulník** - může skončit na libovolném políčku bližším, než je hozeno.

## Implementujte nové typy políček:
1. **Žumpa.** Kdo stoupne na žumpu, vrací se na start.
1. **Trampolína.** Kdo stoupne na trampolínu, může hrát ještě jednou.
1. **Nárazník.** Kdo stoupne na nárazník, vrací se zpět o poloviční počet políček.
1. **Schovka.** Kdo stojí na schovce, nemůže být vyhozen. Na schovce může stát více figurek.
1. **Upgrade.** Kdo stoupne obyčejnou figurkou na upgrade, může ji změnit na tank/vrtulník/...

## Implementujte nové typy kostky:
1. **Kostka se zápornými hodnotami.** Kostka může mít vedle kladných i záporné hodnoty.

## Hra
* vygenerujte náhodně velikost hracího plánu (v rozsahu 10-100 políček).
* vygenerujte náhodně typy políček v plánu (rozmístění), počty různých typů můžete určit dle velikosti plánu
* navrhněte vhodný mix pro typy figurek. Např. 4 normální, 1 tank, 1 vrtulník. 
* zvažte variace pravidel samotných, např. při hození lichého čísla se jde zpět místo vpřed, atp.

Nemusíte implementovat vše. Z výše uvedeného si vyberte, co Vás bude bavit, co se Vám bude dobře implementovat, popř. vymyslete vlastní kreativní variace hry.

Myslete i na strategie, úpravy hry společně s optimalizací vaší strategie by vám měly pomoci vyhrát (ve srovnání s se strategiemi, které neznají nové prvky).

