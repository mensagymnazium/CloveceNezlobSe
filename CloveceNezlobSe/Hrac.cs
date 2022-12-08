namespace CloveceNezlobSe
{
    public class Hrac
	{
		public string Jmeno { get; private set; }

		private List<Figurka> figurky = new();
		public IReadOnlyList<Figurka> Figurky => figurky.AsReadOnly();
		

		private HerniStrategie herniStrategie;

		public Hrac(string jmeno, HerniStrategie herniStrategie)
		{
			this.Jmeno = jmeno;
			this.herniStrategie = herniStrategie;

			for (int i = 0; i < 4; i++)
			{
				figurky.Add(new Figurka(this, $"{this.Jmeno.Substring(0,1)}{(i + 1)}"));
			}
		}

		public bool MaVsechnyFigurkyVDomecku()
		{
			return figurky.All(figurka => figurka.JeVDomecku());
		}

		public Figurka? DejFigurkuKterouHrat(int hod)
		{
			return herniStrategie.DejFigurkuKterouHrat(this, hod);
		}
	}
}
