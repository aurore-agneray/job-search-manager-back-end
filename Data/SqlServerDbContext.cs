using JobSearchManagerBack.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobSearchManagerBack.Data;

internal class SqlServerDbContext : DbContext
{
    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
        : base(options) { }

    public DbSet<JobApplication> JobApplications { get; set; }

    public DbSet<Status> Statuses { get; set; }
}
