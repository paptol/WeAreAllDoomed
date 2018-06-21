using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenGL_Game.Objects;
using OpenGL_Game.Components;
using OpenTK.Graphics.OpenGL;

namespace OpenGL_Game.Components
{
    class ComponentLightEmitter : IComponent
    {
        int lightIndex;
        Vector3 ambient;
        Vector3 diffuse;
        Vector3 specular;

        public ComponentLightEmitter(Vector3 pAmbient, Vector3 pDiffuse, Vector3 pSpecular)
        {
            ambient = pAmbient;
            diffuse = pDiffuse;
            specular = pSpecular;

            // Assigns an index to the light for the lighting system to use.
            lightIndex = OpenGL_Game.Systems.SystemLighting.lightIndex;
            OpenGL_Game.Systems.SystemLighting.lightIndex++;
        }

        public Vector3 Diffuse
        {
            get { return diffuse; }
            set { diffuse = value; }
        }
        public Vector3 Ambient
        {
            get { return ambient; }
            set { ambient = value; }
        }
        public Vector3 Specular
        {
            get { return specular; }
            set { specular = value; }
        }
        public int Index
        {
            get { return lightIndex; }
            set { lightIndex = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_LIGHT_EMITTER; }
        }
    }
}
