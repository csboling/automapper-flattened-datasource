using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace automapper_flattened_datasource
{
    public class ExampleContext : DbContext
    {
        public ExampleContext(SqliteConnection connection)
        {
            this.Connection = connection;
        }

        private SqliteConnection Connection { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlite(this.Connection)
                .UseLoggerFactory(new LoggerFactory().AddConsole());
                //.ConfigureWarnings(opt => opt.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Library>();
            modelBuilder.Entity<LibrarySection>();
            modelBuilder.Entity<LibraryBook>();
            modelBuilder.Entity<Book>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
