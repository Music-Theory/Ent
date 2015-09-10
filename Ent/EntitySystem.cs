using System.Collections.Generic;

namespace Ent {
	public static class EntitySystem {

		public static readonly Dictionary<int, Entity> ENTITIES_MASTER = new Dictionary<int, Entity>();

		static int currentKey = 1;

		public static int MakeEntity() {
			ENTITIES_MASTER.Add(currentKey, new Entity());
			currentKey++;
			return currentKey - 1;
		}

		public static int MakeEntity<T>(int key, Dictionary<int, T> dict, T ent) {
			dict.Add(key, ent);
			key++;
			return key - 1;
		}

		public static void RemoveEntity(int key) {
			ENTITIES_MASTER.Remove(key);
		}

	}
}
