using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Input;

namespace OpenGL_Game.Objects
{
    public class PathFindingNodes
    {
        public List<Vector2> Nodes = new List<Vector2>();
        public PathFindingNodes()
        {
            SetUpNodes();
        }

        private void SetUpNodes()
        {
            for (float i = 0.5f; i < 25; i += 0.5f)
            {
                for (float j = -0.5f; j > -25; j += -0.5f)
                {
                    Nodes.Add(new Vector2(i, j));
                }
            }
            foreach (MazeLevel.WallPoints w in MyGame.currentLevelLoaded.wallPlanePositions)
            {
                float wallStart;
                float wallEnd;


                for (int i = 0; i < Nodes.Count; i++)
                {
                    if (w.startPosition.X == w.endPosition.X)
                    {
                        float x = w.startPosition.X;
                        wallStart = w.startPosition.Y;
                        wallEnd = w.endPosition.Y;

                        for (float y = wallStart; y <= wallEnd; y += 0.5f)
                        {
                            if (Nodes[i] == new Vector2(x, y))
                            {
                                Nodes.Remove(new Vector2(x, y));
                                i = 0;
                            }
                        }
                    }
                    else
                    {
                        float y = w.startPosition.Y;
                        wallStart = w.startPosition.X;
                        wallEnd = w.endPosition.X;

                        for (float x = wallStart; x <= wallEnd; x += 0.5f)
                        {
                            if (Nodes[i] == new Vector2(x, y))
                            {
                                Nodes.Remove(new Vector2(x, y));
                                i = 0;
                            }
                        }
                    }
                }
            }
        }
    }
}
