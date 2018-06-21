using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentScale : IComponent
    {
        Vector3 scale;

        public ComponentScale(float x, float y, float z)
        {
            // Set the scale component to 1 if 0 is used to avoid scaling by zero errors
            if (x == 0)
                x = 1;
            if (y == 0)
                y = 1;
            if (z == 0)
                z = 1;

            scale = new Vector3(x, y, z);
        }

        public ComponentScale(Vector3 pos)
        {
            // Set the scale component to 1 if 0 is used to avoid scaling by zero errors
            if (pos.X == 0)
                pos.X = 1;
            if (pos.Y == 0)
                pos.Y = 1;
            if (pos.Z == 0)
                pos.Z = 1;

            scale = pos;
        }

        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_SCALE; }
        }
    }
}