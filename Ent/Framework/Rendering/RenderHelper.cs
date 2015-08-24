using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;
using Soul.Framework.Utility;
using System.Runtime.InteropServices;

namespace Soul.Framework.Rendering {
	public static class RenderHelper {

		// TODO: Move all rendering stuff to here and have RenderingSystem call methods from here

		static GraphicsContext context;

		public static int shaderProgram;

		public static int positionBufferObject;
		public static int vao;

		public static float[] vertexPositions = {
			1f, 1f, 0.0f, 1.0f,
			2f, 100f, 0.0f, 1.0f,
			100f, 100f, 0.0f, 1.0f
		};

		static readonly string[] strVertexShader = {
			"#version 330\n",
			"layout(location = 0) in vec4 position;\n",
			"void main()\n",
			"{\n",
			"   gl_Position = position;\n",
			"}\n"
		};

		static readonly string[] strFragmentShader = {
			"#version 330\n",
			"out vec4 outputColor;\n",
			"void main()\n",
			"{\n",
			"   outputColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);\n",
			"}\n"
		};

		public static bool init(IntPtr glContext) {

			if (SDL.SDL_GL_GetCurrentContext() == null) {
				Utility.Utility.erLogger.logSDLError(SDL.SDL_GetError());
				return false;
			}

			context = new GraphicsContext(new ContextHandle(glContext),
				(name) => { return SDL.SDL_GL_GetProcAddress(name); },
				() => { return new ContextHandle(SDL.SDL_GL_GetCurrentContext()); });

			initShaders();
			initVertexBuffer();

			vao = GL.GenVertexArray();
			GL.BindVertexArray(vao);
			return true;
		}

		public static void initShaders() {
			List<int> shaderList = new List<int>();
			shaderList.Add(GL.CreateShaderProgram(ShaderType.VertexShader, strVertexShader.Count(), strVertexShader));
			shaderList.Add(GL.CreateShaderProgram(ShaderType.FragmentShader, strFragmentShader.Count(), strFragmentShader));
			shaderProgram = GL.CreateProgram();
			foreach (int s in shaderList) {
				GL.AttachShader(shaderProgram, s);
				GL.LinkProgram(shaderProgram);
				GL.DeleteShader(s);
			}
			GL.LinkProgram(shaderProgram);
		}

		public static unsafe void initVertexBuffer() {
			IntPtr vPPointer;

			positionBufferObject = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, positionBufferObject);
			fixed (float* vP = vertexPositions)
			{
				vPPointer = new IntPtr(vP);
			}
			IntPtr sp = new IntPtr((int*)Marshal.SizeOf(positionBufferObject));
			GL.BufferData(BufferTarget.ArrayBuffer, sp, vPPointer, BufferUsageHint.StaticDraw);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}

		public static void drawTriangle() {
			GL.UseProgram(shaderProgram);

			GL.BindBuffer(BufferTarget.ArrayBuffer, positionBufferObject);
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 0, 0);

			GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

			GL.DisableVertexAttribArray(0);
			GL.UseProgram(0);
		}

		public static void drawLine(Line line, byte r, byte g, byte b, byte a) {
			
		}

		public static void drawTriLoop(Point o, List<Point> vertices) {

		}

	}
}
