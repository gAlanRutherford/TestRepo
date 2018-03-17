using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using AnsibleAPI.Domain.Abstract;
using AnsibleAPI.Domain.Entities;

namespace AnsibleAPI.Controllers
{
    public class EntityController : ApiController
    {
        public readonly IEntityRepository _entityRepository;
        public EntityController(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        [HttpGet]
        [Route("v1/entities/{entityId?}")]
        public IHttpActionResult GetEntity(int? entityId = null)
        {
            if (entityId != null)
            {
                Entity entity = _entityRepository.GetEntiy(entityId.Value);
                if (entity == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(entity);
                }
            }
            else
            {
                IEnumerable<Entity> entities = _entityRepository.GetEntities();
                return Ok(entities);
            }
            return Ok();
        }

        [HttpPost]
        [Route("v1/entity")]
        public IHttpActionResult AddEntity([FromBody] Entity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            _entityRepository.AddEntity(entity);
            return Ok();
        }

        [HttpPost]
        [Route("v1/entities")]
        public IHttpActionResult AddEntities(IEnumerable<Entity> entities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            _entityRepository.AddEntities(entities);
            return Ok();
        }
    }
}
