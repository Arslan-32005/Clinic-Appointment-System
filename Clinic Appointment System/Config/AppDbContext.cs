using Microsoft.EntityFrameworkCore;
using Clinic_Appointment_System.Models;
namespace Clinic_Appointment_System.Config;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options):base(options) { }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
}
