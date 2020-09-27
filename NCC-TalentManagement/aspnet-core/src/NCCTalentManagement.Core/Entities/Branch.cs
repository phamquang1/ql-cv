using Abp.Domain.Entities.Auditing;
using NCCTalentManagement.Authorization.Users;
using System;
using System.Collections.Generic;

namespace NCCTalentManagement.Entities
{
    public partial class Branch : FullAuditedEntity<long>
    {
        public Branch()
        {
            Cvcandidates = new HashSet<CVCandidates>();
            Users = new HashSet<User>();
        }

        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<CVCandidates> Cvcandidates { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
