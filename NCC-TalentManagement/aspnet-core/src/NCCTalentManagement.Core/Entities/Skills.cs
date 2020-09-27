using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace NCCTalentManagement.Entities
{
    public partial class Skills : FullAuditedEntity<long>
    {
        public Skills()
        {
            Cvskills = new HashSet<CVSkills>();
        }

        public long GroupSkillId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual GroupSkills GroupSkill { get; set; }
        public virtual ICollection<CVSkills> Cvskills { get; set; }
    }
}
