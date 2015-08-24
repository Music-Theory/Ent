using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soul.Framework.Utility {

	public class ErrorLogger {
		public void logSDLError(string message) {
			Utility.OSTREAM.WriteLine("Error: " + message + " - " + SDL2.SDL.SDL_GetError());
		}

		public void logGenericError(string message) {
			Utility.OSTREAM.WriteLine("Error: " + message);
		}
	}
}
