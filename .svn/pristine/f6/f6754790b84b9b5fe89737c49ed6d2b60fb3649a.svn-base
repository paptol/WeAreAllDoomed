using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenGL_Game.Managers;

namespace OpenGL_Game.Components
{
    class ComponentNormalMap : IComponent
    {
        int texture;

        public ComponentNormalMap(string textureName)
        {
            texture = ResourceManager.LoadTexture(textureName);
        }

        public int Texture
        {
            get { return texture; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_NORMAL_MAP; }
        }
    }
}

