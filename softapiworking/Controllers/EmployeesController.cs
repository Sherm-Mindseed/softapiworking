using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
using softapiworking.DTO;
using softapiworking.mydb;

namespace softapiworking.Controllers
{
    //api controler for Employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly MydbContext _dbContext;

        public EmployeesController(MydbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet("GetEmployees")]
        public async Task<ActionResult<List<EmployeeDTO>>> Get()
        {
            var List=await _dbContext.Employees.Select(x => new EmployeeDTO
            {
                Id= x.Idemployee,
                Name= x.Name,
                Surname= x.Surname,
                Hired= x.Hired,
                skills = x.SkillsIdskills
            }).ToListAsync();
            if (List.Count <= 0 || List==null) {
                return NotFound();
            }else { return List; }
        }
     }  
}
