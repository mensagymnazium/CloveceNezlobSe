namespace CloveceNezlobSe.Models
{
	public abstract class HerniPlan
	{
		public abstract IReadOnlyList<Policko> Policka { get; }
		
		public abstract int MaximalniPocetHracu { get; }

		public abstract void DejFigurkuNaStartovniPolicko(Figurka figurka);
		
		public abstract Policko? ZjistiCilovePolicko(Figurka figurka, int hod);

		public abstract bool MuzuTahnout(Figurka figurka, int hod);

		public abstract void HrajTahHrace(Hrac hrac, IKostka kostka);

		public abstract void VycistiPolicko(Policko policko);

		public abstract void Vykresli();
	}
}