using softapiworking.mydb;

namespace softapiworking.DTO
{
    public class JobsDTO
    {
        public int Idjobs { get; set; }

        public string? Jobname { get; set; }

        public virtual string SkillsIdskills { get; set; }
    }
}
