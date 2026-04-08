using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using PasificKodeA.Models;
using PasificKodeA.Repositories;

namespace PasificKodeA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository _repo;

        public EmployeesController(EmployeeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            var emp = _repo.GetById(id);
            if (emp == null) return NotFound();
            return Ok(emp);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Employee emp)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _repo.Add(emp);
            return CreatedAtAction(nameof(Get), new { id = emp.EmployeeId }, emp);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Employee emp)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != emp.EmployeeId) return BadRequest("Id mismatch");
            _repo.Update(emp);
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
