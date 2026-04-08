using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using PasificKodeA.Models;
using PasificKodeA.Repositories;

namespace PasificKodeA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentRepository _repo;

        public DepartmentsController(DepartmentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Department> Get(int id)
        {
            var dept = _repo.GetAll().FirstOrDefault(d => d.DepartmentId == id);
            if (dept == null) return NotFound();
            return Ok(dept);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Department dept)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _repo.Add(dept);
            return CreatedAtAction(nameof(Get), new { id = dept.DepartmentId }, dept);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Department dept)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dept.DepartmentId) return BadRequest("Id mismatch");
            _repo.Update(dept);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _repo.Delete(id);
            return NoContent();
        }
    }
}
