using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent.UI.Components {
	/// <summary>
	/// Gives the entity a location relative to the window that contains it. Pretty much required for UI elements.
	/// </summary>
	public class Placeable : Component {

		/// <summary>
		/// Location of the top left corner, in pixels. Relative to the window that contains it.
		/// </summary>
		public int x, y;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="x">Horizontal location relative to the window it's in, in pixels.</param>
		/// <param name="y">Vertical location relative to the window it's in, in pixels.</param>
		public Placeable(int x, int y) {
			this.x = x;
			this.y = y;
			name = "Placeable";
		}
	}
}
