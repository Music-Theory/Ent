using System.Collections.Generic;

namespace Ent {

	public class Entity {

		readonly Dictionary<string, Component> components = new Dictionary<string, Component>();

		public void AddComponent(Component comp) { components.Add(comp.name, comp); }
		public void RemComponent(string name) { components.Remove(name); }

		public void AddComponents(List<Component> comps) {
			if (comps != null) {
				foreach (Component comp in comps) {
					AddComponent(comp);
				}
			}
		}

		public Component GetComponent(string name) { return components.ContainsKey(name) ? components[name] : null; }

		// That's all, folks
	}

}