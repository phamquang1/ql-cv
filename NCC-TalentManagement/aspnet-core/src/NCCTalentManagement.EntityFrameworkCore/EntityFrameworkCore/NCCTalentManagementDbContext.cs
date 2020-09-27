using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using NCCTalentManagement.Authorization.Roles;
using NCCTalentManagement.Authorization.Users;
using NCCTalentManagement.MultiTenancy;
using NCCTalentManagement.Entities;

namespace NCCTalentManagement.EntityFrameworkCore
{
    public class NCCTalentManagementDbContext : AbpZeroDbContext<Tenant, Role, User, NCCTalentManagementDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public NCCTalentManagementDbContext(DbContextOptions<NCCTalentManagementDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<CVAttachments> Cvattachments { get; set; }
        public virtual DbSet<CVCandidates> Cvcandidates { get; set; }
        public virtual DbSet<CVSkills> Cvskills { get; set; }
        public virtual DbSet<Educations> Educations { get; set; }
        public virtual DbSet<EmployeeWorkingExperiences> EmployeeWorkingExperiences { get; set; }
        public virtual DbSet<GroupSkills> GroupSkills { get; set; }
        public virtual DbSet<InterviewCandidates> InterviewCandidates { get; set; }
        public virtual DbSet<PositionType> PositionType { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("AbpUsers");
                entity.HasIndex(e => e.DeleterUserId);
                entity.HasIndex(e => e.CreatorUserId);
                entity.HasIndex(e => e.LastModifierUserId);
                entity.HasOne(d => d.DeleterUser)
                    .WithMany(p => p.InverseDeleterUser)
                    .HasForeignKey(d => d.DeleterUserId);
                entity.HasOne(d => d.CreatorUser)
                    .WithMany(p => p.InverseCreatorUser)
                    .HasForeignKey(d => d.CreatorUserId);
                entity.HasOne(d => d.LastModifierUser)
                    .WithMany(p => p.InverseLastModifierUser)
                    .HasForeignKey(d => d.LastModifierUserId);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<CVAttachments>(entity =>
            {
                entity.ToTable("CVAttachments");

                entity.HasIndex(e => e.CvcandidateId);

                entity.Property(e => e.CvcandidateId).HasColumnName("CVCandidateId");

                entity.Property(e => e.Path).IsRequired();

                entity.HasOne(d => d.Cvcandidate)
                    .WithMany(p => p.Cvattachments)
                    .HasForeignKey(d => d.CvcandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CVAttachments_CVCandidates");
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<CVCandidates>(entity =>
            {
                entity.ToTable("CVCandidates");

                entity.HasIndex(e => e.BranchId);

                entity.HasIndex(e => e.OldCvid);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.InterviewTime).HasColumnType("datetime");

                entity.Property(e => e.OldCvid).HasColumnName("OldCVId");

                entity.Property(e => e.ReceiveTime).HasColumnType("datetime");

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartWorkingTime).HasColumnType("datetime");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Cvcandidates)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CVCandidates_Branch");

                entity.HasOne(d => d.OldCv)
                    .WithMany(p => p.InverseOldCv)
                    .HasForeignKey(d => d.OldCvid)
                    .HasConstraintName("FK_CVCandidates_CVCandidates");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Cvcandidates)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CVCandidates_PositionType");
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<CVSkills>(entity =>
            {
                entity.ToTable("CVSkills");

                entity.HasIndex(e => e.CvcandidateId);

                entity.HasIndex(e => e.CvemployeeId);

                entity.HasIndex(e => e.GroupSkillId);

                entity.HasIndex(e => e.SkillId);

                entity.Property(e => e.CvcandidateId).HasColumnName("CVCandidateId");

                entity.Property(e => e.CvemployeeId).HasColumnName("CVEmployeeId");

                entity.Property(e => e.SkillName).HasMaxLength(50);

                entity.HasOne(d => d.Cvcandidate)
                    .WithMany(p => p.Cvskills)
                    .HasForeignKey(d => d.CvcandidateId)
                    .HasConstraintName("FK_Skills_CVCandidates");

                entity.HasOne(d => d.Cvemployee)
                    .WithMany(p => p.Cvskills)
                    .HasForeignKey(d => d.CvemployeeId)
                    .HasConstraintName("FK_Skills_AbpUsers");

                entity.HasOne(d => d.GroupSkill)
                    .WithMany(p => p.Cvskills)
                    .HasForeignKey(d => d.GroupSkillId)
                    .HasConstraintName("FK_CVSkills_GroupSkills");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.Cvskills)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK_CVSkills_Skills");
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<Educations>(entity =>
            {
                entity.HasIndex(e => e.CvemployeeId);

                entity.Property(e => e.CvemployeeId).HasColumnName("CVEmployeeId");

                entity.Property(e => e.Major)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SchoolOrCenterName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Cvemployee)
                    .WithMany(p => p.Educations)
                    .HasForeignKey(d => d.CvemployeeId)
                    .HasConstraintName("FK_Educations_AbpUsers");
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<EmployeeWorkingExperiences>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.ProjectName).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EmployeeWorkingExperiences)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_EmployeeWorkingExperiences_AbpUsers");
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<GroupSkills>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<InterviewCandidates>(entity =>
            {
                entity.HasIndex(e => e.CvcandidateId);

                entity.HasIndex(e => e.InterviewerId);

                entity.Property(e => e.CvcandidateId).HasColumnName("CVCandidateId");

                entity.HasOne(d => d.Cvcandidate)
                    .WithMany(p => p.InterviewCandidates)
                    .HasForeignKey(d => d.CvcandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InterviewCandidates_CVCandidates");

                entity.HasOne(d => d.Interviewer)
                    .WithMany(p => p.InterviewCandidates)
                    .HasForeignKey(d => d.InterviewerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InterviewCandidates_AbpUsers");
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<PositionType>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(50);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<Skills>(entity =>
            {
                entity.HasIndex(e => e.GroupSkillId);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.GroupSkill)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.GroupSkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skills_GroupSkills");
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

        }
    }
}
