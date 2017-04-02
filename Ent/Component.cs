using System;

namespace Ent {
	public abstract class Component {

		public class EntityChangedArgs : EventArgs {

			public Entity original, current;
			public EntityChangedArgs(Entity original, Entity current) {
				this.original = original;
				this.current = current;
			}

		}

		public event EventHandler<EntityChangedArgs> EntChanged;

		public string Name => GetType().Name;
		protected Entity ent;

		public Entity Entity {
			get { return ent; }
			set {
				Entity o = ent;
				ent = value;
				OnEntChanged(new EntityChangedArgs(o, ent));
			}
		}

		public override string ToString() { return Name + " : " + Entity; }

		public virtual void Destroy() { }

		protected virtual void OnEntChanged(EntityChangedArgs e) { EntChanged?.Invoke(this, e); }

	}
}
