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
        IEnumerable<Entity> Entities { get; set; }
    }
}
