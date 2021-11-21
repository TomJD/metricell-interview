using InterviewTest.Model;
using Microsoft.EntityFrameworkCore;

namespace InterviewTest
{
    public class InterviewTestDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }


        public InterviewTestDbContext()
        {
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=SqliteDB.db");
    }
}
