using Abp.Domain.Entities.Auditing;
using NCCTalentManagement.Authorization.Users;
using System;
using System.Collections.Generic;

namespace NCCTalentManagement.Entities
{
    public partial class CVSkills : FullAuditedEntity<long>
    {
        public long? CvcandidateId { get; set; }
        public long? CvemployeeId { get; set; }
        public long? SkillId { get; set; }
        public int ExperienceMonth { get; set; }
        public long? GroupSkillId { get; set; }
        public string SkillName { get; set; }
        public int? Level { get; set; }

        public virtual CVCandidates Cvcandidate { get; set; }
        public virtual User Cvemployee { get; set; }
        public virtual GroupSkills GroupSkill { get; set; }
        public virtual Skills Skill { get; set; }
    }
}
