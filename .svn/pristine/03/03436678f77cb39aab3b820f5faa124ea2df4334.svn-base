using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGL_Game.Objects;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentMinimapTrack : IComponent
    {
        // Represents the entity being tracked as a quad.
        public Quad dotQuad;
        float size = 0.005f;

        public enum squareColour
        {
            red,
            black,
            green
        }

        public ComponentMinimapTrack(squareColour colour)
        {
            switch (colour)
            {
                case squareColour.red:
                    dotQuad = new Quad(new Vector3(0.0f, 0.0f, 0.0f), size, "Textures/Minimap/redDot.png");
                    dotQuad.setup();
                    break;
                case squareColour.black:
                    dotQuad = new Quad(new Vector3(0.0f, 0.0f, 0.0f), size, "Textures/Minimap/blackDot.png");
                    dotQuad.setup();
                    break;
                case squareColour.green:
                    dotQuad = new Quad(new Vector3(0.0f, 0.0f, 0.0f), size, "Textures/Minimap/greenDot.png");
                    dotQuad.setup();
                    break;
                default:
                    break;
            }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_MINIMAP_TRACK; }
        }

    }
}
