using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using NCCTalentManagement.Entities;

namespace NCCTalentManagement.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";
        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }

        public string PersonalAttribute { get; set; }
        public string AvatarPath { get; set; }
        public string Address { get; set; }
        public string EmailAddressInCV { get; set; }
        public long BranchId { get; set; }
        public long PositionId { get; set; }
        [DefaultValue(false)]
        public virtual Branch Branch { get; set; }
        public virtual PositionType Position { get; set; }
        public virtual ICollection<CVSkills> Cvskills { get; set; }
        public virtual ICollection<Educations> Educations { get; set; }
        public virtual ICollection<EmployeeWorkingExperiences> EmployeeWorkingExperiences { get; set; }
        public virtual ICollection<InterviewCandidates> InterviewCandidates { get; set; }
        public virtual ICollection<User> InverseDeleterUser { get; set; }
        public virtual ICollection<User> InverseCreatorUser { get; set; }
        public virtual ICollection<User> InverseLastModifierUser { get; set; }
        public virtual ICollection<CVCandidates> CandidateReferral { get; set; }
    }
}
