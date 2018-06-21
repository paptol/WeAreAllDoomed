using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentWeapon : IComponent
    {
        bool hasWeapon;

        public ComponentWeapon(bool w)
        {
            hasWeapon = w;
        }


        public bool Weapon
        {
            get { return hasWeapon; }
            set { hasWeapon = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_INPUT; }
        }
    }
}
