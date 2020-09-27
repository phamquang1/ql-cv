using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace NCCTalentManagement.Entities
{
    public partial class GroupSkills : FullAuditedEntity<long>
    {
        public GroupSkills()
        {
            Cvskills = new HashSet<CVSkills>();
            Skills = new HashSet<Skills>();
        }

        public string Name { get; set; }

        public virtual ICollection<CVSkills> Cvskills { get; set; }
        public virtual ICollection<Skills> Skills { get; set; }
    }
}
