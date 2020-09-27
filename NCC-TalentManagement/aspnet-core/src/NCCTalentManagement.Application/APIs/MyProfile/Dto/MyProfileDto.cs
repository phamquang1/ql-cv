using NCCTalentManagement.APIs.MyCV.Dto;
using NCCTalentManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCCTalentManagement.APIs.MyProfile.Dto
{
    public class MyProfileDto
    {
        public bool isHiddenYear { get; set; }
        public AttachmentTypeEnum typeOffile { get; set; }
        public UserGeneralInfoDto EmployeeInfo { get; set; }
        public IEnumerable<EducationDto> EducationBackGround { get; set; }
        public TechnicalExpertiseDto TechnicalExpertises { get; set; }
        public PersonalAttributeDto PersonalAttributes { get; set; }
        public IEnumerable<WorkingExperienceDto> WorkingExperiences { get; set; }
    }
}
