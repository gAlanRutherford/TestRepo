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
        public HttpResponseMessage GetEntity(int? entityId = null)
        {
            var entities = _entityRepository.Entities;
            HttpResponseMessage response;
            if (entityId != null)
            {
                Entity entity = entities.FirstOrDefault(x => x.Id == entityId);
                if (entity == null)
                {
                    response = Request.CreateErrorResponse(HttpStatusCode.NotFound, new ArgumentException("Entity Id does not exist"));
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, entities);
            }
            return response;
        }
    }
}
