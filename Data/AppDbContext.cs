using MedTrackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MedTrackAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
    }
}