using OpenGL_Game.Objects;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGL_Game.Components;

namespace OpenGL_Game
{
    public abstract class MazeLevel
    {
        public List<Vector3> wallPositions = new List<Vector3>();
        public List<Vector3> wallRotations = new List<Vector3>();
        public List<Vector3> wallScales = new List<Vector3>();
        public List<string> wallTexture = new List<string>();
        public List<string> wallNormals = new List<string>();

        public struct WallPoints
        {
            public Vector2 startPosition;
            public Vector2 endPosition;
        }

        public MazeLevel()
        {
            setupMazeEnvironment();
            setupWallPoints();
        }

        public List<WallPoints> wallPlanePositions = new List<WallPoints>();

        protected void setupWallPoints()
        {

            for (int i = 0; i < wallPositions.Count; i++)
            {
                if (wallPositions[i].Y == 0)
                {
                    WallPoints w = new WallPoints();
                    if (wallRotations[i].Y == 0) // if wall is horizontal
                    {
                        float xA = (wallPositions[i].X) - wallScales[i].X;
                        float xB = (wallPositions[i].X) + wallScales[i].X;
                        float Z = wallPositions[i].Z;
                        ;
                        w.startPosition = new Vector2(xA, Z);
                        w.endPosition = new Vector2(xB, Z);
                        wallPlanePositions.Add(w);
                    }
                    else // wall is along the z axis
                    {
                        float x = wallPositions[i].X;
                        float zA = (wallPositions[i].Z) - wallScales[i].X;
                        float zB = (wallPositions[i].Z) + wallScales[i].X;

                        w.startPosition = new Vector2(x, zA);
                        w.endPosition = new Vector2(x, zB);
                        wallPlanePositions.Add(w);

                    }
                }
            }
        }

        protected abstract void addWallPositions();

        protected abstract void addWallRotations();

        protected abstract void addWallScales();

        protected abstract void addWallTextures();

        /// <summary>
        /// Uses the maze walls hard coded at x below 12.5 to generate the remaining positions of the walls.
        /// </summary>
        protected void addSymmetricalWalls()
        {
            int wallsToCopy = wallPositions.Count;

            for (int i = 0; i < wallsToCopy; i++)
            {
                wallPositions.Add(new Vector3(25 - wallPositions[i].X, // line of symmetry lies at x = 12.5
                    0, wallPositions[i].Z));
                wallRotations.Add(new Vector3(wallRotations[i])); // repeat same rotations
                wallScales.Add(new Vector3(wallScales[i])); // repeat same scaling
                wallTexture.Add("Textures/BrickWall/diffuse.png");
                wallNormals.Add("Textures/BrickWall/Normal2.jpg");
            }
        }

        /// <summary>
        /// Adds all the walls which cross the line of symmetry.
        /// </summary>
        protected void addWallsCrossingSymmetry()
        {
            wallPositions.Add(new Vector3(12.5f, 0, -11));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallScales.Add(new Vector3(1, 0, 1.25f));
            wallTexture.Add("Textures/BrickWall/diffuse.png");
            wallNormals.Add("Textures/BrickWall/Normal2.jpg");

            wallPositions.Add(new Vector3(12.5f, 0, -12));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallScales.Add(new Vector3(5, 0, 1.25f));
            wallTexture.Add("Textures/BrickWall/diffuse.png");
            wallNormals.Add("Textures/BrickWall/Normal2.jpg");

            wallPositions.Add(new Vector3(12.5f, 0, -23));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallTexture.Add("Textures/BrickWall/diffuse.png");
            wallNormals.Add("Textures/BrickWall/Normal2.jpg");

            wallPositions.Add(new Vector3(12.5f, 0, -24));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallScales.Add(new Vector3(1, 0, 1.25f));
            wallTexture.Add("Textures/BrickWall/diffuse.png");
            wallNormals.Add("Textures/BrickWall/Normal2.jpg");
        }

        /// <summary>
        /// Adds doors to the maze that can be opened.
        /// </summary>
        protected abstract void addDoors();

        /// <summary>
        /// Adds the walls surrounding the maze.
        /// </summary>
        protected void addOuterWalls(string diffuse, string normal)
        {
            // Left wall
            wallPositions.Add(new Vector3(0, 0, -12.5f));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallScales.Add(new Vector3(12.5f, 0.0f, 1.25f));
            wallTexture.Add(diffuse);
            wallNormals.Add(normal);
            // Right wall
            wallPositions.Add(new Vector3(25, 0, -12.5f));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallScales.Add(new Vector3(12.5f, 0.0f, 1.25f));
            wallTexture.Add(diffuse);
            wallNormals.Add(normal);
            // Top wall
            wallPositions.Add(new Vector3(12.5f, 0, -25.0f));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallScales.Add(new Vector3(12.5f, 0.0f, 1.25f));
            wallTexture.Add(diffuse);
            wallNormals.Add(normal);
            // Bottom wall
            wallPositions.Add(new Vector3(12.5f, 0.0f, 0.0f));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallScales.Add(new Vector3(12.5f, 0.0f, 1.25f));
            wallTexture.Add(diffuse);
            wallNormals.Add(normal);
        }

        /// <summary>
        /// Sets up all the information used to create the wall entities.
        /// </summary>
        void setupMazeEnvironment()
        {
            // Maze walls
            addWallPositions();
            addWallRotations();
            addWallScales();
            addWallTextures();
            addSymmetricalWalls();

            // Adding additional walls + doors
            addWallsCrossingSymmetry();
            addDoors();

            // Forgot to half the lengths of the walls (scaling is applied from center so need to scale by half)
            for (int i = 0; i < wallScales.Count; i++) // temporary fix (i need sleep..)
            {
                wallScales[i] = new Vector3(wallScales[i].X * 0.5f, wallScales[i].Y, wallScales[i].Z);
            }

            // Add the floor of the maze
            wallPositions.Add(new Vector3(12.5f, -1.0f, -12.5f));
            wallRotations.Add(new Vector3(0.0f, 0.0f, 0.0f));
            wallScales.Add(new Vector3(12.5f, 0.0f, 12.5f));
            wallTexture.Add("Textures/StoneFloor/diffuse.jpg");

            addOuterWalls("Textures/Overgrown/diffuse.png", "Textures/Overgrown/normal.png");
        }

        public void loadEntities(OpenGL_Game.Managers.EntityManager entityManager)
        {
            Entity newEntity;

            for (int i = 0; i < wallPositions.Count; i++)
            {
                if (wallPositions[i] == new Vector3(12.5f, -1.0f, -12.5f)) // if this is the ground entitiy
                {
                    newEntity = new Entity("Ground");
                    newEntity.AddComponent(new ComponentNormalMap("Textures/StoneFloor/normal.png"));
                }
                else
                {
                    newEntity = new Entity("MazeWall");
                    newEntity.AddComponent(new ComponentNormalMap("Textures/BrickWall/Normal2.jpg"));
                }

                newEntity.AddComponent(new ComponentVelocity(0, 0, 0.0f));

                newEntity.AddComponent(new ComponentPosition(wallPositions[i]));
                newEntity.AddComponent(new ComponentRotation(wallRotations[i]));
                newEntity.AddComponent(new ComponentScale(wallScales[i]));

                newEntity.AddComponent(new ComponentGeometry("Geometry/QuadGeometry.txt", true));
                newEntity.AddComponent(new ComponentTexture(wallTexture[i]));
                newEntity.AddComponent(new ComponentAlive());


                entityManager.AddEntity(newEntity);
            }
        }


    }
}
