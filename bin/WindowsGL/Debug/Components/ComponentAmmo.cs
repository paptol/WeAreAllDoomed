using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentAmmo : IComponent
    {
        int ammo;

        public ComponentAmmo(int a)
        {
            Ammo = a;
        }

       
        public int Ammo
        {
            get { return ammo; }
            set { ammo = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_AMMO; }
        }
    }
}
