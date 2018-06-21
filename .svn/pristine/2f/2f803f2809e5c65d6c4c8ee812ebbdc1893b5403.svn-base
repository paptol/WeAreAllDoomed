using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentRotation : IComponent
    {
        Vector3 rotation;

        /// <summary>
        ///  Stores the x, y and z components of rotation in a vector 3 member
        /// </summary>
        /// <param name="x">Angle of rotation in radians for the X axis</param>
        /// <param name="y">Angle of rotation in radians for the Y axis</param>
        /// <param name="z">Angle of rotation in radians for the Z axis</param>
        public ComponentRotation(float x, float y, float z)
        {
            rotation = new Vector3(
                MathHelper.DegreesToRadians(x), 
                MathHelper.DegreesToRadians(y), 
                MathHelper.DegreesToRadians(z));
        }

        /// <summary>
        /// Stores the x, y and z components of rotation in a vector 3 member
        /// </summary>
        /// <param name="rot">Vector storing the 3 components of rotation in radians.</param>
        public ComponentRotation(Vector3 rot)
        {
            rotation.X = MathHelper.DegreesToRadians(rot.X);
            rotation.Y = MathHelper.DegreesToRadians(rot.Y);
            rotation.Z = MathHelper.DegreesToRadians(rot.Z);
        }

        public Vector3 Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_ROTATION; }
        }
    }
}
