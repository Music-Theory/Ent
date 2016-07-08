namespace Ent {
	public abstract class Component {

		public string Name => GetType().Name;
		protected uint ent;

		public uint entity {
			get { return GetEnt(); }
			set { SetEnt(value); }
		}

		protected abstract void SetEnt(uint val);
		protected virtual uint GetEnt() { return ent; }

		public override string ToString() { return Name + " : " + entity; }

	}
}
