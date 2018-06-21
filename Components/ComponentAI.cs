using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game.Components
{
    class ComponentAI : IComponent
    {
        public enum AIStates : byte
        {
            Wandering,
            Chasing,
            Disabled,
            DebugOff
        }

        public AIStates CurrentState { get; set; }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_AI; }
        }
    }
}
