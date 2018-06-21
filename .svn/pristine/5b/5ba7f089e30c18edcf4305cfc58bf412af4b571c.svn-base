using OpenGL_Game.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGL_Game.Objects;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using static OpenGL_Game.Components.ComponentAI;

namespace OpenGL_Game.Systems
{
    class SystemAI : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_ROTATION | ComponentTypes.COMPONENT_AI | ComponentTypes.COMPONENT_VELOCITY);

        float droneSpeed = 0.5f;
        int travelCost;
        int targetCost;
        int fCost;
        int pathNumber;
        Node currentNode;
        Node StartNode;
        Vector2 targetNode;
        List<Node> ClosedList = new List<Node>();
        List<Node> OpenList = new List<Node>();
        List<Node> path = new List<Node>();



        public string Name
        {
            get { return "SystemAI"; }
        }

        public void OnAction(Entity entity)
        {


            if ((entity.Mask & MASK) == MASK)
            {

                List<IComponent> components = entity.Components;

                IComponent positionComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });

                ComponentAI aiComponent = (ComponentAI)components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_AI;
                });
                AIStates state = aiComponent.CurrentState;

                IComponent velocityComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_VELOCITY;
                });


                ComponentRotation rotationComponent = (ComponentRotation)components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_ROTATION;
                });
                Vector2 currentRotation = rotationComponent.Rotation.Xz;

                if (MyGame.NewCameraPosition != new Vector2(0, 0))
                {
                   PathFinding((ComponentPosition)positionComponent);
                }
                if (path.Count > 0)
                {
                    FollowPath((ComponentVelocity)velocityComponent, (ComponentPosition)positionComponent);
                }
                PlayerDetection((ComponentPosition)positionComponent, (ComponentVelocity)velocityComponent, state);


            }
        }

        private void PlayerDetection(ComponentPosition pos, ComponentVelocity vel, AIStates state)
        {
            
            if (state == AIStates.Wandering)
            {

                Vector2 AIpos = new Vector2(pos.Position.X, pos.Position.Z);
                Vector2 v = MyGame.NewCameraPosition - AIpos;

                float dot = MyGame.DotProduct(v.Normalized(), AIpos.Normalized());

                if (dot > 0.7)
                {
                    droneSpeed = 0.8f;
                }
                else { droneSpeed = 0.5f; }
            }
        }


        private void PathFinding(ComponentPosition pos)
        {
            bool pathMade = false;
            double xPos = Math.Round(pos.Position.X * 2) / 2;
            double yPos = Math.Round(pos.Position.Z * 2) / 2;
            if (targetNode != new Vector2((float)xPos, (float)yPos))
            {
                xPos = Math.Round(MyGame.NewCameraPosition.X * 2) / 2;
                yPos = Math.Round(MyGame.NewCameraPosition.Y * 2) / 2;
                if (MyGame.gameInstance.pathFinding.Nodes.Contains(new Vector2((float)xPos, (float)yPos)))
                    {
                    do
                    {
                        xPos = Math.Round(pos.Position.X * 2) / 2;
                        yPos = Math.Round(pos.Position.Z * 2) / 2;
                        Vector2 currentNodePos = new Vector2((float)xPos, (float)yPos);
                        xPos = Math.Round(MyGame.NewCameraPosition.X * 2) / 2;
                        yPos = Math.Round(MyGame.NewCameraPosition.Y * 2) / 2;


                        targetNode = new Vector2((float)xPos, (float)yPos);

                        if (OpenList.Count == 0)
                        {
                            StartNode = new Node(currentNodePos, 0, 100);

                            ClosedList.Add(StartNode);
                            path.Add(StartNode);
                            currentNode = StartNode;
                        }

                        float cX = currentNode.Position.X;
                        float cY = currentNode.Position.Y;
                        List<Vector2> adjacentNodes = new List<Vector2>();
                        adjacentNodes.Add(new Vector2(cX - 0.5f, cY));
                        adjacentNodes.Add(new Vector2(cX + 0.5f, cY));
                        adjacentNodes.Add(new Vector2(cX, cY - 0.5f));
                        adjacentNodes.Add(new Vector2(cX, cY + 0.5f));

                        bool removed = false;
                        do
                        {
                            removed = false;
                            for (int i = 0; i < (adjacentNodes.Count); i++)
                            {

                                if (adjacentNodes.Count == 0) { break; }
                                for (int c = 0; c < ClosedList.Count; c++)
                                {
                                    if (adjacentNodes[i] == ClosedList[c].Position)
                                    {
                                        adjacentNodes.Remove(adjacentNodes[i]);
                                        c = 0;
                                        removed = true;
                                        break;

                                    }

                                    if (adjacentNodes.Count == 0) { break; }
                                }
                                if (removed == true) { break; }
                                bool isInOpen = false;
                                if (adjacentNodes.Count == 0) { break; }
                                if (MyGame.gameInstance.pathFinding.Nodes.Contains(adjacentNodes[i]) == false)
                                {
                                    adjacentNodes.Remove(adjacentNodes[i]);
                                    removed = true;
                                    break;
                                }

                                else
                                {
                                    float tX = Math.Abs(adjacentNodes[i].X - targetNode.X);
                                    float tY = Math.Abs(adjacentNodes[i].Y - targetNode.Y);

                                    targetCost = (int)((tX + tY) * 2);
                                    travelCost = ClosedList.Count;
                                    fCost = travelCost + targetCost;
                                    bool open = false;
                                    do
                                    {
                                        open = false;
                                        for (int j = 0; j < OpenList.Count; j++)
                                        {
                                            if (OpenList[j].Position == adjacentNodes[i])
                                            {
                                                isInOpen = true;
                                                if (OpenList[j].fCost > fCost)
                                                {
                                                    OpenList.Remove(OpenList[j]);
                                                    OpenList.Add(new Node(adjacentNodes[i], travelCost, targetCost, currentNode));
                                                    open = true;
                                                }
                                            }

                                        }
                                    } while (open == true);

                                    if (isInOpen != true)
                                    {
                                        OpenList.Add(new Node(adjacentNodes[i], travelCost, targetCost, currentNode));
                                        isInOpen = false;
                                    }
                                }
                            }

                        } while (removed == true);

                        fCost = 1000;
                        Node nextNode = currentNode;
                        for (int i = 0; i < OpenList.Count; i++)
                        {
                            if (OpenList[i].fCost == fCost)
                            {
                                if (currentNode == OpenList[i].parentNode)
                                {
                                    nextNode = OpenList[i];
                                    fCost = OpenList[i].fCost;
                                }
                            }
                            else if (OpenList[i].fCost < fCost)

                            {

                                nextNode = OpenList[i];
                                fCost = OpenList[i].fCost;
                            }
                        }

                        OpenList.Remove(nextNode);
                        ClosedList.Add(nextNode);

                        currentNode = nextNode;
                        for (int i = 0; i < ClosedList.Count; i++)
                        {
                            if (ClosedList[i].Position == targetNode)
                            {
                                pathMade = true;
                                break;
                            }
                        }

                    } while (pathMade == false);
                }
            }
            MakePath();
        }

        private void MakePath()
        {
            path.Clear();
            if (ClosedList.Count > 1)
            {
                Node n = ClosedList[ClosedList.Count - 1];
                path.Add(n);
                do
                {
                    n = n.parentNode;
                    path.Add(n);
                } while (n != StartNode);
                OpenList.Clear();
                ClosedList.Clear();
                path.Reverse();
                pathNumber = path.Count;
            }
        }


        private void FollowPath(ComponentVelocity vel, ComponentPosition pos)
        {
           int p = 0;
                if (path[p + 1].Position.X == path[p].Position.X)
                {
                    if (path[p + 1].Position.Y < path[p].Position.Y)
                    {
                        vel.Velocity = new Vector3(0, 0, -droneSpeed);
                    }
                    else
                    {
                        vel.Velocity = new Vector3(0, 0, droneSpeed);
                    }
                }
                else if (path[p + 1].Position.Y == path[p].Position.Y)
                {
                    if (path[p + 1].Position.X < path[p].Position.X)
                    {
                        vel.Velocity = new Vector3(-droneSpeed, 0, 0);
                    }
                    else
                    {
                        vel.Velocity = new Vector3(droneSpeed, 0, 0);
                    }
                }
            Vector2 v = new Vector2(pos.Position.X, pos.Position.Z);
                if (v == path[p + 1].Position) { p++;  }
            }
        }
    }


