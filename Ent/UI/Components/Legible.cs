using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent.UI.Components {
	/// <summary>
	/// A component that allows its entity to display text.
	/// </summary>
	public class Legible : Component {

		/// <summary>
		/// The text displayed in the entity.
		/// </summary>
		string text;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="text">Text</param>
		public Legible(string text) {
			this.text = text;
			this.name = "Legible";
		}

	}
}
