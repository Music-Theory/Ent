using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soul.Framework.UI.Components {
	/// <summary>
	/// Allows the entity to show text.
	/// </summary>
	public class Legible : Component {

		/// <summary>
		/// The text itself.
		/// </summary>
		public string text;

		/// <summary>
		/// Left = 0, Center = 1, Right = 2
		/// </summary>
		public byte justification;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="text">What the entity says.</param>
		/// <param name="justify">The justification of the text. (Left = 0, Center = 1, Right = 2)</param>
		public Legible(string text, byte justification) {
			this.text = text;
			this.justification = justification;
			name = "Legible";
		}
	}
}
