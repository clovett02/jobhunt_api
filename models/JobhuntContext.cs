
using Microsoft.EntityFrameworkCore;

namespace JobHunt_API.models;

public partial class JobhuntContext : DbContext
{
    public JobhuntContext()
    {
    }

    public JobhuntContext(DbContextOptions<JobhuntContext> options): base(options)
    {
    }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Jobstoskill> Jobstoskills { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        // => optionsBuilder.UseMySQL("server=thor.mysql;userid=jobhuntapi;password=12191990;database=jobhunt");
        => optionsBuilder.UseMySQL("server=thor.mysql;userid=jobhuntapi;password=12191990;database=jobhunt");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("jobs");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ApplicationDate).HasColumnType("date");
            // entity.Property(e => e.ApplicationDay).HasMaxLength(10);
            entity.Property(e => e.ApplicationTime).HasColumnType("datetime");
            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.CompanyUrl)
                .HasMaxLength(255)
                .HasColumnName("CompanyURL");
            entity.Property(e => e.DatePosted).HasColumnType("date");
            entity.Property(e => e.JobDescription).HasColumnType("text");
            entity.Property(e => e.JobTitle)
                .IsRequired()
                .HasMaxLength(90);
            entity.Property(e => e.JobUrl)
                .HasMaxLength(255)
                .HasColumnName("JobURL");
            entity.Property(e => e.ResponseDate).HasColumnType("date");
            // entity.Property(e => e.ResponseDay).HasMaxLength(10);
            entity.Property(e => e.ResponseTime).HasColumnType("datetime");
            entity.Property(e => e.SiteFoundOn).HasMaxLength(45);
            entity.Property(e => e.State)
                .IsRequired()
                .HasMaxLength(45);
        });

        modelBuilder.Entity<Jobstoskill>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("jobstoskills");

            entity.HasIndex(e => e.JobId, "JobID");

            entity.HasIndex(e => e.SkillId, "SkillID");

            entity.Property(e => e.JobId).HasColumnName("JobID");
            entity.Property(e => e.SkillId).HasColumnName("SkillID");

            entity.HasOne(d => d.Job).WithMany()
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("jobstoskills_ibfk_1");

            entity.HasOne(d => d.Skill).WithMany()
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("jobstoskills_ibfk_2");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("skills");

            entity.HasIndex(e => e.Name, "Name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
