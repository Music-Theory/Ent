using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.IO;

namespace Ent.Rendering {

	/// <summary>
	///     A helper class for rendering things and setting up OpenGL.
	/// </summary>
	public static class Graphics {
		#region Variables

		/// <summary>
		///     The current shader program.
		/// </summary>
		static int currentShader;

		/// <summary>
		///     The perspective matrix (For transforming things into camera space)
		/// </summary>
		static Matrix4 perspectiveMatrix;

		/// <summary>
		///     The uniform location for the perspective matrix.
		/// </summary>
		static int perspectiveMatrixUnif;

		/// <summary>
		///     The frustum scale.
		/// </summary>
		static float frustumScale;

		#endregion

		#region Window

		/// <summary>
		///     What to do when the window resizes.
		/// </summary>
		/// <param name="sender">Where the resize event came from.</param>
		/// <param name="e">The arguments of the resize event.</param>
		/// <param name="width">The new width of the window.</param>
		/// <param name="height">The new height of the window.</param>
		public static void Resize(object sender, EventArgs e, int width, int height) {
			perspectiveMatrix.M11 = frustumScale / ( width / (float) height ); // THE CAST IS INTEGRAL TO THIS
			perspectiveMatrix.M22 = frustumScale;

			GL.UseProgram(currentShader);
			GL.UniformMatrix4(perspectiveMatrixUnif, false, ref perspectiveMatrix);
			GL.UseProgram(0);

			GL.Viewport(0, 0, width, height);
		}

		#endregion

		#region Init & Quit

		/// <summary>
		///     Initialize.
		/// </summary>
		/// <returns>True if success.</returns>
		public static bool Init() {
			Utility.Utility.OSTREAM.WriteLine("Loading Ent Graphics...");
			return true;
		}

		/// <summary>
		///     Quit.
		/// </summary>
		public static void Quit() { Utility.Utility.OSTREAM.WriteLine("Unloading Ent Graphics..."); }

		#endregion

		#region Shader Methods

		/// <summary>
		/// Creates a shader from a text file.
		/// </summary>
		/// <param name="type">The type of shader.</param>
		/// <param name="fileName">The name of the file.</param>
		/// <returns>An integer ID referring to the shader.</returns>
		public static int LoadShader(ShaderType type, string fileName) {

			List<string> shaderData = new List<string>();

			StreamReader shaderFile = new StreamReader(fileName);

			Console.Out.WriteLine("Loading shader: " + fileName);

			while (!shaderFile.EndOfStream) {
				shaderData.Add(shaderFile.ReadLine() + "\n"); // Apparently GLSL requires a newline at the end of every string
				Console.Out.Write(shaderData[shaderData.Count - 1]);
			}

			return CreateShader(type, shaderData.ToArray());
		}

		/// <summary>
		/// Creates a shader from a string.
		/// </summary>
		/// <param name="type">The type of shader (For example, vertex or fragment)</param>
		/// <param name="shaderFile">The shader itself. Each line is an entry in the array.</param>
		/// <returns>An integer ID referring to the shader.</returns>
		public static int CreateShader(ShaderType type, string[] shaderFile) {
			int shader = GL.CreateShader(type);
			//const char* strFileData = strShaderFile.c_str();
			GL.ShaderSource(shader, shaderFile.Length, shaderFile, (int[])null); // lol apparently you can cast null

			GL.CompileShader(shader);

			// Error checking
			/*int status;
			glGetShaderiv(shader, GL_COMPILE_STATUS, &status);
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
		/// Makes a shader program.
		/// </summary>
		/// <param name="shaderList">An ID list of shaders to be added to the program.</param>
		/// <returns>An integer ID referring to the shader program.</returns>
		public static int CreateProgram(List<int> shaderList) {
			int program = GL.CreateProgram();

			foreach (int shader in shaderList) {
				GL.AttachShader(program, shader);
			}

			GL.LinkProgram(program);

			Console.Out.WriteLine(GL.GetError());

			// Error Checking
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

			foreach (int shader in shaderList) {
				GL.DetachShader(program, shader);
			}

			foreach (int shader in shaderList) {
				GL.DeleteShader(shader);
			}

			return program;
		}

		#endregion
	}

}