using Medsoft.Models;
using Microsoft.EntityFrameworkCore;

namespace Medsoft.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Sex> Sex { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<PatientType> PatientType { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<PatientState> PatientState { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<WorkSchedule> WorkSchedule { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }
}
