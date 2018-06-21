using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentCollisionLine : IComponent
    {
        Vector2 wallLine;
        Vector2 playerLine;
        public ComponentCollisionLine(float wX, float wY, float pX, float pY)
        {
            wallLine = new Vector2(wX, wY);
            playerLine = new Vector2(pX, pY);

        }

        public ComponentCollisionLine(Vector2 wall, Vector2 player)
        {
            wallLine = wall;
            playerLine = player;
        }

        public Vector2 WallLine
        {
            get { return wallLine; }
            set { wallLine = value; }
        }
        public Vector2 PlayerLine
        {
            get { return playerLine; }
            set { playerLine = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_COLLISION_LINE; }
        }
    }
}
