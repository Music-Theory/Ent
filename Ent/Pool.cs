using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent {

	public struct EntEventArgs {

		public Entity ent;

		public EntEventArgs(Entity ent) { this.ent = ent; }

	}

	public struct ChangeEventArgs {

		public uint id;
		public bool added;
		public string comp;
		public ChangeEventArgs(uint id, string comp, bool added) {
			this.id = id;
			this.comp = comp;
			this.added = added;
		}

	}

	public class Pool : IObservable<Pool> {

		/// <summary>
		/// Fires when an entity is added. Shows the entity's new ID.
		/// </summary>
		public event EventHandler<EntEventArgs> EntAdded;
		/// <summary>
		/// Fires when an entity is removed. Shows the entity's old ID.
		/// </summary>
		public event EventHandler<EntEventArgs> EntRemoved;
		/// <summary>
		/// Fires when an entity is changed.
		/// </summary>
		public event EventHandler<ChangeEventArgs> EntChanged; 

		uint cKey = 1;

		public readonly Dictionary<uint, Entity> entities = new Dictionary<uint, Entity>();

		public Entity this[uint key] {
			get { return entities[key]; }
			set {
				if (value == null) {
					DestEnt(key);
					return;
				}
				if (cKey <= key) { cKey = key + 1; } else { DestEnt(key); }
				value.id = key;
				entities[key] = value;
				value.pool = this;
				value.AddedComp += OnCompAddedToEnt;
				value.RemovedComp += OnCompRemovedFromEnt;
				EntAdded?.Invoke(this, new EntEventArgs(value));
			}
		}

		void OnCompAddedToEnt(object o, ComponentEventArgs args) { EntChanged?.Invoke(o, new ChangeEventArgs(args.id, args.compName, true)); }
		void OnCompRemovedFromEnt(object o, ComponentEventArgs args) { EntChanged?.Invoke(o, new ChangeEventArgs(args.id, args.compName, false)); }

		public Entity MakeEnt() {
			Entity ent = new Entity();
			AddEnt(ent);
			return ent;
		}

		public bool Contains(uint id) { return entities.ContainsKey(id); }

		public void AddEnt(Entity ent) {
			//ent.id = cKey++;
			//entities.Add(ent.id, ent);
			//ent.pool = this;
			//EntAdded?.Invoke(this, new EntEventArgs(ent));
			this[cKey] = ent;
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
			DestEnt(ent.id);
		}

		public void DestEnt(uint id) {
			Entity ent = this[id];
			RemEnt(id);
			Entity.DestroyEntity(ent);
		}

		public IDisposable Subscribe(IObserver<Pool> observer) {
			throw new NotImplementedException();
		}

	}

}
