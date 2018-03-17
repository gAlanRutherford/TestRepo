using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnsibleAPI.Domain.Entities;

namespace AnsibleAPI.Domain.Abstract
{
    public interface IEntityRepository
    {
        IEnumerable<Entity> GetEntities();
        Entity GetEntiy(int entityId);
        bool AddEntities(IEnumerable<Entity> entities);
        bool AddEntity(Entity entity);
    }
}
