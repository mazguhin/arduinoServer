using Microsoft.EntityFrameworkCore;

namespace ArduinoServer.Models
{
    public class TestsContext : DbContext
    {
        public DbSet<Temp> Temps { get; set; }

        public TestsContext(DbContextOptions<TestsContext> options)
            : base(options)
        { }

        
    }
}