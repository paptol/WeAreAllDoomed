using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    [FlagsAttribute]
    public enum ComponentTypes {
        COMPONENT_NONE = 0,
	    COMPONENT_POSITION = 1 << 0,
        COMPONENT_GEOMETRY = 1 << 1,
        COMPONENT_TEXTURE  = 1 << 2,
        COMPONENT_VELOCITY = 1 << 3,
        COMPONENT_AMMO = 1 << 4,
        COMPONENT_HEALTH = 1 << 5,
        COMPONENT_LIGHT = 1 << 6,
        COMPONENT_WEAPON = 1 << 7,
        COMPONENT_COLLISION_SPHERE = 1 << 8,
        COMPONENT_COLLISION_LINE = 1 << 9,
        COMPONENT_INPUT = 1 << 10,
        COMPONENT_SCALE = 1 << 11,
        COMPONENT_ROTATION = 1 << 12,
        COMPONENT_AUDIOEMITTER = 1 << 13,
        COMPONENT_AI = 1 << 14,
        COMPONENT_PICK_UP = 1 << 15,
        COMPONENT_LIGHT_EMITTER = 1 << 16,
        COMPONENT_ALIVE = 1 << 17,
        COMPONENT_NORMAL_MAP = 1 << 18,
        COMPONENT_LIGHT_DIRECTION = 1 << 19,
        COMPONENT_MINIMAP_TRACK = 1 << 20
    }

    public interface IComponent
    {
        ComponentTypes ComponentType
        {
            get;
        }
    }
}
