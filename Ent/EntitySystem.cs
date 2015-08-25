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

		public static void removeEntity(int key) {
			entitiesMaster.Remove(key);
		}

	}
}
