﻿using CloveceNezlobSe.Services;

namespace CloveceNezlobSe.Models
{
	public abstract class TurnajBase
	{

		protected List<HerniStrategieDescriptor> herniStrategie = new();
		public int PocetHer { get; init; } = 5000;
		public int VelikostHernihoPlanu { get; init; } = 40;
		public IWriter Writer { get; init; } = new NullWriter();

		public void PridejStrategii(HerniStrategieDescriptor strategieDescriptor)
		{
			herniStrategie.Add(strategieDescriptor);
		}

		public abstract void Start();
	}
}