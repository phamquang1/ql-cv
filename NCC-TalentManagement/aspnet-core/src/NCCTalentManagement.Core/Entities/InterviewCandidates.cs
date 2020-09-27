using Abp.Domain.Entities.Auditing;
using NCCTalentManagement.Authorization.Users;
using System;
using System.Collections.Generic;

namespace NCCTalentManagement.Entities
{
    public partial class InterviewCandidates : FullAuditedEntity<long>
    {
        public long InterviewerId { get; set; }
        public long CvcandidateId { get; set; }

        public virtual CVCandidates Cvcandidate { get; set; }
        public virtual User Interviewer { get; set; }
    }
}
