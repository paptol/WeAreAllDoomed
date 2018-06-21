using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenGL_Game
{
    class Level1 : MazeLevel
    {      
        protected override void addDoors()
        {
            //wallPositions.Add(new Vector3(12.5f, 1.25f, -16));
            //wallRotations.Add(new Vector3(90, 0, 0));
            //wallScales.Add(new Vector3(1, 0, 1.25f));

            //wallPositions.Add(new Vector3(12.5f, 1.25f, -19));
            //wallRotations.Add(new Vector3(90, 0, 0));
            //wallScales.Add(new Vector3(1, 0, 1.25f));
        }

        protected override void addWallPositions()
        {
            wallPositions.Add(new Vector3(1, 0, -2));
            wallPositions.Add(new Vector3(2, 0, -3));
            wallPositions.Add(new Vector3(2, 0, -4));
            wallPositions.Add(new Vector3(3, 0, -2.5f));
            wallPositions.Add(new Vector3(4.5f, 0, -4));
            wallPositions.Add(new Vector3(5.5f, 0, -2));
            wallPositions.Add(new Vector3(5, 0, -4.5f));
            wallPositions.Add(new Vector3(8.5f, 0, -3));
            wallPositions.Add(new Vector3(7, 0, -2));
            wallPositions.Add(new Vector3(9, 0, -1));
            wallPositions.Add(new Vector3(10.5f, 0, -2));
            wallPositions.Add(new Vector3(12, 0, -2));
            wallPositions.Add(new Vector3(2.5f, 0, -6));
            wallPositions.Add(new Vector3(2, 0, -7.5f));
            wallPositions.Add(new Vector3(8, 0, -5.5f));
            wallPositions.Add(new Vector3(9, 0, -7.5f));
            wallPositions.Add(new Vector3(7, 0, -7));
            wallPositions.Add(new Vector3(6, 0, -8));
            wallPositions.Add(new Vector3(5, 0, -9.5f));
            wallPositions.Add(new Vector3(2.5f, 0, -9));
            wallPositions.Add(new Vector3(2, 0, -10));
            wallPositions.Add(new Vector3(3, 0, -12));
            wallPositions.Add(new Vector3(6, 0, -10));
            wallPositions.Add(new Vector3(8, 0, -11));
            wallPositions.Add(new Vector3(10, 0, -4));
            wallPositions.Add(new Vector3(12, 0, -6));
            wallPositions.Add(new Vector3(11, 0, -8));
            wallPositions.Add(new Vector3(10.5f, 0, -9));
            wallPositions.Add(new Vector3(11, 0, -11));
            wallPositions.Add(new Vector3(7, 0, -10.5f));
            wallPositions.Add(new Vector3(10, 0, -15.5f));
            wallPositions.Add(new Vector3(6, 0, -13));
            wallPositions.Add(new Vector3(2, 0, -13));
            wallPositions.Add(new Vector3(1, 0, -14));
            wallPositions.Add(new Vector3(3, 0, -15));
            wallPositions.Add(new Vector3(6, 0, -16.5f));
            wallPositions.Add(new Vector3(8, 0, -14));
            wallPositions.Add(new Vector3(7.5f, 0, -16));
            wallPositions.Add(new Vector3(1, 0, -17));
            wallPositions.Add(new Vector3(2, 0, -18));
            wallPositions.Add(new Vector3(3.5f, 0, -17));
            wallPositions.Add(new Vector3(4, 0, -19));
            wallPositions.Add(new Vector3(8, 0, -18));
            wallPositions.Add(new Vector3(9, 0, -19));
            wallPositions.Add(new Vector3(5.5f, 0, -21));
            wallPositions.Add(new Vector3(1.5f, 0, -22));
            wallPositions.Add(new Vector3(2, 0, -22));
            wallPositions.Add(new Vector3(4.5f, 0, -22));
            wallPositions.Add(new Vector3(6, 0, -23));
            wallPositions.Add(new Vector3(8, 0, -21));
            wallPositions.Add(new Vector3(9.5f, 0, -21));
            wallPositions.Add(new Vector3(11, 0, -22));
            wallPositions.Add(new Vector3(12, 0, -21));
            wallPositions.Add(new Vector3(10.5f, 0, -20));
            wallPositions.Add(new Vector3(9, 0, -24));
            wallPositions.Add(new Vector3(11, 0, -23.5f));
            wallPositions.Add(new Vector3(12, 0, -10));
            wallPositions.Add(new Vector3(12, 0, -24.5f));
            wallPositions.Add(new Vector3(11, 0, -16));
            wallPositions.Add(new Vector3(11, 0, -19));
        }

        protected override void addWallRotations()
        {
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 90, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
            wallRotations.Add(new Vector3(90, 0, 0));
        }

        protected override void addWallScales()
        {
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(7, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(4, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(5, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(5, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(4, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(4, 0, 1.25f));
            wallScales.Add(new Vector3(4, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(1, 0, 1.25f));
            wallScales.Add(new Vector3(7, 0, 1.25f));
            wallScales.Add(new Vector3(4, 0, 1.25f));
            wallScales.Add(new Vector3(4, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(5, 0, 1.25f));
            wallScales.Add(new Vector3(4, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(4, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(1, 0, 1.25f));
            wallScales.Add(new Vector3(4, 0, 1.25f));
            wallScales.Add(new Vector3(5, 0, 1.25f));
            wallScales.Add(new Vector3(4, 0, 1.25f));
            wallScales.Add(new Vector3(4, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(3, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(1, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(1, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
            wallScales.Add(new Vector3(2, 0, 1.25f));
        }

        protected override void addWallTextures()
        {
            for (int i = 0; i < 60; i++)
            {
                wallTexture.Add("Textures/BrickWall/diffuse.png");
                wallNormals.Add("Textures/BrickWall/Normal2.jpg");
            }
        }
    }
}
