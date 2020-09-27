using AutoMapper;
using NCCTalentManagement.Entities;

namespace NCCTalentManagement.APIs.Candidate.Dto
{
    public class CandidateMapProfile : Profile
    {
        public CandidateMapProfile()
        {
            CreateMap<CandidateDto, CVCandidates>()
                .ForMember(e => e.Id, dto => dto.MapFrom(d => d.Id))
                .ForMember(e => e.BranchId, dto => dto.MapFrom(d => d.BranchId))
                .ForMember(e => e.Email, dto => dto.MapFrom(d => d.Email))
                .ForMember(e => e.FullName, dto => dto.MapFrom(d => d.FullName))
                .ForMember(e => e.InterviewTime, dto => dto.MapFrom(d => d.InterviewTime))
                .ForMember(e => e.OldCvid, dto => dto.MapFrom(d => d.OldCVId))
                .ForMember(e => e.Phone, dto => dto.MapFrom(d => d.Phone))
                .ForMember(e => e.PositionId, dto => dto.MapFrom(d => d.PositionId))
                .ForMember(e => e.ReceiveTime, dto => dto.MapFrom(d => d.ReceiveTime))
                .ForMember(e => e.Source, dto => dto.MapFrom(d => d.Source))
                .ForMember(e => e.StartWorkingTime, dto => dto.MapFrom(d => d.StartWorkingTime))
                .ForMember(e => e.Status, dto => dto.MapFrom(d => d.Status))
                .ForMember(e => e.AttachmentPatch, dto => dto.MapFrom(d => d.AttachmentPatch))
                .ForMember(e => e.DegreeType, dto => dto.MapFrom(d => d.DegreeType))
                .ForMember(e => e.PresenterId, dto => dto.MapFrom(d => d.PresenterId))
                .ForMember(e => e.WorkExperience, dto => dto.MapFrom(d => d.WorkExperience));
            
            CreateMap<CVCandidateSkillDto, CVSkills>()
                .ForMember(e => e.Id, dto => dto.MapFrom(d => d.Id))
                .ForMember(e => e.CvcandidateId, dto => dto.MapFrom(d => d.CVCandidateId))
                .ForMember(e => e.GroupSkillId, dto => dto.MapFrom(d => d.GroupSkillId))
                .ForMember(e => e.Level, dto => dto.MapFrom(d => d.Level))
                .ForMember(e => e.SkillId, dto => dto.MapFrom(d => d.SkillId))
                .ForMember(e => e.SkillName, dto => dto.MapFrom(d => d.SkillName));

            CreateMap<CVCandidateEducationDto, Educations>()
                .ForMember(e => e.Id, dto => dto.MapFrom(d => d.Id))
                .ForMember(e => e.DegreeType, dto => dto.MapFrom(d => d.DegreeType))
                .ForMember(e => e.EndYear, dto => dto.MapFrom(d => d.EndYear))
                .ForMember(e => e.Major, dto => dto.MapFrom(d => d.Major))
                .ForMember(e => e.Order, dto => dto.MapFrom(d => d.Order))
                .ForMember(e => e.SchoolOrCenterName, dto => dto.MapFrom(d => d.SchoolOrCenterName))
                .ForMember(e => e.StartYear, dto => dto.MapFrom(d => d.StartYear));

            CreateMap<InterviewCandidateDto, InterviewCandidates>()
                .ForMember(e => e.CvcandidateId, dto => dto.MapFrom(d => d.CvcandidateId))
                .ForMember(e => e.InterviewerId, dto => dto.MapFrom(d => d.InterviewerId))
                .ForMember(e => e.Id, dto => dto.MapFrom(d => d.Id));
        }
    }
}
