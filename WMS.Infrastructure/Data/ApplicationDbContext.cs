using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
namespace WMS.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Leave> Leaves { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<EmployeeProjectAllocation> EmployeeProjectAllocations { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserLogin>()
            .HasKey(x => x.UserId);

        modelBuilder.Entity<Role>()
            .HasKey(x => x.RoleId);

        modelBuilder.Entity<Employee>()
            .HasKey(x => x.EmployeeId);

        modelBuilder.Entity<Department>()
            .HasKey(x => x.DepartmentId);

        modelBuilder.Entity<EmployeeProjectAllocation>()
            .HasKey(x => x.AllocationId);
        modelBuilder.Entity<Attendance>()
    .HasOne(a => a.Employee)
    .WithMany(e => e.Attendances)
    .HasForeignKey(a => a.EmpId);

        base.OnModelCreating(modelBuilder);
    }
}