using System.Collections.Generic;

namespace Ent {
	public static class EntitySystem {

		public static Dictionary<int, Entity> entitiesMaster = new Dictionary<int, Entity>();

		static int currentKey = 1;

		public static int makeEntity() {
			entitiesMaster.Add(currentKey, new Entity());
			currentKey++;
			return currentKey - 1;
		}

		public static int makeEntity<T>(int key, Dictionary<int, T> dict, T ent) {
			dict.Add(key, ent);
			key++;
			return key - 1;
		}

		public static void removeEntity(int key) {
			entitiesMaster.Remove(key);
		}

	}
}
