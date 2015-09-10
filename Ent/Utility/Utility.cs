using System;
using System.IO;
using Ent.Geometry;
using Ent.Rendering;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Ent.Utility {
	/// <summary>
	/// An all-purpose helper class for meta stuff.
	/// </summary>
	public static class Utility {

		#region File Path Helpers

		public static string basePath;

		public static string GetBasePath() { return basePath ?? ( basePath = "" ); }

		public static string GetResourcePath() {
			if (basePath == null) {
				GetBasePath();
			}
			return basePath + "Resources" + GetPathDelimiter();
		}

		public static char GetPathDelimiter() {
			return Configuration.RunningOnWindows ? '\\' : '/';
		}

		#endregion

		#region Init and Quit

		/// <summary>
		/// Initialize all Ent systems.
		/// </summary>
		/// <returns>True if nothing failed.</returns>
		public static bool InitEnt() {
			Toolkit.Init();
			return true;
		}
		/// <summary>
		/// Quit all Ent systems.
		/// </summary>
		public static void QuitEnt() {
			Graphics.Quit();
		}

		#endregion

		#region Graphics Settings

		public static Vector2<uint> res;

		#endregion

		#region Logging

		public static readonly TextWriter OSTREAM = Console.Out;

		public static void LogGLError(string message) {
			OSTREAM.WriteLine("Error: " + message + " - " + GL.GetError());
		}

		#endregion

	}
}
