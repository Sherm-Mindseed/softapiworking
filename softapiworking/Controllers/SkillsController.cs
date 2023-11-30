using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using softapiworking.DTO;
using softapiworking.mydb;
using System.Net;

namespace softapiworking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : Controller
    {
        private readonly MydbContext _dbContext;

        public SkillsController(MydbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet("GetSkills")]
        public async Task<ActionResult<List<SkillDTO>>> Get()
        {
            var List = await _dbContext.Skills.Select(x => new SkillDTO
            {
                Idskills = x.Idskills,
                Name = x.Name,
                Desc = x.Desc,
                Creation=x.Creation

            }).ToListAsync();
            if (List.Count <= 0 || List == null)
            {
                return NotFound();
            }
            else { return List; }
        }

        [HttpGet("GetSkillByName")]
        public async Task<ActionResult<SkillDTO>> GetSkillByName(string name)
        {
            SkillDTO skill = await Task.Run(()=>_dbContext.Skills.Where(w=>w.Name == name).Select(
                s=>new SkillDTO
                {
                    Idskills=s.Idskills,
                    Name = s.Name,
                    Desc = s.Desc,
                    Creation=s.Creation
                }).FirstOrDefault());
            if (skill == null) { return NotFound(); }
            return skill;
        }

        [HttpGet("GetSkillById")]
        public async Task<ActionResult<SkillDTO>> GetEmployeeById(int Id)
        {
            SkillDTO skill = await Task.Run(() => _dbContext.Skills.Where(w => w.Idskills == Id)?.Select(
                s => new SkillDTO
                {
                    Idskills = s.Idskills,
                    Name = s.Name,
                    Desc = s.Desc,
                    Creation = s.Creation
                })
                .FirstOrDefault());
            if (skill == null)
            {
                return NotFound();
            }
            else
            {
                return skill;
            }
        }
        [HttpPost("InsertSkill")]
        public async Task<ActionResult<SkillDTO>> InsertSkill(SkillDTO skill)
        {
            var entity = new Skill()
            {
                Name = skill.Name,
                Desc=skill.Desc,
                Creation = skill.Creation
            };
            
            _dbContext.Skills.Add(entity);
            await _dbContext.SaveChangesAsync();
            SkillDTO skillDto = new SkillDTO()
            {
                Idskills = entity.Idskills,
                Name = entity.Name,
                Desc = entity.Desc,
                Creation = entity.Creation
            };
            return skillDto;
        }
        
        [HttpPut("UpdateSkill")]
        public async Task<HttpStatusCode> UpdateSkill(SkillDTO skill)
        {
            var entity = await _dbContext.Skills.Where(s => s.Idskills == skill.Idskills).FirstOrDefaultAsync();
            entity.Name = skill.Name;
            entity.Desc = skill.Desc;
            entity.Creation = skill.Creation;
            await _dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteSkill/{Id}")]
        public async Task<HttpStatusCode> DeleteUser(int Id)
        {
            var entity = new Skill()
            {
                Idskills = Id
            };
            _dbContext.Skills.Attach(entity);
            _dbContext.Skills.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
    
}
