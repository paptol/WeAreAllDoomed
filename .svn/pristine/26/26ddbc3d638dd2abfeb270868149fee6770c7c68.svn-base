using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using OpenGL_Game.Objects;

namespace OpenGL_Game.Managers
{
    public class EntityManager
    {
        List<Entity> entityList;

        public EntityManager()
        {
            entityList = new List<Entity>();
        }

        public void AddEntity(Entity entity)
        {
            Entity result = FindEntity(entity.Name);
            //Debug.Assert(result != null, "Entity '" + entity.Name + "' already exists");
            entityList.Add(entity);
        }
        
        public void AddEntity(List<Entity> entities)
        {
            foreach (Entity i in entities)
                entityList.Add(i);     
        }

        private Entity FindEntity(string name)
        {
            return entityList.Find(delegate (Entity e)
            {
                return e.Name == name;
            }
            );
        }

        public List<Entity> Entities()
        {
            return entityList;
        }
    }
}
