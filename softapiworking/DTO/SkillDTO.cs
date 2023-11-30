using softapiworking.mydb;

namespace softapiworking.DTO
{
    public class SkillDTO
    {
        public int Idskills { get; set; }

        public string Name { get; set; } = null!;

        public string? Desc { get; set; }

        public DateTime Creation { get; set; }

    }
}
