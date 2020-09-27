using System;
using System.Collections.Generic;
using System.Text;

namespace NCCTalentManagement.APIs.MyProfile.Dto
{
    public class SkillDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long GroupSkillId { get; set; }
    }
}
