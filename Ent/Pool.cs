using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent {

	public class EntEventArgs {

		public Entity ent;

		public EntEventArgs(Entity ent) { this.ent = ent; }

	}

	public delegate void EntAddedHandler(Pool sender, EntEventArgs e);

	public delegate void EntRemovedHandler(Pool sender, EntEventArgs e);

	public class Pool {

		/// <summary>
		/// Fires when an entity is added. Shows the entity's new ID.
		/// </summary>
		public event EntAddedHandler EntAdded;
		/// <summary>
		/// Fires when an entity is removed. Shows the entity's old ID.
		/// </summary>
		public event EntRemovedHandler EntRemoved;

		uint cKey = 1;

		public readonly Dictionary<uint, Entity> entities = new Dictionary<uint, Entity>();

		public Entity Get(uint key) { return entities[key]; }

		public Entity this[uint key] => Get(key);

		public Entity MakeEnt() {
			Entity ent = new Entity();
			this.AddEnt(ent);
			return ent;
		}

		public void AddEnt(Entity ent) {
			ent.id = cKey++;
			entities.Add(ent.id, ent);
			ent.pool = this;
			EntAdded?.Invoke(this, new EntEventArgs(ent));
		}

		public bool RemEnt(Entity ent) { return RemEnt(ent.id); }

		public bool RemEnt(uint id) {
			if (!entities.ContainsKey(id)) { return false; }
			Entity ent = entities[id];
			entities.Remove(id);
			EntRemoved?.Invoke(this, new EntEventArgs(ent));
			ent.id = 0;
			ent.pool = null;
			return true;
		}

		public void DestEnt(Entity ent) {
			RemEnt(ent);
			Entity.DestroyEntity(ent);
		}

	}

}
