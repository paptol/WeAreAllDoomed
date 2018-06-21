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
    class SystemMinimap : ISystem
    {
        int shaderProg = 0;
        int vsID = 0, fsID = 0;
        int uniform_stex = 0;
        int uniform_model = 0;
        int uniform_proj = 0;
        Quad minimap;

        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_MINIMAP_TRACK);

        public SystemMinimap(Quad pMinimap)
        {
            shaderProg = GL.CreateProgram();
            LoadShader("Shaders/minimapVS.glsl", ShaderType.VertexShader, shaderProg, out vsID);
            LoadShader("Shaders/minimapFS.glsl", ShaderType.FragmentShader, shaderProg, out fsID);
            GL.LinkProgram(shaderProg);
            Console.WriteLine(GL.GetProgramInfoLog(shaderProg));

            uniform_stex = GL.GetUniformLocation(shaderProg, "minimapTexture");
            uniform_model = GL.GetUniformLocation(shaderProg, "model");
            uniform_proj = GL.GetUniformLocation(shaderProg, "proj");

            minimap = pMinimap;
        }

        public string Name
        {
            get { return "SystemMinimap"; }
        }

        public void OnAction(Entity entity)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent positionComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });
                IComponent mapTrackComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_MINIMAP_TRACK;
                });

                Vector3 position = ((ComponentPosition)positionComponent).Position;
                Quad quad = ((ComponentMinimapTrack)mapTrackComponent).dotQuad;


                // MINIMAP 
                float minMap = minimap.pos.X - minimap.size;
                float maxMap = minimap.pos.X + minimap.size;
                Vector2 mazeInterval = new Vector2(minMap, maxMap);

                Vector2 mapIntervalX = new Vector2(0, 25);
                Vector2 mapIntervalY = new Vector2(0, -25);

                Vector3 pos = new Vector3(
               (mapMazePositionToMinimap(position.X, mapIntervalX, mazeInterval)),
               (mapMazePositionToMinimap(position.Z, mapIntervalY, mazeInterval)),
               0.0f);

                quad.pos = pos;
            }
        }


        /// <summary>
        /// Maps the position between two coordinate ranges.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="inputRange"></param>
        /// <param name="outputRange"></param>
        /// <returns></returns>
        float mapMazePositionToMinimap(float value, Vector2 inputRange, Vector2 outputRange)
        {
            float output = outputRange.X + ((outputRange.Y - outputRange.X) / (inputRange.Y - inputRange.X)) * (value - inputRange.X);
            return output;
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
    }
}
