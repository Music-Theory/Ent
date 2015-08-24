using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soul.Framework.UI {
	class Screen {

		/// <summary>
		/// Whether the screen is something that can be input to. If not, pass input to the simulation.
		/// </summary>
		bool handlesInput;

		/// <summary>
		/// A list of all windows currently in use by the screen.
		/// </summary>
		List<Window> windows = new List<Window>();

	}
}
