using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Input;

namespace OpenGL_Game.Objects
{
    public class Node
    {
        public Vector2 Position;
        public int fCost;
        public int travelCost;
        public int targetCost;
        public Node parentNode;
        public Node(Vector2 p, int tr, int ta, Node n)
        {
            Position = p;
            travelCost = tr;
            targetCost = ta;
            fCost = travelCost + targetCost;
            parentNode = n;
        }

        public Node(Vector2 p, int tr, int ta)
        {
            Position = p;
            travelCost = tr;
            targetCost = ta;
            fCost = travelCost + targetCost;

        }
        public Node(Vector2 p)
        {
            Position = p;        
        }


    }
}
