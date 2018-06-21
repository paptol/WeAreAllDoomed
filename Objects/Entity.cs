using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using OpenGL_Game.Components;

namespace OpenGL_Game.Objects
{
    public class Entity
    {
        string name;
        List<IComponent> componentList = new List<IComponent>();
        ComponentTypes mask;
 
        public Entity(string name)
        {
            this.name = name;
        }

        /// <summary>Adds a single component</summary>
        public void AddComponent(IComponent component)
        {
            Debug.Assert(component != null, "Component cannot be null");

            componentList.Add(component);
            mask |= component.ComponentType;
        }

        /// <summary>Stops the entity being updated or drawn.</summary>
        public void Delete()
        {
            mask &= ~ComponentTypes.COMPONENT_ALIVE;
        }

        

        public String Name
        {
            get { return name; }
        }

        public ComponentTypes Mask
        {
            get { return mask; }
        }

        public List<IComponent> Components
        {
            get { return componentList; }
        }

       
    }
}
