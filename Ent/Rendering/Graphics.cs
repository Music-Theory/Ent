using System;
using System.Collections.Generic;
using SDL2;
using OpenGLDotNet;

using System.Runtime.InteropServices;

namespace Ent.Rendering {
	/// <summary>
	/// Handles rendering of things using SDL and OpenGL.
	/// </summary>
	public static class Graphics {

		#region Temporary

		static float[] vertexPositions = {
			0.75f, 0.75f, 0.0f, 1.0f,
			0.75f, -0.75f, 0.0f, 1.0f,
			-0.75f, -0.75f, 0.0f, 1.0f,
		};

		static uint[] positionBufferObject;
		static uint[] vao;

		#endregion

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

		#region Shaders

		static uint shaderProgram;

		static string[] strVertexShader = {
			"#version 330\n",
			"layout(location = 0) in vec4 position;\n",
			"void main()\n",
			"{\n",
			"   gl_Position = position;\n",
			"}\n"
		};

		static string[] strFragmentShader = {
			"#version 330\n",
			"out vec4 outputColor;\n",
			"void main()\n",
			"{\n",
			"   outputColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);\n",
			"}\n"
		};

		#endregion

		#region Init

		/// <summary>
		/// Initializes Ent graphics. Makes a renderer, a window, and a GL Context using SDL.
		/// </summary>
		/// <returns>True if nothing failed.</returns>
		public static bool init() {
			if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0) {
				//Utility.erLogger.logSDLError("SDL_image.IMG_Init");
				return false;
			}

			window = SDL.SDL_CreateWindow("Soul", 10, 10, (int) Utility.Utility.RES.x, (int) Utility.Utility.RES.y, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL);
			if (window == IntPtr.Zero) {
				//Utility.erLogger.logSDLError("SDL_CreateWindow");
				return false;
			}

			renderer = SDL.SDL_CreateRenderer(window, -1, 2 | 4); //2 = accelerated, 4 = vsync
			if (renderer == IntPtr.Zero) {
				//Utility.erLogger.logSDLError("SDL_CreateRenderer");
				return false;
			}

			return initGL();
		}
		/// <summary>
		/// Unload all of the graphics stuff. Right now this just calls SDL_Quit.
		/// </summary>
		public static void quit() {
			SDL.SDL_Quit();
		}

		/// <summary>
		/// Initializes OpenGL.
		/// </summary>
		/// <returns>True if successful.</returns>
		public static bool initGL() {

			glContext = SDL.SDL_GL_CreateContext(window);
			SDL.SDL_GL_MakeCurrent(window, glContext);

			initShaders();
			initVertexBuffer();

			GL.GenVertexArrays(1, vao);
			GL.BindVertexArray(vao[0]);

			GL.ClearColor(0f, 0f, 0f, 0f);

			return true;
		}

		/// <summary>
		/// Initializes OpenGL shaders.
		/// </summary>
		public static void initShaders() {
			List<uint> shaderList = new List<uint>();

			shaderList.Add(createShader(GL.GL_VERTEX_SHADER, strVertexShader));
			shaderList.Add(createShader(GL.GL_FRAGMENT_SHADER, strVertexShader));

			shaderProgram = createProgram(shaderList);

			foreach (uint shader in shaderList) { GL.DeleteShader(shader); }
		}
		/// <summary>
		/// Initializes the vertex buffer.
		/// </summary>
		public static void initVertexBuffer() {
			GL.GenBuffers(1, positionBufferObject);

			GL.BindBuffer(GL.GL_ARRAY_BUFFER, positionBufferObject[0]);
			unsafe {
				fixed (float* vPPointer = vertexPositions) {
					GL.BufferData(GL.GL_ARRAY_BUFFER, Marshal.SizeOf(vertexPositions), new IntPtr(vPPointer), GL.GL_STATIC_DRAW);
				}
			}

			GL.BindBuffer(GL.GL_ARRAY_BUFFER, 0);
		}

		#endregion

		#region Render Methods

		/// <summary>
		/// Clears the buffer to whatever color GL.ClearColor is set to.
		/// </summary>
		public static void renderClear() {
			GL.Clear(GL.GL_COLOR_BUFFER_BIT);
		}
		/// <summary>
		/// I think this swaps the buffers and, therefore, puts everything on the screen?
		/// </summary>
		public static void renderPresent() {
			GLUT.SwapBuffers();
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

		#region Shader Methods

		/// <summary>
		/// Creates a shader.
		/// </summary>
		/// <param name="shaderType">The type of the shader. Defined by constants in GL.</param>
		/// <param name="shaderFile">The array of strings that make up the shader.</param>
		/// <returns>A uint representing the compiled shader.</returns>
		static uint createShader(uint shaderType, string[] shaderFile) {
			uint shader = GL.CreateShaderObjectARB(shaderType);
			GL.ShaderSource(shader, shaderFile.Length, shaderFile, null);

			GL.CompileShader(shader);

			// Error logging that I don't feel like messing with right now.
			/*int status;
			GL.GetShaderiv(shader, GL.GL_COMPILE_STATUS, status);
			if (status == GL_FALSE) {
				GLint infoLogLength;
				glGetShaderiv(shader, GL_INFO_LOG_LENGTH, &infoLogLength);

				GLchar* strInfoLog = new GLchar[infoLogLength + 1];
				glGetShaderInfoLog(shader, infoLogLength, NULL, strInfoLog);

				const char* strShaderType = NULL;
				switch (eShaderType) {
					case GL_VERTEX_SHADER: strShaderType = "vertex"; break;
					case GL_GEOMETRY_SHADER: strShaderType = "geometry"; break;
					case GL_FRAGMENT_SHADER: strShaderType = "fragment"; break;
				}

				fprintf(stderr, "Compile failure in %s shader:\n%s\n", strShaderType, strInfoLog);
				delete[] strInfoLog;
			}*/

			return shader;
		}

		/// <summary>
		/// Makes a GLSL shader program out of a list of shaders and then returns a uint that represents it.
		/// </summary>
		/// <param name="shaderList">The list of shaders to be attached to the program.</param>
		/// <returns>A uint representing the program that was made.</returns>
		static uint createProgram(List<uint> shaderList) {
			uint program = GL.CreateProgramObjectARB();

			foreach (uint shader in shaderList) {
				GL.AttachShader(program, shader);
			}

			GL.LinkProgram(program);

			// This is error checking and I don't feel like adding it right now
			/*GLint status;
			glGetProgramiv(program, GL_LINK_STATUS, &status);
			if (status == GL_FALSE) {
				GLint infoLogLength;
				glGetProgramiv(program, GL_INFO_LOG_LENGTH, &infoLogLength);

				GLchar* strInfoLog = new GLchar[infoLogLength + 1];
				glGetProgramInfoLog(program, infoLogLength, NULL, strInfoLog);
				fprintf(stderr, "Linker failure: %s\n", strInfoLog);
				delete[] strInfoLog;
			}*/

			foreach (uint shader in shaderList) {
				GL.DetachShader(program, shader);
			}

			return program;
		}

		#endregion

	}
}
