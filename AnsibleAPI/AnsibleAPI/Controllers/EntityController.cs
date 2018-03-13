using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AnsibleAPI.Models;
using Newtonsoft.Json;

namespace AnsibleAPI.Controllers
{
    public class EntityController : ApiController
    {
        public EntityController()
        {

        }

        [HttpGet]
        [Route("v1/entities/{entityId}")]
        public HttpResponseMessage GetEntity(int entityId)
        {
            var entity = new Entity()
            {
                Id = 1,
                Name = "Test Entity"
            };
            var response = Request.CreateResponse(HttpStatusCode.OK, entity);
            return response;
        }
    }
}
