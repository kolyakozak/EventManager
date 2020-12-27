using Microsoft.EntityFrameworkCore;
using EventManager.Models;

namespace EventManager.Data
{
    public class MvcEventContext : DbContext
    {
        public MvcEventContext(DbContextOptions<MvcEventContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Event { get; set; }
    }
}