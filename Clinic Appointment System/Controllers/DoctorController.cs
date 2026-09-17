using Microsoft.AspNetCore.Mvc;
using Clinic_Appointment_System.Config;
using Clinic_Appointment_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Appointment_System.Controllers
{
    public class DoctorController : Controller
    {
        private readonly AppDbContext _context;
        public DoctorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var doctors = _context.Doctors.ToList();
            return View(doctors);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var doctor = _context.Doctors.Include(d => d.Appointments).FirstOrDefault(d => d.Id == id);
            if(doctor==null)
            {
                return NotFound();
            }
            return View(doctor);

        }
        [HttpPost]
        public IActionResult Edit(int id, Doctor updateddoctor)
        {
            var doctor = _context.Doctors.Find(id);
            if(doctor == null)
            {
                return NotFound();
            }
            doctor.FullName = updateddoctor.FullName;
            doctor.Specialization = updateddoctor.Specialization;
            doctor.Email = updateddoctor.Email;
            doctor.PhoneNumber = updateddoctor.PhoneNumber;
            doctor.Qualification = updateddoctor.Qualification;
            doctor.ConsultationFee = updateddoctor.ConsultationFee;
            doctor.Gender = updateddoctor.Gender;
            doctor.IsAvailable = updateddoctor.IsAvailable;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            bool hasAppointments = _context.Appointments.Any(a => a.DoctorId == id);
            if(hasAppointments)
            {

                ViewBag.ErrorMessage = "Cannot delete doctor with existing appointments.";
                return View("Index",_context.Doctors.ToList());
            }
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }





    }
}
