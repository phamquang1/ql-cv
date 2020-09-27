using System;
using System.Collections.Generic;
using System.Text;

namespace NCCTalentManagement.APIs.Common.Dto
{
    public class SkillCandidateDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long GroupSkillId { get; set; }
    }
}
