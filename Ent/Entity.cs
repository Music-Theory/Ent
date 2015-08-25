using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ent.Utility;

namespace Ent {
	public class Entity {

		Dictionary<string, Component> components = new Dictionary<string, Component>();

		public void addComponent(Component comp) { this.components.Add(comp.name, comp); }
		public void remComponent(string name) { components.Remove(name); }

		public void addComponents(List<Component> comps) {
			if (comps != null) {
				foreach (Component comp in comps) {
					this.addComponent(comp);
				}
			}
		}

		public Component getComponent(string name) { return components.ContainsKey(name) ? components[name] : null; }

		// That's all, folks
	}
}
