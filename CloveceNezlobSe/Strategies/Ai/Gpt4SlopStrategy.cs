using Azure.AI.OpenAI;
using CloveceNezlobSe.Models;
using CloveceNezlobSe.Models.Boards;
using CloveceNezlobSe.Models.Figurky;
using OpenAI.Chat;
using System.Text;

namespace CloveceNezlobSe.Strategies.Ai;

public class Gpt4SlopStrategy()
	: HerniStrategie
{
	public readonly Hra hra = null!;
	private readonly ChatClient chatClient = null!;

	public Gpt4SlopStrategy(Hra hra)
		: this()
	{
		this.hra = hra;
		
		AzureOpenAIClient azureClient = new(
			new Uri("https://hakenoai.openai.azure.com"),
			new System.ClientModel.ApiKeyCredential("...")); // <- key is invalidated now, sry

		this.chatClient = azureClient.GetChatClient("gpt-4o");
	}

	public override HerniRozhodnuti? DejHerniRozhodnuti(Hrac hrac, int hod, IHerniInformace informace)
	{
		var plan = PlanToString(hra.HerniPlan);
		var hratelneFigurky = HratelneFigurky(hrac, hod, informace);

		var prompt = 
			$"""
			The dice rolled: {hod}
			{plan}
			{hratelneFigurky.prompt}
			""";

		Console.WriteLine(
			$"""

			Giving prompt:
			{prompt}
			
			""");

		var completion = chatClient.CompleteChat([
			new SystemChatMessage(
				"You are a player of Ludo. Choose what figure you are going to" +
				" move based on the board data. The board is linear with start at place 0" +
				" and end at final place. You will be given a board as a string of places." +
				" Only answer a single number from available move options." +
				" Your figures start with A (in format A%index%)." +
				" If you can, try to take out enemy figures."
			),
			new UserChatMessage(prompt)
		]);

		var result = completion.Value.Content[0].Text;
		var move = int.TryParse(result, out var moveI)
			? hratelneFigurky.playOptions.FirstOrDefault(po => po.index == moveI).figurka
			: null;

		if (move is null)
		{
			Console.WriteLine($"Not valid move result was: {result}");
			return null;
		}

		Console.WriteLine(
			$"""
			
			Magic AI Says:
			{moveI}

			""");

		var selectedOption = hratelneFigurky.playOptions.FirstOrDefault(po => po.index == moveI);
		return new()
		{
			Figurka = selectedOption.figurka
		};
	}

	private (string prompt, (int index, Figurka figurka)[] playOptions) HratelneFigurky(Hrac hrac, int hod, IHerniInformace informace)
	{
		var hratelneFigurky = hrac.Figurky.Where(f => !f.JeVDomecku());
		var sb = new StringBuilder();

		sb.AppendLine("Playable options: ");
		Models.Figurky.Figurka[] hfarr = hratelneFigurky.ToArray();
		
		var playOptions = new (int index, Figurka figurka)[hfarr.Length];
		for (int i = 0; i < hfarr.Length; i++)
		{
			Models.Figurky.Figurka? f = hfarr[i];
			var cilovePolicko = informace.ZjistiCilovePolicko(f, hod);
			if (cilovePolicko is null)
			{
				// Not valid move
				continue;
			}

			var enemyFigurky = cilovePolicko.ZjistiFigurkyProtihracu(hrac);
			var isTakeout = enemyFigurky.Any();

			var fromI = hra.HerniPlan.Policka
				.Select((p, i) => (p, i))
				.FirstOrDefault(t => t.p.figurkyNaPolicku.Contains(f)).i
				.ToString();

			var toI = hra.HerniPlan.Policka
				.Select((p, i) => (p, i))
				.FirstOrDefault(t => t.p == cilovePolicko).i
				.ToString();

			sb.Append($"{i}: {f.OznaceniFigurky} from {fromI} to {toI}");
			if (isTakeout)
			{
				sb.Append(" (takeout)");
			}
			sb.AppendLine();
			playOptions[i] = (i, f);
		}

		return (sb.ToString(), playOptions);
	}

	private string PlanToString(HerniPlan herniPlan)
	{
		var sb = new StringBuilder();
		sb.AppendLine("Board state: ");
		for (int i = 0; i < herniPlan.Policka.Count; i++)
		{
			Policko? p = herniPlan.Policka[i];
			
			var figurkyNaPolicku = p.figurkyNaPolicku;
			sb
				.Append(i)
				.Append(":[")
				.Append(string.Join(", ", figurkyNaPolicku.Select(f => f.OznaceniFigurky)))
				.Append("] ");
		}
		return sb.ToString();
	}

}