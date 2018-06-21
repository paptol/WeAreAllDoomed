using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
namespace OpenGL_Game.Components
{
     
        class ComponentVelocity : IComponent
        {
            Vector3 velocity;

            public ComponentVelocity(float x, float y, float z)
            {
                velocity = new Vector3(x, y, z);
            }

            public ComponentVelocity(Vector3 v)
            {
                velocity = v;
            }

            public Vector3 Velocity
            {
                get { return velocity; }
                set { velocity = value; }
            }

            public ComponentTypes ComponentType
            {
                get { return ComponentTypes.COMPONENT_VELOCITY; }
            }
        }
}
