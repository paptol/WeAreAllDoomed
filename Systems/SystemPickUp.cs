using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGL_Game.Components;
using OpenGL_Game.Objects;
using System.IO;
using static OpenGL_Game.Components.ComponentAI;


namespace OpenGL_Game.Systems
{
    class SystemPickUp : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_HEALTH | ComponentTypes.COMPONENT_AMMO | ComponentTypes.COMPONENT_AI | ComponentTypes.COMPONENT_PICK_UP | ComponentTypes.COMPONENT_ALIVE);


        public string Name
        {
            get { return "SystemPickUp"; }
        }

        public void OnAction(Entity entity)
        {
         
         
            if (entity.Name == "Player")  
             {
               
                //Delete(entity);
            
            }
            
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent healthComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_HEALTH;
                });

                IComponent ammoComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_AMMO;
                });

                ComponentAI aiComponent = (ComponentAI)components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_AI;
                });
                AIStates state = aiComponent.CurrentState;


                IComponent pickupComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_PICK_UP;
                });

                IComponent aliveComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_ALIVE;
                });

                PowerUp((ComponentHealth)healthComponent, (ComponentAmmo)ammoComponent, (ComponentPickUp)pickupComponent,(ComponentAlive)(aliveComponent));
            

               if (pickupComponent==null && aliveComponent==null)
                {
                    Delete(entity);
                }
            }
        }

        private void PowerUp(ComponentHealth h, ComponentAmmo a, ComponentPickUp up , ComponentAlive alive)
        {
            
            h.Health += up.Pick_health;
            a.Ammo += up.Pick_ammo;
            

            if(up.Pick_health == h.Health)
            {
               Delete(alive);
            }
            if (up.Pick_ammo == a.Ammo)
            {
                Delete(alive);
            }

          
        }

        private void Delete(ComponentAlive alive)
        {
            throw new NotImplementedException();
        }

        public void Delete(Entity entity)
        {

            entity.Delete();
        }
    }
}







