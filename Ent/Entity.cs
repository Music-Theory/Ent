using System;
using System.Collections.Generic;
using System.Linq;

namespace Ent {
	
	public class ComponentEventArgs : EventArgs {

		public uint id;
		public string compName;

		public ComponentEventArgs(uint id, string compName) {
			this.id = id;
			this.compName = compName;
		}
	}

	public class Entity {

		public event EventHandler<ComponentEventArgs> AddedComp;
		public event EventHandler<ComponentEventArgs> RemovedComp;
		public event EventHandler Destroyed;

		internal Pool pool;

		public Pool Pool {
			get { return pool; }
			set {
				pool?.RemEnt(this);
				value?.AddEnt(this);
				this.pool = value;
			}
		}

		readonly Dictionary<string, Component> components = new Dictionary<string, Component>();

		internal uint id = 0;

		public uint ID => id;

		public Entity() { this.id = 0; }

		static internal void DestroyEntity(Entity ent) {
			List<string> toRemove = ent.components.Select(comp => comp.Key).ToList();
			foreach (string key in toRemove) {
				Component comp = ent[key];
				ent.Rem(key);
				comp.Destroy();
			}
			ent.pool = null;
			ent.Destroyed?.Invoke(ent, EventArgs.Empty);
		}

		public bool Contains(string compName) { return components.ContainsKey(compName); }

		public void Add(Component comp) {
			if (comp == null) { return; }
			components.Add(comp.Name, comp);
			comp.Entity = this;
			AddedComp?.Invoke(this, new ComponentEventArgs(id, comp.Name));
		}

		public bool Rem(string name) {
			if (!components.ContainsKey(name)) { return false; }
			RemovedComp?.Invoke(this, new ComponentEventArgs(id, name));
			components[name].Entity = null;
			components.Remove(name);
			return true;
		}

		public void AddRange(IEnumerable<Component> comps) {
			foreach (Component comp in comps) {
				Add(comp);
			}
		}

		public Component this[string key] {
			get {
				if (!components.ContainsKey(key)) { return null; }
				return components[key];
			}
		}

		public override string ToString() { return ID.ToString(); }

	}

}