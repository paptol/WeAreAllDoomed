using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenGL_Game.Components;
using OpenGL_Game.Objects;

namespace OpenGL_Game.Objects
{
    class Quad
    {
        // Vertex data
        List<float> vertices = new List<float>();
        int VAO = 0;
        int VBO = 0;
        int minimapTexture = 0;
        int shaderProg = 0;
        int vsID = 0, fsID = 0;

        public float size = 0.22f;
        public Vector3 pos = new Vector3(0.75f, 0.75f, 0.0f);

        string filename = "Textures/Minimap/minimap.png";

        int uniform_stex = 0;
        int uniform_model = 0;
        int uniform_proj = 0;

        public Quad()
        {
        }
        public Quad(Vector3 pPos, float pSize, string pFileName)
        {
            pos = pPos;
            size = pSize;
            filename = pFileName;
        }

        public void setup()
        {
            Vector3 leftTop = new Vector3(0.0f - size, 0.0f + size, 0.0f);
            Vector3 leftBottom = new Vector3(0.0f - size, 0.0f - size, 0.0f);
            Vector3 rightBottom = new Vector3(0.0f + size, 0.0f - size, 0.0f);
            Vector3 rightTop = new Vector3(0.0f + size, 0.0f + size, 0.0f);

            Vector2 uv1 = new Vector2(0.0f, -1.0f);
            Vector2 uv2 = new Vector2(0.0f, 0.0f);
            Vector2 uv3 = new Vector2(1.0f, 0.0f);
            Vector2 uv4 = new Vector2(1.0f, -1.0f);

            vertices.Add(leftTop.X);
            vertices.Add(leftTop.Y);
            vertices.Add(leftTop.Z);
            vertices.Add(uv1.X);
            vertices.Add(uv1.Y);

            vertices.Add(leftBottom.X);
            vertices.Add(leftBottom.Y);
            vertices.Add(leftBottom.Z);
            vertices.Add(uv2.X);
            vertices.Add(uv2.Y);

            vertices.Add(rightBottom.X);
            vertices.Add(rightBottom.Y);
            vertices.Add(rightBottom.Z);
            vertices.Add(uv3.X);
            vertices.Add(uv3.Y);

            vertices.Add(leftTop.X);
            vertices.Add(leftTop.Y);
            vertices.Add(leftTop.Z);
            vertices.Add(uv1.X);
            vertices.Add(uv1.Y);

            vertices.Add(rightBottom.X);
            vertices.Add(rightBottom.Y);
            vertices.Add(rightBottom.Z);
            vertices.Add(uv3.X);
            vertices.Add(uv3.Y);

            vertices.Add(rightTop.X);
            vertices.Add(rightTop.Y);
            vertices.Add(rightTop.Z);
            vertices.Add(uv4.X);
            vertices.Add(uv4.Y);

            // configure quad VAO
            GL.GenVertexArrays(1, out VAO);
            GL.GenBuffers(1, out VBO);

            GL.BindVertexArray(VAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Count * 4), vertices.ToArray<float>(), BufferUsageHint.StaticDraw);

            // Positions
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * 4, 0);
            // Tex Coords
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * 4, 3 * 4);

            setupTexture(filename);
            setupShader();
        }
        void setupTexture(string fileName)
        {
            minimapTexture = Managers.ResourceManager.LoadTexture(fileName);
        }

        void setupShader()
        {
            shaderProg = GL.CreateProgram();
            LoadShader("Shaders/minimapVS.glsl", ShaderType.VertexShader, shaderProg, out vsID);
            LoadShader("Shaders/minimapFS.glsl", ShaderType.FragmentShader, shaderProg, out fsID);
            GL.LinkProgram(shaderProg);
            Console.WriteLine(GL.GetProgramInfoLog(shaderProg));

            uniform_stex = GL.GetUniformLocation(shaderProg, "minimapTexture");
            uniform_model = GL.GetUniformLocation(shaderProg, "model");
            uniform_proj = GL.GetUniformLocation(shaderProg, "proj");
        }
        void LoadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }
        public void Render()
        {
            GL.UseProgram(shaderProg);

            // Binding and activating diffuse texture
            GL.Uniform1(uniform_stex, 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, minimapTexture);
            GL.Enable(EnableCap.Texture2D);

            // setting model transformation
            Matrix4 mModel = Matrix4.CreateTranslation(pos);
            GL.UniformMatrix4(uniform_model, false, ref mModel);



            GL.BindVertexArray(VAO);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}
