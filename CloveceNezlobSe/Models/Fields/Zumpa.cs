namespace CloveceNezlobSe.Models;

public class Zumpa : Policko
{
    public Zumpa(HerniPlan herniPlan, bool dovolitViceFigurek = false) : base(herniPlan, dovolitViceFigurek)
    {
    }

    public override void Vykresli(Services.IWriter writer)
    {
		var originalColor = writer.GetForegroundColor();
		writer.SetForegroundColor(ConsoleColor.DarkGreen);
		writer.Write("[]");
		writer.SetForegroundColor(originalColor);
	}
}