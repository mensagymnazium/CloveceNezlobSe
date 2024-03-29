﻿using CloveceNezlobSe.Models.Boards;
using CloveceNezlobSe.Models.Figurky;
using CloveceNezlobSe.Strategies;

namespace CloveceNezlobSe.Models
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

			for (int i = 0; i < 3; i++)
			{
				figurky.Add(new Figurka(this, $"{this.Jmeno.Substring(0, 1)}{(i + 1)}"));
			}
			figurky.Add(new Tank(this, $"{this.Jmeno.Substring(0, 1)}{(4)}"));
		}

		public void ZamenFigurku(Figurka stara, Figurka nova) {
			this.figurky[this.figurky.IndexOf(stara)] = nova;
		}

		public bool MaVsechnyFigurkyVDomecku()
		{
			return figurky.All(figurka => figurka.JeVDomecku());
		}

		public HerniRozhodnuti? DejHerniRozhodnuti(int hod, IHerniInformace informace)
		{
			return herniStrategie.DejHerniRozhodnuti(this, hod, informace);
		}
	}
}
