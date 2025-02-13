using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PTr.Models;

namespace PTr.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PTr.Models.WorkSession> WorkSession { get; set; } = default!;
        public DbSet<PTr.Models.TaskType> TaskType { get; set; } = default!;
        public DbSet<PTr.Models.Task> Task { get; set; } = default!;
        public DbSet<PTr.Models.WorkStat> WorkStat { get; set; } = default!;
    }
}
