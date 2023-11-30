using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using softapiworking.DTO;
using softapiworking.mydb;
using System.Net;

namespace softapiworking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : Controller
    {
        private readonly MydbContext _dbContext;

        public JobController(MydbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet("GetJobs")]
        public async Task<ActionResult<List<JobsDTO>>> Get()
        {
            var List = await _dbContext.Jobs.Select(x => new JobsDTO
            {
                Idjobs=x.Idjobs,
                Jobname = x.Jobname,
          
            }).ToListAsync();
            if (List.Count <= 0 || List == null)
            {
                return NotFound();
            }
            else { return List; }
        }

        [HttpGet("GetJobyId")]
        public async Task<ActionResult<EmployeeDTO>> GetJobById(int Id)
        {
            EmployeeDTO employee = await Task.Run(() => _dbContext.Jobs.Where(w => w.Idjobs == Id)?.Select(
                s => new EmployeeDTO
                {
                    Id = s.Idjobs,
                    Name = s.Jobname,
                    skills = s.Skills,
                   
                })
                .FirstOrDefault());
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return employee;
            }
        }

        [HttpPost("InsertJob")]
        public async Task<HttpStatusCode> InsertJob(JobsDTO job)
        {
            var entity = new Job()
            {
                Jobname=job.Jobname,
                
            };

            _dbContext.Jobs.Add(entity);
            await _dbContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }
        [HttpPut("UpdateJob")]
        public async Task<HttpStatusCode> UpdateJob(JobsDTO job)
        {
            var entity = await _dbContext.Jobs.Where(s => s.Idjobs == job.Idjobs).FirstOrDefaultAsync();
            entity.Jobname=job.Jobname;
           
            await _dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

    }
}
