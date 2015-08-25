using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;
using Ent.Geometry;
using System.Runtime.InteropServices;

namespace Ent.Rendering {
	/// <summary>
	/// Handles rendering of things using SDL and OpenGL.
	/// </summary>
	public static class Graphics {

		#region Pointers

		/// <summary>
		/// An IntPtr to the renderer provided by SDL.
		/// </summary>
		public static IntPtr renderer;
		/// <summary>
		/// An IntPtr to the window provided by SDL.
		/// </summary>
		public static IntPtr window;
		/// <summary>
		/// An IntPtr to the GL Context provided by SDL.
		/// </summary>
		public static IntPtr glContext;

		#endregion

		#region Setup

		/// <summary>
		/// Initializes Ent graphics. Makes a renderer, a window, and a GL Context using SDL.
		/// </summary>
		/// <returns>True if nothing failed.</returns>
		public static bool init() {
			if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0) {
				//Utility.erLogger.logSDLError("SDL_image.IMG_Init");
				return false;
			}

			window = SDL.SDL_CreateWindow("Soul", 10, 10, Utility.Utility.RES_X, Utility.Utility.RES_Y, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL);
			if (window == IntPtr.Zero) {
				//Utility.erLogger.logSDLError("SDL_CreateWindow");
				return false;
			}

			glContext = SDL.SDL_GL_CreateContext(window);
			SDL.SDL_GL_MakeCurrent(window, glContext);
			//Graphics.init(glContext);

			renderer = SDL.SDL_CreateRenderer(window, -1, 2 | 4); //2 = accelerated, 4 = vsync
			if (renderer == IntPtr.Zero) {
				//Utility.erLogger.logSDLError("SDL_CreateRenderer");
				return false;
			}
			return true;
		}

		#endregion

		#region Texture Methods

		/// <summary>
		/// Renders a texture, or part of one.
		/// </summary>
		/// <param name="tex">IntPtr to the texture.</param>
		/// <param name="renderer">IntPtr to the renderer.</param>
		/// <param name="sourceRect">IntPtr to a rectangle describing the portion of the texture to be rendered.</param>
		/// <param name="x">X of the top left corner of the destination rect.</param>
		/// <param name="y">Y of the top left corner of the destination rect.</param>
		/// <param name="h">Height of the destination rect.</param>
		/// <param name="w">Width of the destination rect.</param>
		public static void renderTexture(IntPtr tex, IntPtr renderer, IntPtr sourceRect, int x, int y, int h, int w) {
			SDL.SDL_Rect rect = new SDL.SDL_Rect();
			rect.x = x;
			rect.y = y;
			rect.w = w;
			rect.h = h;

			//uint format = SDL2.SDL.SDL_PIXELFORMAT_UNKNOWN;
			//int access = (int) SDL2.SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC;
			//SDL2.SDL.SDL_QueryTexture(tex.texture, out format, out access, out rect.w, out rect.h); //this stuff is used to get the dimensions of the image
			SDL.SDL_RenderCopy(renderer, tex, sourceRect, ref rect);
		}

		/// <summary>
		/// Creates a texture from an image.
		/// </summary>
		/// <param name="image">The image to create the texture from. Usually created with loadImage()</param>
		/// <param name="freeSurface">Frees the image memory. Make this true if this is the last time you are using the image.</param>
		/// <returns>An IntPtr to the texture.</returns>
		public static IntPtr createTexture(IntPtr image, bool freeSurface = false) {
			IntPtr texture = SDL.SDL_CreateTextureFromSurface(renderer, image);
			if (freeSurface) {
				SDL.SDL_FreeSurface(image);
			}
			if (texture == IntPtr.Zero) {
				//Utility.erLogger.logSDLError("SDL_CreateTextureFromSurface");
				return IntPtr.Zero;
			}
			return texture;
		}

		#endregion

		#region Image Methods

		/// <summary>
		/// Loads an image using a path to the image.
		/// </summary>
		/// <param name="imagePath">The path to the image.</param>
		/// <returns>An IntPtr to the image that was loaded.</returns>
		public static IntPtr loadImage(string imagePath) {
			IntPtr image = SDL_image.IMG_Load(imagePath);
			if (image == IntPtr.Zero) {
				//Utility.erLogger.logSDLError("SDL_Image.IMG_Load");
				return IntPtr.Zero;
			}
			return image;
		}

		#endregion

	}
}
