﻿using NCCTalentManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NCCTalentManagement.APIs.MyProfile.Dto
{
    public class EducationDto
    {
        public long? CvcandidateId { get; set; }
        public long? CvemployeeId { get; set; }
        [Required(ErrorMessage = "SchoolOrCenterName is required")]
        public string SchoolOrCenterName { get; set; }
        public DegreeTypeEnum DegreeType { get; set; }
        [Required(ErrorMessage = "Major is required")]
        public string Major { get; set; }
        [Required(ErrorMessage = "Start Year is required")]
        [StringLength(4)]
        public string StartYear { get; set; }
        [Required(ErrorMessage = "End Year is required")]
        [StringLength(4)]
        public string EndYear { get; set; }
        public string Description { get; set; }
        public int? Order { get; set; }
        public long Id { get; set; }
    }
}
