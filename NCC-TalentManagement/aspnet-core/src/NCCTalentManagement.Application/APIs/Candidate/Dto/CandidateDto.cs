using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCCTalentManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NCCTalentManagement.APIs.Candidate.Dto
{
    public class CandidateDto
    {
        public long? Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        public long PositionId { get; set; }
        [Required]
        public long BranchId { get; set; }
        [Required]
        public DateTime ReceiveTime { get; set; }
        public DateTime? InterviewTime { get; set; }
        public DateTime? StartWorkingTime { get; set; }
        public CandidateStatusEnum Status { get; set; }
        public string AttachmentPatch { get; set; }
        public DegreeTypeEnum? DegreeType { get; set; }
        public long? OldCVId { get; set; }
        public string Source { get; set; }
        public List<string> Attachments { get; set; }
        public List<CVCandidateSkillDto> CVSkills { get; set; }
        public List<InterviewCandidateDto> InterviewCandidates { get; set; }
        public long? PresenterId { get; set; }
        public string WorkExperience { get; set; }
    }

    public class InterviewCandidateDto
    {
        public long? Id { get; set; }
        public long InterviewerId { get; set; }
        public long CvcandidateId { get; set; }
        public string Name { get; set; }
    }

    public class CVCandidateEducationDto
    {
        public long Id { get; set; }
        public string SchoolOrCenterName { get; set; }
        public DegreeTypeEnum DegreeType { get; set; }
        public string Major { get; set; }
        [StringLength(4)]
        public string StartYear { get; set; }
        [StringLength(4)]
        public string EndYear { get; set; }
        public long? CVCandidateId { get; set; }
        public int? Order { get; set; }
    }

    public class CVCandidateSkillDto
    {
        public long? Id { get; set; }
        public long? SkillId { get; set; }
        public long? GroupSkillId { get; set; }
        public string SkillName { get; set; }
        public long? CVCandidateId { get; set; }
        public int? Level { get; set; }
    }

    public class CVAttachmentOutputDto
    {
        public long? Id { get; set; }
        public long CVCandidateId { get; set; }
        public string FilePath { get; set; }
    }
}
