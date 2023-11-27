using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace softapiworking.mydb;

public partial class MydbContext : DbContext
{
    public MydbContext()
    {
    }

    public MydbContext(DbContextOptions<MydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=Fasolada132@@;database=mydb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Idemployee).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.HasIndex(e => e.Idemployee, "idemployee_UNIQUE").IsUnique();

            entity.Property(e => e.Idemployee).HasColumnName("idemployee");
            entity.Property(e => e.Hired)
                .HasColumnType("date")
                .HasColumnName("hired");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .HasColumnName("surname");

            entity.HasMany(d => d.SkillsIdskills).WithMany(p => p.EmployeeIdemployees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeHasSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillsIdskills")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_employee_has_skills_skills1"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeIdemployee")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_employee_has_skills_employee1"),
                    j =>
                    {
                        j.HasKey("EmployeeIdemployee", "SkillsIdskills").HasName("PRIMARY");
                        j.ToTable("employee_has_skills");
                        j.HasIndex(new[] { "EmployeeIdemployee" }, "fk_employee_has_skills_employee1_idx");
                        j.HasIndex(new[] { "SkillsIdskills" }, "fk_employee_has_skills_skills1_idx");
                        j.IndexerProperty<int>("EmployeeIdemployee").HasColumnName("employee_idemployee");
                        j.IndexerProperty<int>("SkillsIdskills").HasColumnName("skills_idskills");
                    });
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Idjobs).HasName("PRIMARY");

            entity.ToTable("jobs");

            entity.HasIndex(e => e.Idjobs, "idjobs_UNIQUE").IsUnique();

            entity.Property(e => e.Idjobs).HasColumnName("idjobs");
            entity.Property(e => e.Jobname)
                .HasMaxLength(45)
                .HasColumnName("jobname");

            entity.HasMany(d => d.SkillsIdskills).WithMany(p => p.JobsIdjobs)
                .UsingEntity<Dictionary<string, object>>(
                    "JobsHasSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillsIdskills")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_jobs_has_skills_skills1"),
                    l => l.HasOne<Job>().WithMany()
                        .HasForeignKey("JobsIdjobs")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_jobs_has_skills_jobs"),
                    j =>
                    {
                        j.HasKey("JobsIdjobs", "SkillsIdskills").HasName("PRIMARY");
                        j.ToTable("jobs_has_skills");
                        j.HasIndex(new[] { "JobsIdjobs" }, "fk_jobs_has_skills_jobs_idx");
                        j.HasIndex(new[] { "SkillsIdskills" }, "fk_jobs_has_skills_skills1_idx");
                        j.IndexerProperty<int>("JobsIdjobs").HasColumnName("jobs_idjobs");
                        j.IndexerProperty<int>("SkillsIdskills").HasColumnName("skills_idskills");
                    });
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Idskills).HasName("PRIMARY");

            entity.ToTable("skills");

            entity.HasIndex(e => e.Idskills, "idskills_UNIQUE").IsUnique();

            entity.Property(e => e.Idskills).HasColumnName("idskills");
            entity.Property(e => e.Creation)
                .HasColumnType("date")
                .HasColumnName("creation");
            entity.Property(e => e.Desc)
                .HasMaxLength(45)
                .HasColumnName("desc");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
