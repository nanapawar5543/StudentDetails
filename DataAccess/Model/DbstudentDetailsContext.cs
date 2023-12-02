using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Model;

public partial class DbstudentDetailsContext : DbContext
{
    public DbstudentDetailsContext()
    {
    }

    public DbstudentDetailsContext(DbContextOptions<DbstudentDetailsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblClassMaster> TblClassMasters { get; set; }

    public virtual DbSet<TblExamMaster> TblExamMasters { get; set; }

    public virtual DbSet<TblStudentDetail> TblStudentDetails { get; set; }

    public virtual DbSet<TblStudentMarkDetail> TblStudentMarkDetails { get; set; }

    public virtual DbSet<TblStudentMaster> TblStudentMasters { get; set; }

    public virtual DbSet<TblSubjectClassMapping> TblSubjectClassMappings { get; set; }

    public virtual DbSet<TblSubjectMaster> TblSubjectMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-LN3AL050;Database=DBStudentDetails;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblClassMaster>(entity =>
        {
            entity.HasKey(e => e.ClassMasterIdPk).HasName("PK_ClassMaster");

            entity.ToTable("tblClassMaster");

            entity.Property(e => e.ClassMasterIdPk).HasColumnName("ClassMasterID_pk");
            entity.Property(e => e.ClassName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblExamMaster>(entity =>
        {
            entity.HasKey(e => e.ExamMasterIdPk).HasName("PK_ExamMaster");

            entity.ToTable("tblExamMaster");

            entity.Property(e => e.ExamMasterIdPk).HasColumnName("ExamMasterID_pk");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ExamName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblStudentDetail>(entity =>
        {
            entity.HasKey(e => e.StudentDetailsIdPk).HasName("PK_StudentDetails");

            entity.ToTable("tblStudentDetails");

            entity.Property(e => e.StudentDetailsIdPk).HasColumnName("StudentDetailsID_pk");
            entity.Property(e => e.ClassMasterIdFk).HasColumnName("ClassMasterID_fk");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ExamMasterIdFk).HasColumnName("ExamMasterID_fk");
            entity.Property(e => e.SemPct)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Sem-PCT");
            entity.Property(e => e.StudentMasterIdFk).HasColumnName("StudentMasterID_fk");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.ClassMasterIdFkNavigation).WithMany(p => p.TblStudentDetails)
                .HasForeignKey(d => d.ClassMasterIdFk)
                .HasConstraintName("FK__tblStuden__Class__4316F928");

            entity.HasOne(d => d.ExamMasterIdFkNavigation).WithMany(p => p.TblStudentDetails)
                .HasForeignKey(d => d.ExamMasterIdFk)
                .HasConstraintName("FK__tblStuden__ExamM__440B1D61");

            entity.HasOne(d => d.StudentMasterIdFkNavigation).WithMany(p => p.TblStudentDetails)
                .HasForeignKey(d => d.StudentMasterIdFk)
                .HasConstraintName("FK__tblStuden__Stude__4222D4EF");
        });

        modelBuilder.Entity<TblStudentMarkDetail>(entity =>
        {
            entity.HasKey(e => e.StudentMarkDetailsIdPk).HasName("PK_StudentMarkDetails");

            entity.ToTable("tblStudentMarkDetails");

            entity.Property(e => e.StudentMarkDetailsIdPk).HasColumnName("StudentMarkDetailsID_pk");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.StudentDetailsIdFk).HasColumnName("StudentDetailsID_fk");
            entity.Property(e => e.SubjectClassMappingIdFk).HasColumnName("SubjectClassMappingID_fk");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.StudentDetailsIdFkNavigation).WithMany(p => p.TblStudentMarkDetails)
                .HasForeignKey(d => d.StudentDetailsIdFk)
                .HasConstraintName("FK__tblStuden__Stude__46E78A0C");

            entity.HasOne(d => d.SubjectClassMappingIdFkNavigation).WithMany(p => p.TblStudentMarkDetails)
                .HasForeignKey(d => d.SubjectClassMappingIdFk)
                .HasConstraintName("FK__tblStuden__Subje__47DBAE45");
        });

        modelBuilder.Entity<TblStudentMaster>(entity =>
        {
            entity.HasKey(e => e.StudentMasterIdPk).HasName("PK_StudentMaster");

            entity.ToTable("tblStudentMaster");

            entity.HasIndex(e => e.Prn, "UQ_PRN").IsUnique();

            entity.HasIndex(e => e.ParentContact1, "UQ_ParentContact1").IsUnique();

            entity.HasIndex(e => e.ParentContact2, "UQ_ParentContact2")
                .IsUnique()
                .HasFilter("([ParentContact2] IS NOT NULL)");

            entity.Property(e => e.StudentMasterIdPk).HasColumnName("StudentMasterID_pk");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ParentContact1)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ParentContact2)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Prn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PRN");
            entity.Property(e => e.StudentAddress)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.StudentEmailId)
                .HasMaxLength(50)
                .HasColumnName("StudentEmailID");
            entity.Property(e => e.StudentName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblSubjectClassMapping>(entity =>
        {
            entity.HasKey(e => e.SubjectClassMappingIdPk).HasName("PK_SubjectClassMapping");

            entity.ToTable("tblSubjectClassMapping");

            entity.Property(e => e.SubjectClassMappingIdPk).HasColumnName("SubjectClassMappingID_pk");
            entity.Property(e => e.ClassMasterIdFk).HasColumnName("ClassMasterID_fk");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.SubjectMasterIdFk).HasColumnName("SubjectMasterID_fk");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.ClassMasterIdFkNavigation).WithMany(p => p.TblSubjectClassMappings)
                .HasForeignKey(d => d.ClassMasterIdFk)
                .HasConstraintName("FK__tblSubjec__Class__31EC6D26");

            entity.HasOne(d => d.SubjectMasterIdFkNavigation).WithMany(p => p.TblSubjectClassMappings)
                .HasForeignKey(d => d.SubjectMasterIdFk)
                .HasConstraintName("FK__tblSubjec__Subje__30F848ED");
        });

        modelBuilder.Entity<TblSubjectMaster>(entity =>
        {
            entity.HasKey(e => e.SubjectMasterIdPk).HasName("PK_SubjectMaster");

            entity.ToTable("tblSubjectMaster");

            entity.Property(e => e.SubjectMasterIdPk).HasColumnName("SubjectMasterID_pk");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
