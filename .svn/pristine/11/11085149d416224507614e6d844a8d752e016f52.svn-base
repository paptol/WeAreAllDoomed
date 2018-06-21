using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentPickUp : IComponent
    {
        int pick_ammo;
        int pick_health;
        int pick_dis;

        public ComponentPickUp(int pa, int ph, int pd )

        {
            Pick_ammo = pa;
            Pick_health = ph;
            Pick_dis = pd;
        }
        public int Pick_ammo
        {
            get { return pick_ammo; }
            set { pick_ammo = value; }
        }

        public int Pick_health
        {
            get { return pick_health; }
            set { pick_health = value; }
        }

        public int Pick_dis
        {
            get { return pick_dis; }
            set { pick_dis = value; }
        }
        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_PICK_UP; }
        }
       
        
    }
}
