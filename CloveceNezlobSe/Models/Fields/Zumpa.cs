﻿namespace CloveceNezlobSe.Models;

public class Zumpa : Policko
{
    public Zumpa(HerniPlan herniPlan, bool dovolitViceFigurek = false) : base(herniPlan, dovolitViceFigurek)
    {
    }

    public override void Vykresli()
    {
        Console.Write("< Žumpa >");
    }
}