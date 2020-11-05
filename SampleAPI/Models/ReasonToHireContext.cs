using Microsoft.EntityFrameworkCore;

namespace SampleAPI.Models
{
    public class ReasonToHireContext : DbContext
    {
        public ReasonToHireContext(DbContextOptions<ReasonToHireContext> options) : base(options)
        {
        }
        public DbSet<ReasonToHire> ReasonsToHire { get; set; }
    }
}
