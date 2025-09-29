using Microsoft.EntityFrameworkCore;
using Wsi.CyFun.Elephants.Core.Entities;

namespace Wsi.CyFun.Elephants.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Translation> Translations { get; set; }
    public DbSet<Assessment> Assessments { get; set; }
    public DbSet<Requirement> Requirements { get; set; }
    public DbSet<Municipality> Municipalities { get; set; }
    public DbSet<MaturityLevel> MaturityLevels { get; set; }
    public DbSet<Maturity> Maturities { get; set; }
    public DbSet<Guidance> Guidances { get; set; }
    public DbSet<Function> Functions { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Score> Scores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assessment>()
            .HasOne(a => a.ApplicationUser)
            .WithMany(a => a.AssessmentsUsers)
            .HasForeignKey(a => a.ApplicationUserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Assessment>()
            .HasOne(a => a.Assessor)
            .WithMany(a => a.AssessmentsAssessors)
            .HasForeignKey(a => a.AssessorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Assessment>()
            .HasOne(a => a.Municipality)
            .WithMany(m => m.Assessments)
            .HasForeignKey(a => a.MunicipalityId);

        modelBuilder.Entity<Assessment>()
            .HasOne(a => a.Maturity)
            .WithMany(m => m.Assessments)
            .HasForeignKey(a => a.MaturityId);

        modelBuilder.Entity<MaturityLevel>()
            .HasOne(m => m.Maturity)
            .WithMany(m => m.MaturityLevels)
            .HasForeignKey(a => a.MaturityId);

        modelBuilder.Entity<Score>()
            .HasKey(s => new { s.AssessmentId, s.RequirementId });

        modelBuilder.Entity<Score>()
            .HasOne(s => s.Assessment)
            .WithMany(a => a.Scores)
            .HasForeignKey(s => s.AssessmentId);

        modelBuilder.Entity<Score>()
            .HasOne(s => s.Requirement)
            .WithMany(a => a.Scores)
            .HasForeignKey(s => s.RequirementId);

        modelBuilder.Entity<Requirement>()
            .HasOne(r => r.SubCategory)
            .WithMany(s => s.Requirements)
            .HasForeignKey(r => r.SubCategoryId);

        modelBuilder.Entity<Guidance>()
            .HasOne(g => g.Requirement)
            .WithMany(r => r.Guidances)
            .HasForeignKey(g => g.RequirementId);

        modelBuilder.Entity<SubCategory>()
            .HasOne(s => s.Category)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(s => s.CategoryId);

        modelBuilder.Entity<Category>()
            .HasOne(c => c.Function)
            .WithMany(f => f.Categories)
            .HasForeignKey(c => c.FunctionId);

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(a => a.Username).IsRequired();
        });

        modelBuilder.Entity<Assessment>(entity =>
        {
            entity.Property(a => a.ApplicationUserId)
            .IsRequired();
            entity.Property(a => a.MunicipalityId)
            .IsRequired();
            entity.Property(a => a.AssessorId)
            .IsRequired();
            entity.Property(a => a.MaturityId)
                .IsRequired();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(c => c.Description)
            .IsRequired();
            entity.Property(c => c.Code)
            .IsRequired();
            entity.Property(c => c.Order)
            .IsRequired();
            entity.Property(c => c.FunctionId)
            .IsRequired();
            entity.Property(c => c.Name)
            .IsRequired();
        });

        modelBuilder.Entity<Function>(entity =>
        {
            //entity.Property(f => f.Description)
            //.IsRequired();
            entity.Property(f => f.Code)
            .IsRequired();
            entity.Property(f => f.Order)
            .IsRequired();
            entity.Property(f => f.Name)
            .IsRequired();
        });

        modelBuilder.Entity<Guidance>(entity =>
        {
            entity.Property(g => g.Description)
            .IsRequired();
            entity.Property(g => g.Order)
            .IsRequired();
            entity.Property(g => g.RequirementId)
            .IsRequired();
        });

        modelBuilder.Entity<Maturity>(entity =>
        {
            entity.Property(m => m.Threshold)
            .IsRequired();
            entity.Property(m => m.Description)
            .IsRequired();
        });

        modelBuilder.Entity<MaturityLevel>(entity =>
        {
            entity.Property(ml => ml.Level)
            .IsRequired();
            entity.Property(ml => ml.Value)
            .IsRequired();
            entity.Property(ml => ml.Documentation)
            .IsRequired();
            entity.Property(ml => ml.Implementation)
            .IsRequired();
            entity.Property(ml => ml.MaturityId)
            .IsRequired();
        });

        modelBuilder.Entity<Municipality>(entity =>
        {
            entity.Property(m => m.Name)
            .IsRequired();
        });

        modelBuilder.Entity<Requirement>(entity =>
        {
            entity.Property(r => r.Description)
            .IsRequired();
            entity.Property(r => r.SubCategoryId)
            .IsRequired();
            entity.Property(r => r.Code)
            .IsRequired();
            entity.Property(r => r.Order)
            .IsRequired();
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.Property(s => s.AssessmentId)
            .IsRequired();
            entity.Property(s => s.RequirementId)
            .IsRequired();
            entity.Property(s => s.DocumentationMaturityScore)
            .IsRequired();
            entity.Property(s => s.ImplementationMaturityScore)
            .IsRequired();
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.Property(sc => sc.Description)
            .IsRequired();
            entity.Property(sc => sc.CategoryId)
            .IsRequired();
            entity.Property(sc => sc.Code)
            .IsRequired();
            entity.Property(sc => sc.Order)
            .IsRequired();
            entity.Property(sc => sc.Name)
            .IsRequired();
        });

        modelBuilder.Entity<Translation>(entity =>
        {
            entity.Property(t => t.LinkedId)
            .IsRequired();
            entity.Property(t => t.Name)
            .IsRequired();
            entity.Property(t => t.Description)
            .IsRequired();
        });
        Seeder.Seed(modelBuilder);

        base.OnModelCreating(modelBuilder); // niet vergeten
    }
}
