using Abp.Authorization.Users;
using Abp.AutoMapper;
using NCCTalentManagement.APIs.MyProfile.Dto;
using NCCTalentManagement.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NCCTalentManagement.APIs.MyCV.Dto
{ 
    public class UserGeneralInfoDto
    {
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        //[Required]
        public string PhoneNumber { get; set; }

        public string EmailAddressInCV { get; set; }
        public string ImgPath { get; set; }

        [Required]
        public string CurrentPosition { get; set; }
        public long UserId { get; set; }

       // [Required]
        public string Address { get; set; }

        [Required]
        public string Branch { get; set; }
    }
}
