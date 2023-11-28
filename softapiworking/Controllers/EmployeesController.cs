using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
using softapiworking.DTO;
using softapiworking.mydb;
using System.Net;

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

        [HttpGet("GetEmployeeById")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int Id)
        {
            EmployeeDTO employee = await Task.Run(() => _dbContext.Employees.Where(w => w.Idemployee == Id)?.Select(
                s => new EmployeeDTO
                {
                    Id = s.Idemployee,
                    Name = s.Name,
                    Surname = s.Surname,
                    Hired = s.Hired,
                    skills = s.SkillsIdskills
                })
                .FirstOrDefault());
            if (employee==null)
            {
                return NotFound();
            }
            else
            {
                return employee;
            }
        }

        [HttpPost("InsertEmployee")]
        public async Task<HttpStatusCode> InsertUser(EmployeeDTO employee)
        {
            var entity = new Employee()
            {
                Name = employee.Name,
                Surname = employee.Surname,
                Hired = employee.Hired,
                SkillsIdskills = employee.skills
            };

            _dbContext.Employees.Add(entity);
            await _dbContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }
        [HttpPut("UpdateEmployee")]
        public async Task<HttpStatusCode> UpdateEmployee(EmployeeDTO employee)
        {
            var entity = await _dbContext.Employees.Where(x=>x.Idemployee==employee.Id).FirstOrDefaultAsync();
            entity.Name= employee.Name;
            entity.Surname= employee.Surname;
            entity.Hired= employee.Hired;
            entity.SkillsIdskills= employee.skills;
            await _dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }

   
}
