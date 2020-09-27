using Abp.Domain.Entities.Auditing;
using NCCTalentManagement.Constants.Enum;
using System;
using System.Collections.Generic;

namespace NCCTalentManagement.Entities
{
    public partial class CVAttachments : FullAuditedEntity<long>
    {
        public string Path { get; set; }
        public AttachmentTypeEnum Type { get; set; }
        public long CvcandidateId { get; set; }

        public virtual CVCandidates Cvcandidate { get; set; }
    }
}
