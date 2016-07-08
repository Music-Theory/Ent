using System;
using System.Collections.Generic;
using System.Linq;

namespace Ent {
	
	public class ComponentEventArgs : EventArgs {
		public string compName;
		public ComponentEventArgs(string compName) { this.compName = compName; }
	}

	public delegate void AddedCompHandler(Entity sender, ComponentEventArgs e);
	public delegate void RemovedCompHandler(Entity sender, ComponentEventArgs e);
	public delegate void DestroyedEventHandler(Entity sender, EventArgs e);

	public class Entity {

		public event AddedCompHandler AddedComp;
		public event RemovedCompHandler RemovedComp;
		public event DestroyedEventHandler Destroyed;

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

		internal Entity() { this.id = 0; }

		internal static void DestroyEntity(Entity ent) {
			List<string> toRemove = ent.components.Select(comp => comp.Key).ToList();
			foreach (string key in toRemove) { ent.Rem(key); }
			ent.pool = null;
			ent.Destroyed?.Invoke(ent, EventArgs.Empty);
		}

		public void Add(Component comp) {
			components.Add(comp.Name, comp);
			comp.entity = ID;
			AddedComp?.Invoke(this, new ComponentEventArgs(comp.Name));
		}

		public bool Rem(string name) {
			if (!components.ContainsKey(name)) { return false; }
			RemovedComp?.Invoke(this, new ComponentEventArgs(name));
			components[name].entity = 0;
			components.Remove(name);
			return true;
		}

		public void AddRange(IEnumerable<Component> comps) {
			foreach (Component comp in comps) {
				Add(comp);
			}
		}

		public Component Get(string name) { return components[name]; }

		public Component this[string key] => Get(key);

		public override string ToString() { return ID.ToString(); }

	}

}