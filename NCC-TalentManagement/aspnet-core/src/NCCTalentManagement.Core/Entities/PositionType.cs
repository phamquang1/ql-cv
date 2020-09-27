using Abp.Domain.Entities.Auditing;
using NCCTalentManagement.Authorization.Users;
using System;
using System.Collections.Generic;

namespace NCCTalentManagement.Entities
{
    public partial class PositionType : FullAuditedEntity<long>
    {
        public PositionType()
        {
            AbpUsers = new HashSet<User>();
            Cvcandidates = new HashSet<CVCandidates>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> AbpUsers { get; set; }
        public virtual ICollection<CVCandidates> Cvcandidates { get; set; }
    }
}
