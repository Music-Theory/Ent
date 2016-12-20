namespace Ent {
	public abstract class Component {

		public string Name => GetType().Name;
		protected Entity ent;

		public Entity Entity {
			get { return ent; }
			set { ent = value; }
		}

		public override string ToString() { return Name + " : " + Entity; }

	}
}
