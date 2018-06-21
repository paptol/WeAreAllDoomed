using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenGL_Game.Components;
using OpenGL_Game.Objects;

namespace OpenGL_Game.Systems
{
    class SystemPhysics : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_VELOCITY);

        public string Name
        {
            get { return "SystemPhysics"; }
        }

        public void OnAction(Entity entity)
        {
            if (entity.Name == "Drone")
            {

            }

            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent positionComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });

                IComponent velocityComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_VELOCITY;
                });

                if (entity.Name == "Player") // update the player location component with current camera position.
                {
                    ((ComponentPosition)positionComponent).Position = MyGame.gameInstance.playerCamera.Position;
                }

                Collision();
                Motion((ComponentPosition)positionComponent, (ComponentVelocity)velocityComponent);
            }
        }

        private void Motion(ComponentPosition pos, ComponentVelocity vel)
        {
            pos.Position += vel.Velocity * MyGame.dt;
        }

        private void Collision()
        {
            Vector2 oldPosition = MyGame.OldCameraPosition;
            Vector2 newPosition = MyGame.NewCameraPosition;
            if (newPosition == new Vector2(0, 0) && oldPosition.X > 1)
            {
                newPosition = oldPosition;
            }
            foreach (MazeLevel.WallPoints w in MyGame.currentLevelLoaded.wallPlanePositions)
            {
                float dx = w.endPosition.X - w.startPosition.X;
                float dy = w.endPosition.Y - w.startPosition.Y;

                Vector2 normal = new Vector2(-dy, dx);
                normal.Normalize();

                float oldPos = MyGame.DotProduct(normal, oldPosition - w.startPosition);
                float newPos = MyGame.DotProduct(normal, newPosition - w.startPosition);

                float q = (newPos * oldPos) - 0.01f;

                if (q < 0)
                {
                    dx = newPosition.X - oldPosition.X;
                    dy = newPosition.Y - oldPosition.Y;
                    normal = new Vector2(-dy, dx);

                    oldPos = MyGame.DotProduct(normal, w.startPosition - oldPosition);
                    newPos = MyGame.DotProduct(normal, w.endPosition - oldPosition);
                    float z = (newPos * oldPos) + 0.01f;
                    if ((newPos * oldPos) < 0)
                   {
                        if (w.startPosition.X == w.endPosition.X)
                        {
                            MyGame.gameInstance.playerCamera.Position = new Vector3(oldPosition.X, 0, newPosition.Y);
                            MyGame.gameInstance.playerCamera.Collision = true;
                        }
                        if (w.startPosition.Y == w.endPosition.Y)
                        {
                            MyGame.gameInstance.playerCamera.Position = new Vector3(newPosition.X, 0, oldPosition.Y);
                            MyGame.gameInstance.playerCamera.Collision = true;
                        }
                    }
                }
            }
        }
    }
}
    

