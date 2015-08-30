using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using SDL2;
using Ent.Rendering;
using Ent.Geometry;

namespace Ent.Utility {
	public static class Utility {

		#region File Path Helpers

		public static string BASE_PATH;

		public static string getBasePath() {
			if (BASE_PATH == null) {
				BASE_PATH = SDL.SDL_GetBasePath();
			}
			return BASE_PATH;
		}

		public static string getResourcePath() {
			if (BASE_PATH == null) {
				getBasePath();
			}
			return BASE_PATH + "Resources" + getPathDelimiter();
		}

		public static char getPathDelimiter() {
			if (SDL.SDL_GetPlatform().Equals("Windows")) {
				return '\\';
			} else {
				return '/';
			}
		}

		#endregion

		#region Init and Quit

		/// <summary>
		/// Initialize all Ent systems.
		/// </summary>
		/// <returns>True if nothing failed.</returns>
		public static bool initEnt() {
			return Graphics.init();
		}
		/// <summary>
		/// Quit all Ent systems.
		/// </summary>
		public static void quitEnt() {
			Graphics.quit();
		}

		#endregion

		#region Graphics Settings

		public static Vector2<uint> RES;

		#endregion

		#region Logging

		public static TextWriter OSTREAM = Console.Out;

		public static void logSDLError(string message) {
			OSTREAM.WriteLine("Error: " + message + " - " + SDL2.SDL.SDL_GetError());
		}

		#endregion

	}
}
