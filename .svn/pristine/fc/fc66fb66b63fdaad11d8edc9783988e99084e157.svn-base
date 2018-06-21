using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace OpenGL_Game.Objects
{
    public class CubeMap
    {
        float[] skyboxVertices = {
		// positions          
		-1.0f,  1.0f, -1.0f,
        -1.0f, -1.0f, -1.0f,
         1.0f, -1.0f, -1.0f,
         1.0f, -1.0f, -1.0f,
         1.0f,  1.0f, -1.0f,
        -1.0f,  1.0f, -1.0f,

        -1.0f, -1.0f,  1.0f,
        -1.0f, -1.0f, -1.0f,
        -1.0f,  1.0f, -1.0f,
        -1.0f,  1.0f, -1.0f,
        -1.0f,  1.0f,  1.0f,
        -1.0f, -1.0f,  1.0f,

         1.0f, -1.0f, -1.0f,
         1.0f, -1.0f,  1.0f,
         1.0f,  1.0f,  1.0f,
         1.0f,  1.0f,  1.0f,
         1.0f,  1.0f, -1.0f,
         1.0f, -1.0f, -1.0f,

        -1.0f, -1.0f,  1.0f,
        -1.0f,  1.0f,  1.0f,
         1.0f,  1.0f,  1.0f,
         1.0f,  1.0f,  1.0f,
         1.0f, -1.0f,  1.0f,
        -1.0f, -1.0f,  1.0f,

        -1.0f,  1.0f, -1.0f,
         1.0f,  1.0f, -1.0f,
         1.0f,  1.0f,  1.0f,
         1.0f,  1.0f,  1.0f,
        -1.0f,  1.0f,  1.0f,
        -1.0f,  1.0f, -1.0f,

        -1.0f, -1.0f, -1.0f,
        -1.0f, -1.0f,  1.0f,
         1.0f, -1.0f, -1.0f,
         1.0f, -1.0f, -1.0f,
        -1.0f, -1.0f,  1.0f,
         1.0f, -1.0f,  1.0f
    };
        List<string> faces = new List<string>{};
        int VAO_Handle;
        int VBO_Handle;
        int textureID;
        protected int shader_Handle = 0;
        // Shader variables
        private int attribute_vpos;
        private int uniform_mView;
        private int uniform_mProjection;
        private int uniform_mSamplerCube;

        private int vsID;
        private int fsID;

        private string path = System.Environment.CurrentDirectory;

        public CubeMap()
        {
        }

        public void setupSkybox()
        {
            path = path.Substring(0, path.IndexOf("bin"));
            path = path.Replace("\\","/");

            faces.Add(path.Insert(path.Length, "Textures/CubeMap/skybox3/right.png"));
            faces.Add(path.Insert(path.Length, "Textures/CubeMap/skybox3/left.png"));
            faces.Add(path.Insert(path.Length, "Textures/CubeMap/skybox3/top.png"));
            faces.Add(path.Insert(path.Length, "Textures/CubeMap/skybox3/bottom.png"));
            faces.Add(path.Insert(path.Length, "Textures/CubeMap/skybox3/front.png"));
            faces.Add(path.Insert(path.Length, "Textures/CubeMap/skybox3/back.png"));

            CreateShader();
            bufferData();
            loadCubemapTextures();
        }


        private void LoadShader(String filename, ShaderType type, int program, out int address)
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

        private void CreateShader()
        {
            shader_Handle = GL.CreateProgram();
            LoadShader("Shaders/cubeMapVS.glsl", ShaderType.VertexShader, shader_Handle, out vsID);
            LoadShader("Shaders/cubeMapFS.glsl", ShaderType.FragmentShader, shader_Handle, out fsID);
            GL.LinkProgram(shader_Handle);
            Console.WriteLine(GL.GetProgramInfoLog(shader_Handle));

            attribute_vpos = GL.GetAttribLocation(shader_Handle, "a_Position");

            uniform_mProjection = GL.GetUniformLocation(shader_Handle, "projection");
            uniform_mView = GL.GetUniformLocation(shader_Handle, "view");
            uniform_mSamplerCube = GL.GetUniformLocation(shader_Handle, "skybox");
        }

        private void bufferData()
        {
            GL.GenVertexArrays(1, out VAO_Handle);
            GL.GenBuffers(1, out VBO_Handle);

            GL.BindVertexArray(VAO_Handle);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO_Handle);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(skyboxVertices.Length * 4), skyboxVertices, BufferUsageHint.StaticDraw);

            // position attribute
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * 4, 0);

            GL.BindVertexArray(0);
        }

        private void loadCubemapTextures()
        {
            GL.GenTextures(1, out textureID);
            GL.BindTexture(TextureTarget.TextureCubeMap, textureID);

            // Setting texture parameters
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
           // GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            //GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            //GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);

            // We will not upload mipmaps, so disable mipmapping (otherwise the texture will not appear).
            // We can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create
            // mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            // Loading textures
            for (int i = 0; i < faces.Count; i++)
            {
                string filename = faces[i];
                Bitmap bmp = new Bitmap(filename);

                BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0 ,PixelInternalFormat.Rgb, bmp_data.Width, bmp_data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Rgb, PixelType.UnsignedByte, bmp_data.Scan0);

                bmp.UnlockBits(bmp_data);
            }
        }


        public void renderCubemap()
        {
            GL.UseProgram(shader_Handle);

            GL.DepthFunc(DepthFunction.Lequal); // set depth function so depth test passes when value is equal to 1 as is set in the cubemap shader

            // Binding sampler cube
            GL.Uniform1(uniform_mSamplerCube, 0); // possibly incorrect methid for uniforms of type samplerCube
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.TextureCubeMap, textureID);
            GL.Enable(EnableCap.TextureCubeMap);
            //GL.Enable(EnableCap.Texture2D);

            // Setting the view and projection matrices
            // View
            Matrix4 mView = MyGame.gameInstance.playerCamera.getViewMatrix();
            mView = mView.ClearTranslation(); // keep the cube map centered on the player by removing the translation from the view matrix
            GL.UniformMatrix4(uniform_mView, false, ref mView);
            //Projection
            Matrix4 mProjection = MyGame.gameInstance.projection;
            GL.UniformMatrix4(uniform_mProjection, false, ref mProjection);

            GL.BindVertexArray(VAO_Handle);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            GL.DepthFunc(DepthFunction.Less);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }
    }
}
