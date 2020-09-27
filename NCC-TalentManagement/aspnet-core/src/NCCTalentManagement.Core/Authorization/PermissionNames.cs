using Abp.MultiTenancy;
using System.Collections.Generic;
using static NCCTalentManagement.Authorization.Roles.StaticRoleNames;

namespace NCCTalentManagement.Authorization
{
    public static class PermissionNames
    {
        public const string Pages_Tenants = "Pages.Tenants";

        public const string Pages_Users = "Pages.Users";

        public const string Pages_Roles = "Pages.Roles";

        // Permission for MyProFile
        public const string Pages_MyProfile = "Page.MyProfile"; //Create,Delete,Edit,Export Profile

        // Permission for Manage CVCandidate
        public const string Pages_CVCandidate = "Pages.CVCandidate"; //View,Create,Edit,Delete Candidate

        // Permission for Manage CVEmployee
        public const string Pages_View_CVEmployee = "Pages.CVEmployee"; //ViewList,ViewDetail,Export


        public const string Pages_EditAsSales_Employee = "Pages.EditAsSales.Employee";// Edit Fake For Sales

        public const string Pages_Delete_CVEmployee = "Pages.Delete.CVEmployee";// Delete CV Employee
    }

    public class SystemPermission
    {
        public string Permission { get; set; }
        public MultiTenancySides MultiTenancySides { get; set; }
        public string DisplayName { get; set; }
        public bool IsConfiguration { get; set; }

        public static List<SystemPermission> ListPermissions = new List<SystemPermission>()
        {
            // for default
            new SystemPermission{ Permission =  PermissionNames.Pages_Tenants, MultiTenancySides = MultiTenancySides.Host, DisplayName = "Tenants" },
            new SystemPermission{ Permission =  PermissionNames.Pages_Users, MultiTenancySides = MultiTenancySides.Host , DisplayName = "Users" },
            new SystemPermission{ Permission =  PermissionNames.Pages_Roles, MultiTenancySides = MultiTenancySides.Host , DisplayName = "Roles" },
            new SystemPermission{ Permission =  PermissionNames.Pages_MyProfile, MultiTenancySides = MultiTenancySides.Host , DisplayName = "MyProfile" },
            new SystemPermission{ Permission =  PermissionNames.Pages_CVCandidate, MultiTenancySides = MultiTenancySides.Host , DisplayName = "CVCandidate" },
            new SystemPermission{ Permission =  PermissionNames.Pages_View_CVEmployee, MultiTenancySides = MultiTenancySides.Host , DisplayName = "CVEmployee" },
            new SystemPermission{ Permission =  PermissionNames.Pages_EditAsSales_Employee, MultiTenancySides = MultiTenancySides.Host , DisplayName = "EditAsSales" },
            new SystemPermission{ Permission =  PermissionNames.Pages_Delete_CVEmployee, MultiTenancySides = MultiTenancySides.Host , DisplayName = "DeleteCVEmployee" },
        };

    }
    public class GrantPermissionRoles
    {
        public static Dictionary<string, List<string>> PermissionRoles = new Dictionary<string, List<string>>()
        {
            {
                Host.Admin,
                new List<string>()
                {
                    PermissionNames.Pages_Tenants,
                    PermissionNames.Pages_Users,
                    PermissionNames.Pages_Roles,
                    PermissionNames.Pages_MyProfile,
                    PermissionNames.Pages_CVCandidate,
                    PermissionNames.Pages_View_CVEmployee,
                    PermissionNames.Pages_EditAsSales_Employee,
                    PermissionNames.Pages_Delete_CVEmployee,
                }
            },

            {
                Host.HR,
                new List<string>()
                {
                    PermissionNames.Pages_MyProfile,
                    PermissionNames.Pages_CVCandidate,
                    PermissionNames.Pages_View_CVEmployee
                }
            },
            {
                Host.PM,
                new List<string>()
                {
                    PermissionNames.Pages_MyProfile,
                    PermissionNames.Pages_View_CVEmployee
                }
            },
            {
                Host.Sales,
                new List<string>()
                {
                    PermissionNames.Pages_MyProfile,
                    PermissionNames.Pages_View_CVEmployee,
                    PermissionNames.Pages_EditAsSales_Employee
                }
            },
            {
                Host.Employee,
                new List<string>()
                {
                    PermissionNames.Pages_MyProfile
                }
            }

        };
    }
}
