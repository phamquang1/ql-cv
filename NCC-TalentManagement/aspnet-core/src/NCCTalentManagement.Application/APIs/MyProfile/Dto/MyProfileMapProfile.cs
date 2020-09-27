﻿using AutoMapper;
using NCCTalentManagement.Entities;

namespace NCCTalentManagement.APIs.MyProfile.Dto
{
    public class MyProfileMapProfile : Profile
    {
        public MyProfileMapProfile()
        {
            CreateMap<WorkingExperienceDto, EmployeeWorkingExperiences>()
                .ForMember(we => we.Id, dto => dto.MapFrom(d => d.Id))
                .ForMember(we => we.Position, dto => dto.MapFrom(d => d.Position))
                .ForMember(we => we.ProjectDescription, dto => dto.MapFrom(d => d.ProjectDescription))
                .ForMember(we => we.ProjectName, dto => dto.MapFrom(d => d.ProjectName))
                .ForMember(we => we.Responsibilities, dto => dto.MapFrom(d => d.Responsibility))
                .ForMember(we => we.StartTime, dto => dto.MapFrom(d => d.StartTime))
                .ForMember(we => we.EndTime, dto => dto.MapFrom(d => d.EndTime))
                .ForMember(we => we.UserId, dto => dto.MapFrom(d => d.UserId))
                .ForMember(we => we.Technologies, dto => dto.MapFrom(d => d.Technologies));
        }
    }
}
