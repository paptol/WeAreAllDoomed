using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Input;

namespace OpenGL_Game.Objects
{
    public class Camera
    {
        // Camera attributes
        public Vector3 Position;
        public Vector3 Front;
        public Vector3 Up;
        public Vector3 Right;
        public Vector3 WorldUp;
        public bool Collision;
        // Eular Angles
        public double Yaw;
        public double Pitch;

        // Camera options
        public float MouseSensitivity;
        public float Zoom;

        public enum CameraMovement
        {
            Forward,
            Backward,
            Left,
            Right
        }

        // Creates a camera object with default attributes.
        public Camera()
        {
            Position = new Vector3(12.5f, 0.0f, -12.5f);
            Front = new Vector3(0.0f, 0.0f, -1.0f);
            Right = new Vector3(0.0f, 0.0f, 0.0f);
            WorldUp = new Vector3(0.0f, 1.0f, 0.0f);
            Yaw = -90.0f;
            Pitch = 0.0f;
            MouseSensitivity = 0.1f;
            Zoom = 45.0f;
            UpdateCameraAttributes();
        }

        // Process the camera movement using the keyboard to move around.
        public void ProcessMovement(CameraMovement direction, int movementSpeed)
        {
            MyGame.OldCameraPosition = new Vector2 (Position.X, Position.Z);
             if (Collision == true)
                movementSpeed = movementSpeed / 2;
            float velocity = movementSpeed * MyGame.dt;

            if (direction == CameraMovement.Forward)
                Position += Front * velocity;
            if (direction == CameraMovement.Backward)
                Position -= Front * velocity;
            if (direction == CameraMovement.Left)
                Position -= Right * velocity;
            if (direction == CameraMovement.Right)
                Position += Right * velocity;

            //Stick to floor
            Position.Y = 0;
            Collision = false;
            MyGame.NewCameraPosition = new Vector2(Position.X, Position.Z);
        }

        // Process the camera movement using the x and y offset of the mouse to look around.
        public void ProcessMouseMovement(float xOffset, float yOffset)
        {
            xOffset *= MouseSensitivity;
            yOffset *= MouseSensitivity;

            Yaw += xOffset;
            Pitch += yOffset;

            // Constrain pitch so screen doesnt flip
            if (Pitch > 89.0f)
                Pitch = 89.0f;
            if (Pitch < -89.0f)
                Pitch = -89.0f;

            UpdateCameraAttributes();
        }

        // Calculates and updates the new front, right and up vectors whenever the camera changes.
        private void UpdateCameraAttributes()
        {
            // Calculate the front vector
            Vector3 front;
            front.X = (float)(Math.Cos(MathHelper.DegreesToRadians(Yaw)) * Math.Cos(MathHelper.DegreesToRadians(Pitch)));
            front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(Pitch));
            front.Z = (float)(Math.Sin(MathHelper.DegreesToRadians(Yaw)) * Math.Cos(MathHelper.DegreesToRadians(Pitch)));

            Front = front.Normalized();

            // Recalculate the up and right vector
            Right = Vector3.Normalize(Vector3.Cross(Front, WorldUp));
            Up = Vector3.Normalize(Vector3.Cross(Right, Front));
        }

        // Return the view matrix using the camera position orientation and up vectors.
        public Matrix4 getViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + Front, Up);
        }

    }
}
