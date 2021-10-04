using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourcePlacement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ResourcePlacement.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            try
            {
                repository.Insert(entity);
                return Ok("Sukses Insert Data");
                //return StatusCode((int)HttpStatusCode.OK, new { status = (int)HttpStatusCode.OK, data = "Sukses Insert Data" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                //return StatusCode((int)HttpStatusCode.InternalServerError, new { Status = (int)HttpStatusCode.InternalServerError, Message = e.Message });
            }

        }

        [HttpGet]

        public ActionResult Get()
        {
            var temp = repository.Get();
            if (temp == null)
            {
                return NotFound(temp);
                //return StatusCode((int)HttpStatusCode.NotFound, new { status = (int)HttpStatusCode.NotFound, data = "No result" });
            }
            else
            {
                return Ok(temp);
                //return StatusCode((int)HttpStatusCode.OK, new { status = (int)HttpStatusCode.OK, data = temp });
            }
        }

        [HttpGet("{key}")]

        public ActionResult GetKey(Key key)
        {
            var temp = repository.Get(key);
            if (temp == null)
            {
                return NotFound(temp);
                //return StatusCode((int)HttpStatusCode.NotFound, new { status = (int)HttpStatusCode.NotFound, data = "No result" });
            }
            else
            {
                return Ok(temp);
                //return StatusCode((int)HttpStatusCode.OK, new { status = (int)HttpStatusCode.OK, data = temp });
            }
        }

        [HttpPut]

        public ActionResult update(Entity entity)
        {
            try
            {
                var temp = repository.Update(entity);
                return Ok("Sukses Update Data");
                //return StatusCode((int)HttpStatusCode.OK, new { status = (int)HttpStatusCode.OK, data = "Sukses Update Data" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                //return StatusCode((int)HttpStatusCode.InternalServerError, new { Status = (int)HttpStatusCode.InternalServerError, Message = e.Message });
            }

        }

        [HttpDelete("{key}")]

        public ActionResult Delete(Key key)
        {
            try
            {
                var temp = repository.Delete(key);
                return Ok("Sukses Delete Data");
                //return StatusCode((int)HttpStatusCode.OK, new { status = HttpStatusCode.OK, data = "Sukses Delete Data" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                //return StatusCode((int)HttpStatusCode.InternalServerError, new { Status = (int)HttpStatusCode.InternalServerError, Message = e.Message });
            }
        }
    }
}
