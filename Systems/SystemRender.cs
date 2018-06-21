using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenGL_Game.Components;
using OpenGL_Game.Objects;

namespace OpenGL_Game.Systems
{
    class SystemRender : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_GEOMETRY |
            ComponentTypes.COMPONENT_TEXTURE | ComponentTypes.COMPONENT_SCALE);

        public static int pgmID;
        protected int vsID;
        protected int fsID;

        // Vertex shader attributes
        protected int attribute_vpos;
        protected int attribute_vnormal;
        protected int attribute_vtex;
        protected int attribute_vTangent;
        protected int attribute_vBitangent;

        protected int uniform_mModel;
        protected int uniform_mView;
        protected int uniform_mProjection;

        // Fragment shader attributes
        protected int uniform_material;
        protected int uniform_light;
        protected int uniform_viewPos;
        protected int uniform_stex;
        protected int uniform_snormalTex;
        protected int uniform_isNormalMapping;

        public SystemRender()
        {
            pgmID = GL.CreateProgram();
            LoadShader("Shaders/vs.glsl", ShaderType.VertexShader, pgmID, out vsID);
            LoadShader("Shaders/fs.glsl", ShaderType.FragmentShader, pgmID, out fsID);
            GL.LinkProgram(pgmID);
            Console.WriteLine(GL.GetProgramInfoLog(pgmID));

            attribute_vpos = GL.GetAttribLocation(pgmID, "a_Position");
            attribute_vnormal = GL.GetAttribLocation(pgmID, "a_Normal");
            attribute_vtex = GL.GetAttribLocation(pgmID, "a_TexCoord");
            attribute_vTangent = GL.GetAttribLocation(pgmID, "a_Tangent");
            attribute_vBitangent = GL.GetAttribLocation(pgmID, "a_Bitangent");

            uniform_mModel = GL.GetUniformLocation(pgmID, "model");
            uniform_mView = GL.GetUniformLocation(pgmID, "view");
            uniform_mProjection = GL.GetUniformLocation(pgmID, "projection");

            uniform_material = GL.GetUniformLocation(pgmID, "material");
            uniform_light = GL.GetUniformLocation(pgmID, "light.position");
            uniform_viewPos = GL.GetUniformLocation(pgmID, "viewPos");
            uniform_stex = GL.GetUniformLocation(pgmID, "s_texture");
            uniform_snormalTex = GL.GetUniformLocation(pgmID, "s_normal");
            uniform_isNormalMapping = GL.GetUniformLocation(pgmID, "isNormalMap");
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

        public string Name
        {
            get { return "SystemRender"; }
        }

        public void OnAction(Entity entity)
        {

            // Rendering the quads representing entities on the minimap.
            if ((entity.Mask & ComponentTypes.COMPONENT_MINIMAP_TRACK) == ComponentTypes.COMPONENT_MINIMAP_TRACK)
            {
                List<IComponent> components = entity.Components;

                IComponent mapTrackComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_MINIMAP_TRACK;
                });
                Quad quad = ((ComponentMinimapTrack)mapTrackComponent).dotQuad;
                quad.Render();
            }


                if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent geometryComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_GEOMETRY;
                });
                Geometry geometry = ((ComponentGeometry)geometryComponent).Geometry();

                #region Model matrix set up

                IComponent positionComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });
                Vector3 position = ((ComponentPosition)positionComponent).Position;

                IComponent rotationComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_ROTATION;
                });
                Vector3 rotation = ((ComponentRotation)rotationComponent).Rotation;

                IComponent scaleComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_SCALE;
                });
                Vector3 scale = ((ComponentScale)scaleComponent).Scale;

                // Combine transformations to create the model matrix
                Matrix4 mModel = Matrix4.CreateScale(scale) * Matrix4.CreateRotationX(rotation.X) *
                    Matrix4.CreateRotationY(rotation.Y) * Matrix4.CreateRotationZ(rotation.Z) *
                    Matrix4.CreateTranslation(position);

                #endregion

                IComponent textureComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_TEXTURE;
                });
                int texture = ((ComponentTexture)textureComponent).Texture;

                int normalTexture = -1;
                if ((entity.Mask & ComponentTypes.COMPONENT_NORMAL_MAP) == ComponentTypes.COMPONENT_NORMAL_MAP)
                {
                    IComponent normalComponent = components.Find(delegate (IComponent component)
                    {
                        return component.ComponentType == ComponentTypes.COMPONENT_NORMAL_MAP;
                    });
                    normalTexture = ((ComponentNormalMap)normalComponent).Texture;
                }

                Draw(mModel, geometry, texture, normalTexture);
            }
        }

        public void Draw(Matrix4 model, Geometry geometry, int texture, int normalTex)
        {
            GL.UseProgram(pgmID);

            // Binding and activating diffuse texture
            GL.Uniform1(uniform_stex, 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.Enable(EnableCap.Texture2D);


            // Binding and activating normal map texture
            if (normalTex != -1)
            {
                GL.Uniform1(uniform_isNormalMapping, 1);

                GL.Uniform1(uniform_snormalTex, 2);
                GL.ActiveTexture(TextureUnit.Texture2);
                GL.BindTexture(TextureTarget.Texture2D, normalTex);
                GL.Enable(EnableCap.Texture2D);
            }
            else
                GL.Uniform1(uniform_isNormalMapping, 0);



            // Setting the matrices/vectors to perform shader light calculations.
            // Model
            Matrix4 mModel = model;
            GL.UniformMatrix4(uniform_mModel, false, ref mModel);
            // View
            Matrix4 mView = MyGame.gameInstance.playerCamera.getViewMatrix();
            GL.UniformMatrix4(uniform_mView, false, ref mView);
            //Projection
            Matrix4 mProjection = MyGame.gameInstance.projection;
            GL.UniformMatrix4(uniform_mProjection, false, ref mProjection);
            //view position
            Vector3 mViewPos = MyGame.gameInstance.playerCamera.Position;
            GL.Uniform3(uniform_viewPos, ref mViewPos);

            Vector3 A = new Vector3(1.0f, 0.0f, 0.0f);
            Vector3 B = new Vector3(1.0f, 1.0f, 0.0f);
            float result = Vector3.Dot(B.Normalized(), A.Normalized());

            geometry.Render();

            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }
    }
}
