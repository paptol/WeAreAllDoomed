using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using OpenGL_Game.Systems;
using OpenGL_Game.Objects;

namespace OpenGL_Game.Managers
{
    class SystemManager
    {
        List<ISystem> systemList = new List<ISystem>();

        public SystemManager()
        {
        }

        public void ActionSystems(EntityManager entityManager)
        {
            List<Entity> entityList = entityManager.Entities();
            foreach(ISystem system in systemList)
            {
                //Don't render in update call
                if (system != FindSystem("SystemRender"))
                {
                    foreach (Entity entity in entityList)
                    {
                        //Only update if alive
                        if((entity.Mask & Components.ComponentTypes.COMPONENT_ALIVE) == Components.ComponentTypes.COMPONENT_ALIVE)
                        {
                            system.OnAction(entity);
                        }
                    }
                }
            }
        }

        public void RenderSystems(EntityManager entityManager)
        {
            List<Entity> entityList = entityManager.Entities();

            ISystem system = FindSystem("SystemRender");

            foreach (Entity entity in entityList)
            {
                //Only render if alive
                if ((entity.Mask & Components.ComponentTypes.COMPONENT_ALIVE) == Components.ComponentTypes.COMPONENT_ALIVE)
                {
                    system.OnAction(entity);
                }
            }
        }

        public void AddSystem(ISystem system)
        {
            ISystem result = FindSystem(system.Name);
            //Debug.Assert(result != null, "System '" + system.Name + "' already exists");
            systemList.Add(system);
        }

        private ISystem FindSystem(string name)
        {
            return systemList.Find(delegate(ISystem system)
            {
                return system.Name == name;
            }
            );
        }
    }
}
