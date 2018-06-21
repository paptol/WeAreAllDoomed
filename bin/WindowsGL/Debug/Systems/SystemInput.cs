using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGL_Game.Objects;
using OpenGL_Game.Components;
using OpenTK.Input;
using OpenTK;

namespace OpenGL_Game.Systems
{
    class SystemInput : ISystem
    {
        //Keyboard
        private KeyboardState KeyStates;
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_INPUT);
        
        //Mouse
        Vector2 lastMousePosition;
        Vector2 deltaMousePosition;
        bool firstMouse = true;

        public string Name
        {
            get { return "SystemInput"; }
        }

        public void OnAction(Entity entity)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                KeyStates = Keyboard.GetState();

                int speed;

                if (KeyStates.IsKeyDown(Key.LShift))
                {
                    speed = 4;
                }
                else
                {
                    speed = 2;
                }

                //Forward and Backwards motions
                if (KeyStates.IsKeyDown(Key.W) || KeyStates.IsKeyDown(Key.Up))
                {
                    MyGame.gameInstance.playerCamera.ProcessMovement(Camera.CameraMovement.Forward, speed);
                }
                else if (KeyStates.IsKeyDown(Key.S) || KeyStates.IsKeyDown(Key.Down))
                {
                    MyGame.gameInstance.playerCamera.ProcessMovement(Camera.CameraMovement.Backward, speed);
                }

                //Left and Right motions
                if (KeyStates.IsKeyDown(Key.A) || KeyStates.IsKeyDown(Key.Left))
                {
                    MyGame.gameInstance.playerCamera.ProcessMovement(Camera.CameraMovement.Left, speed);
                }
                else if (KeyStates.IsKeyDown(Key.D) || KeyStates.IsKeyDown(Key.Right))
                {
                    MyGame.gameInstance.playerCamera.ProcessMovement(Camera.CameraMovement.Right, speed);
                }

                //Update Cursor
                processMouseMove();

                //Exit Game
                if (GamePad.GetState(1).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Key.Escape))
                    MyGame.ExitGame();
            }
        }


        public void resetCursor()
        {
           OpenTK.Input.Mouse.SetPosition(MyGame.gameInstance.Bounds.Left + MyGame.gameInstance.Bounds.Width / 2, MyGame.gameInstance.Bounds.Top + MyGame.gameInstance.Bounds.Height / 2);
           lastMousePosition = new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);
        }

        private void processMouseMove()
        {
            
            deltaMousePosition = lastMousePosition - new Vector2(OpenTK.Input.Mouse.GetState().X, OpenTK.Input.Mouse.GetState().Y);

            MyGame.gameInstance.playerCamera.ProcessMouseMovement(-deltaMousePosition.X, deltaMousePosition.Y);
            resetCursor();
        }
    }
}
