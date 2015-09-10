using System;
using System.Collections.Generic;
using Ent.Geometry;

namespace Ent.UI {
	/// <summary>
	/// A utility class for use with the windowing system.
	/// </summary>
	public static class WindowSystem {

		#region Metadata

		/// <summary>
		/// Allows the rendering system to determine the order in which to display windows. Windows with the same precedence are rendered in order of the time they were last clicked.
		/// </summary>
		public enum Precedence {
			FRONT,
			MIDDLE,
			BACK
		}

		/// <summary>
		/// Window property flags.
		/// </summary>
		[Flags]
		public enum WindowProperty {
			SCROLLABLE = 0x01,
			RESIZEABLE = 0x02,
			MOBILE = 0x04,
			CLOSEABLE = 0x08,
			BORDERLESS = 0x16
		}

		/// <summary>
		/// The current key. Used and incremented when making a window.
		/// </summary>
		static int currentKey = 1;
		/// <summary>
		/// The key of the window that currently holds focus. -1 for none.
		/// </summary>
		public static int currentFocus = -1;

		#endregion

		#region Collections

		/// <summary>
		/// A dictionary containing all windows.
		/// </summary>
		static Dictionary<int, Window> windowDict = new Dictionary<int, Window>();

		#endregion

		#region Window Creation and Removal

		/// <summary>
		/// Makes a new window. Location and size are in pixels.
		/// </summary>
		/// <param name="loc">The location of the window.</param>
		/// <param name="size">The size of the window.</param>
		/// <returns>The key corresponding to the window.</returns>
		public static int MakeWindow(Vector2<uint> loc, Vector2<uint> size) {
			return EntitySystem.MakeEntity(currentKey, windowDict, new Window(loc, size));
		}
		/// <summary>
		/// Removes a window from the window dictionary.
		/// </summary>
		/// <param name="key">The key of the window.</param>
		/// <returns>True if the removal operation was successful.</returns>
		public static bool RemoveWindow(int key) {
			return windowDict.Remove(key);
		}

		#endregion

		#region Window Interaction

		/// <summary>
		/// Get a window from the dictionary.
		/// </summary>
		/// <param name="key">The key of the window.</param>
		/// <returns>The window, if it exists. NULL if it doesn't.</returns>
		public static Window GetWindow(int key) {
			return windowDict[key];
		}

		#endregion

	}
}
