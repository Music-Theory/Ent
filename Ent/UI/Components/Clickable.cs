using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent.UI.Components {
	/// <summary>
	/// Allows the entity to do something when it's clicked.
	/// </summary>
	public class Clickable : Component {

		/// <summary>
		/// The method that's called when the entity is clicked.
		/// </summary>
		/// <returns>True if successful.</returns>
		public delegate bool OnClickDelegate();

		/// <summary>
		/// The method that's called when the entity is clicked.
		/// </summary>
		OnClickDelegate OnClick;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="OnClick">The method that's called when the entity is clicked.</param>
		public Clickable(OnClickDelegate OnClick) {
			this.OnClick = OnClick;
			name = "Clickable";
		}

	}
}
