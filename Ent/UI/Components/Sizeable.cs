using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ent.Geometry;

namespace Ent.UI.Components {
	/// <summary>
	/// A component that gives an entity size in a window.
	/// </summary>
	public class Sizeable : Component {

		/// <summary>
		/// The size of the entity, in pixels.
		/// </summary>
		Vector2<uint> size;

		/// <summary>
		/// Whether the size should be the same as the size of the window.
		/// </summary>
		bool fullWindow;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="size">Size</param>
		public Sizeable(Vector2<uint> size, bool fullWindow = false) {
			this.size = size;
			this.fullWindow = fullWindow;
			name = "Sizeable";
		}

	}
}
