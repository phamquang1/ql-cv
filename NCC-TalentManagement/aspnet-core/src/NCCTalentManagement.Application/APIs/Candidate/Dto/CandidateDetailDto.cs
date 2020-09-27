using NCCTalentManagement.APIs.Common.Dto;
using NCCTalentManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCCTalentManagement.APIs.Candidate.Dto
{
    public class CandidateDetailDto
    {
        public long? Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public PositionDto Position { get; set; }
        public BranchDto Branch { get; set; }
        public DateTime ReceiveTime { get; set; }
        public DateTime? InterviewTime { get; set; }
        public DateTime? StartWorkingTime { get; set; }
        public CandidateStatusEnum Status { get; set; }
        public DegreeTypeEnum? DegreeType { get; set; }
        public string AttachmentPath { get; set; }
        public long? OldCVId { get; set; }
        public string Source { get; set; }
        public string WorkExperience { get; set; }
        public DateTime CreationTime { get; set; }
        public List<string> Attachments { get; set; }
        public List<CVCandidateSkillDto> CVSkills { get; set; }
        public List<CVCandidateEducationDto> Educations { get; set; }
        public List<InterviewCandidateDto> InterviewCandidates { get; set; }
        public PresenterDto Presenter { get; set; }
    }

    public class PositionDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
    }

    public class PresenterDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
    }
}
