using Ent.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent.UI {
	/// <summary>
	/// A window that can appear in a screen. You can display stuff inside of windows.
	/// </summary>
	public class Window {

		#region Size and Location

		/// <summary>
		/// Location in pixels.
		/// </summary>
		Vector2<uint> loc;
		/// <summary>
		/// Width and height in pixels.
		/// </summary>
		Vector2<uint> size;

		#endregion

		#region Properties

		WindowSystem.WindowProperty propFlags;

		#endregion

		#region Contents

		List<int> contents = new List<int>();

		#endregion

		#region Init

		/// <summary>
		/// Constructor. All numbers are in pixels.
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="width">Width</param>
		/// <param name="height">Height</param>
		public Window(Vector2<uint> loc, Vector2<uint> size, WindowSystem.WindowProperty propFlags = 0) {
			this.loc = loc;
			this.size = size;
			this.propFlags = propFlags;
		}

		#endregion

		#region Content Methods

		/// <summary>
		/// Add a piece of content to the window.
		/// </summary>
		/// <param name="comps">List of components of the content</param>
		/// <returns>The key of the entity.</returns>
		public int addContent(List<Component> comps) {
			int ent = EntitySystem.makeEntity();
			EntitySystem.entitiesMaster[ent].addComponents(comps);
			return ent;
		}

		#endregion

	}
}
