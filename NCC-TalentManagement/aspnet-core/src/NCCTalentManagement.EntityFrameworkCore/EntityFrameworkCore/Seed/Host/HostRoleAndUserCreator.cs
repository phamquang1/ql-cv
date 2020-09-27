using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using NCCTalentManagement.Authorization;
using NCCTalentManagement.Authorization.Roles;
using NCCTalentManagement.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace NCCTalentManagement.EntityFrameworkCore.Seed.Host
{
    public class HostRoleAndUserCreator
    {
        private readonly NCCTalentManagementDbContext _context;

        public HostRoleAndUserCreator(NCCTalentManagementDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateHostRoleAndUsers();
        }

        private void CreateHostRoleAndUsers()
        {
            // Admin role for host

            var adminRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
            if (adminRoleForHost == null)
            {
                adminRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.Admin, StaticRoleNames.Host.Admin) { IsStatic = true, IsDefault = true }).Entity;
                _context.SaveChanges();
            }

            // Grant all permissions to admin role for host

            var grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == null && p.RoleId == adminRoleForHost.Id)
                .Select(p => p.Name)
                .ToList();

            var permissions = PermissionFinder
                .GetAllPermissions(new NCCTalentManagementAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host) &&
                            !grantedPermissions.Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = null,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = adminRoleForHost.Id
                    })
                );
                _context.SaveChanges();
            }

            // Admin user for host

            var adminUserForHost = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == null && u.UserName == AbpUserBase.AdminUserName);
            if (adminUserForHost == null)
            {
                var user = new User
                {
                    TenantId = null,
                    UserName = AbpUserBase.AdminUserName,
                    Name = "admin",
                    Surname = "admin",
                    EmailAddress = "admin@aspnetboilerplate.com",
                    IsEmailConfirmed = true,
                    IsActive = true
                };

                user.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(user, "123qwe");
                user.SetNormalizedNames();

                adminUserForHost = _context.Users.Add(user).Entity;
                _context.SaveChanges();

                // Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();

                _context.SaveChanges();
            }

            // HR role for host

            var hrRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.HR);
            if (hrRoleForHost == null)
            {
                hrRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.HR, StaticRoleNames.Host.HR) { IsStatic = true, IsDefault = true }).Entity;
                _context.SaveChanges();
            }

            // Grant all permissions to hr role for host

            grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == null && p.RoleId == hrRoleForHost.Id)
                .Select(p => p.Name)
                .ToList();

            permissions = PermissionFinder
                .GetAllPermissions(new NCCTalentManagementAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host) &&
                            !grantedPermissions.Contains(p.Name) && GrantPermissionRoles.PermissionRoles[StaticRoleNames.Host.HR].Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = null,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = hrRoleForHost.Id
                    })
                );
                _context.SaveChanges();
            }

            // PM role for host

            var pmRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.PM);
            if (pmRoleForHost == null)
            {
                pmRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.PM, StaticRoleNames.Host.PM) { IsStatic = true, IsDefault = true }).Entity;
                _context.SaveChanges();
            }

            // Grant all permissions to PM role for host

            grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == null && p.RoleId == pmRoleForHost.Id)
                .Select(p => p.Name)
                .ToList();

            permissions = PermissionFinder
                .GetAllPermissions(new NCCTalentManagementAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host) &&
                            !grantedPermissions.Contains(p.Name) && GrantPermissionRoles.PermissionRoles[StaticRoleNames.Host.PM].Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = null,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = pmRoleForHost.Id
                    })
                );
                _context.SaveChanges();
            }

            // Sales role for host

            var salesRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Sales);
            if (salesRoleForHost == null)
            {
                salesRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.Sales, StaticRoleNames.Host.Sales) { IsStatic = true, IsDefault = true }).Entity;
                _context.SaveChanges();
            }

            // Grant all permissions to sales role for host

            grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == null && p.RoleId == salesRoleForHost.Id)
                .Select(p => p.Name)
                .ToList();

            permissions = PermissionFinder
                .GetAllPermissions(new NCCTalentManagementAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host) &&
                            !grantedPermissions.Contains(p.Name) && GrantPermissionRoles.PermissionRoles[StaticRoleNames.Host.Sales].Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = null,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = salesRoleForHost.Id
                    })
                );
                _context.SaveChanges();
            }

            // Employee role for host

            var employeeRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Employee);
            if (employeeRoleForHost == null)
            {
                employeeRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.Employee, StaticRoleNames.Host.Employee) { IsStatic = true, IsDefault = true }).Entity;
                _context.SaveChanges();
            }

            // Grant all permissions to Employee role for host

            grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == null && p.RoleId == employeeRoleForHost.Id)
                .Select(p => p.Name)
                .ToList();

            permissions = PermissionFinder
                .GetAllPermissions(new NCCTalentManagementAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host) &&
                            !grantedPermissions.Contains(p.Name) && GrantPermissionRoles.PermissionRoles[StaticRoleNames.Host.Employee].Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = null,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = employeeRoleForHost.Id
                    })
                );
                _context.SaveChanges();
            }
        }
    }
}
