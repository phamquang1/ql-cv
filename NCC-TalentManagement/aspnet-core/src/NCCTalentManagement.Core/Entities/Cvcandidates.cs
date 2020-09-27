using Abp.Domain.Entities.Auditing;
using NCCTalentManagement.Authorization.Users;
using NCCTalentManagement.Constants.Enum;
using System;
using System.Collections.Generic;

namespace NCCTalentManagement.Entities
{
    public partial class CVCandidates : FullAuditedEntity<long>
    {
        public CVCandidates()
        {
            Cvattachments = new HashSet<CVAttachments>();
            Cvskills = new HashSet<CVSkills>();
            InterviewCandidates = new HashSet<InterviewCandidates>();
            InverseOldCv = new HashSet<CVCandidates>();
        }

        public string FullName { get; set; }
        public DateTime ReceiveTime { get; set; }
        public DateTime? InterviewTime { get; set; }
        public DateTime? StartWorkingTime { get; set; }
        public CandidateStatusEnum Status { get; set; }
        public string AttachmentPatch { get; set; }
        public string Email { get; set; }
        public DegreeTypeEnum? DegreeType { get; set; }
        public long? OldCvid { get; set; }
        public long BranchId { get; set; }
        public string Source { get; set; }
        public long PositionId { get; set; }
        public string Phone { get; set; }
        public string WorkExperience { get; set; }
        public long? PresenterId { get; set; }

        public virtual User Presenter { get;set;}
        public virtual Branch Branch { get; set; }
        public virtual CVCandidates OldCv { get; set; }
        public virtual PositionType Position { get; set; }
        public virtual ICollection<CVAttachments> Cvattachments { get; set; }
        public virtual ICollection<CVSkills> Cvskills { get; set; }
        public virtual ICollection<InterviewCandidates> InterviewCandidates { get; set; }
        public virtual ICollection<CVCandidates> InverseOldCv { get; set; }
    }
}
