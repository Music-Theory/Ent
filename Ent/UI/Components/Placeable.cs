using Ent.Geometry;

namespace Ent.UI.Components {
	/// <summary>
	/// A component giving an entity a location in a window.
	/// </summary>
	public class Placeable : Component {

		/// <summary>
		/// The location of the entity.
		/// </summary>
		public Vector2<uint> loc;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="loc">Location</param>
		public Placeable(Vector2<uint> loc) {
			this.loc = loc;
			name = "Placeable";
		}

	}
}
