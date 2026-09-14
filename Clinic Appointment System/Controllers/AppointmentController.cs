using Microsoft.AspNetCore.Mvc;
using Clinic_Appointment_System.Config;
using Clinic_Appointment_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Appointment_System.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppDbContext _context;
        public AppointmentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search, string sortByAppointmentDate, int page = 1)
        {
            var appointments = _context.Appointments.Include(a => a.Doctor).ToList();

            int pageSize = 4;
            List<Appointment> filtered = appointments;

            if (!string.IsNullOrEmpty(search))
            {
                filtered = appointments.Where(a => a.PatientName.Contains(search)).ToList();
            }

            if (!string.IsNullOrEmpty(sortByAppointmentDate))
            {
                filtered = filtered.OrderBy(a => a.AppointmentDate).ToList();
            }

            int totalPages = (int)Math.Ceiling((double)filtered.Count / pageSize);

            filtered = filtered.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Search = search;
            ViewBag.SortByAppointmentDate = sortByAppointmentDate;

            return View(filtered);
        }

        public IActionResult Add()
        {
            ViewBag.Doctors = _context.Doctors.Where(d => d.IsAvailable == true).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Appointment appointment)
        {
            var doctor = _context.Doctors
                .FirstOrDefault(d => d.Id == appointment.DoctorId);

            if (doctor == null || doctor.IsAvailable == false)
            {
                ViewBag.ErrorMessage = "Selected doctor is not available.";
                ViewBag.Doctors = _context.Doctors
                    .Where(d => d.IsAvailable == true)
                    .ToList();

                return View(appointment);
            }

            bool alreadyBooked = _context.Appointments.Any(a =>
                a.DoctorId == appointment.DoctorId &&
                a.AppointmentDate == appointment.AppointmentDate &&
                a.AppointmentTime == appointment.AppointmentTime);

            if (alreadyBooked)
            {
                ViewBag.ErrorMessage = "This doctor is already booked for the selected date and time.";
                ViewBag.Doctors = _context.Doctors.Where(d => d.IsAvailable == true).ToList();
                return View(appointment);
            }

            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var appointment = _context.Appointments.Include(a => a.Doctor).FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewBag.Doctors = _context.Doctors.Where(d => d.IsAvailable == true).ToList();
            return View(appointment);
        }

        [HttpPost]
        public IActionResult Edit(int id, Appointment updatedAppointment)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            bool alreadyBooked = _context.Appointments.Any(a =>
                a.Id != id &&
                a.DoctorId == updatedAppointment.DoctorId &&
                a.AppointmentDate == updatedAppointment.AppointmentDate &&
                a.AppointmentTime == updatedAppointment.AppointmentTime);

            if (alreadyBooked)
            {
                ViewBag.ErrorMessage = "This doctor is already booked for the selected date and time.";
                ViewBag.Doctors = _context.Doctors.Where(d => d.IsAvailable == true).ToList();
                return View(updatedAppointment);
            }

            appointment.PatientName = updatedAppointment.PatientName;
            appointment.PatientPhone = updatedAppointment.PatientPhone;
            appointment.AppointmentDate = updatedAppointment.AppointmentDate;
            appointment.AppointmentTime = updatedAppointment.AppointmentTime;
            appointment.DoctorId = updatedAppointment.DoctorId;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}