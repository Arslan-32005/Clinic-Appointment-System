using Microsoft.AspNetCore.Mvc;
using Clinic_Appointment_System.Config;
using Clinic_Appointment_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Clinic_Appointment_System.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppDbContext _context;
        public AppointmentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search,string sortbyappointments,int page=1)
        {

            var appointments = _context.Appointments.Include(a => a.Doctor).ToList();
            int pageSize = 4;
            List<Appointment> filtered = appointments;
            if (!string.IsNullOrEmpty(search))
            {
                filtered = appointments.Where(a => a.PatientName.Contains(search)).ToList();
            }
            if(!string.IsNullOrEmpty(sortbyappointments))
            {
                filtered=appointments.OrderBy(a => a.AppointmentDate).ToList();
            }
            int totalpages=(int)Math.Ceiling((double)filtered.Count / pageSize);
            filtered = filtered.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalpages;
            ViewBag.Search = search;
            ViewBag.sortbyappointments = sortbyappointments;
            return View(filtered);
        }
        public IActionResult Add()
        {
            var doctors = _context.Doctors.Where(d => d.IsAvailable == true).ToList();
            ViewBag.Doctors = doctors;
            return View();
        }
        [HttpPost]
        public IActionResult Add(Appointment appointment)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.Id == appointment.DoctorId);
            if (doctor == null || doctor.IsAvailable == false)
            {
                ViewBag.ErrorMessage = "Selected doctor is not available";
                ViewBag.Doctors = _context.Doctors.Where(d => d.IsAvailable == true).ToList();
                return View(appointment);
            }
            bool AlreadyBooked = _context.Appointments.Any(a => a.DoctorId == appointment.DoctorId && a.AppointmentDate == appointment.AppointmentDate && a.AppointmentTime == appointment.AppointmentTime);
            if (AlreadyBooked)
            {
                ViewBag.ErrorMessage = "Doctor is already booked";
                ViewBag.Doctors = _context.Doctors.Where(d => d.IsAvailable == true).ToList();
                return View(appointment);
            }
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var appoinment = _context.Appointments.Include(a => a.Doctor).FirstOrDefault(a => a.Id == id);
            if (appoinment == null)
            {
                return NotFound();
            }
            ViewBag.Doctors = _context.Doctors.Where(d => d.IsAvailable == true).ToList();
            return View(appoinment);
        }
        [HttpPost]
        public IActionResult Edit(int id, Appointment updatedappointment)
        {
            var appoinment = _context.Appointments.Find(id);
            if (appoinment == null)
            {
                return NotFound();
            }
            bool AlreadyBooked = _context.Appointments.Any(a => a.DoctorId == updatedappointment.DoctorId && a.AppointmentDate == updatedappointment.AppointmentDate && a.AppointmentTime == updatedappointment.AppointmentTime && a.Id != id);
            if (AlreadyBooked)
            {
                ViewBag.ErrorMessage = "Doctor is already booked";
                ViewBag.Doctors = _context.Doctors.Where(d => d.IsAvailable == true).ToList();
                return View(updatedappointment);
            }
            appoinment.DoctorId = updatedappointment.DoctorId;
            appoinment.PatientName = updatedappointment.PatientName;
            appoinment.PatientPhone = updatedappointment.PatientPhone;
            appoinment.AppointmentDate = updatedappointment.AppointmentDate;
            appoinment.AppointmentTime = updatedappointment.AppointmentTime;
            appoinment.Reason = updatedappointment.Reason;
            appoinment.PaymentStatus = updatedappointment.PaymentStatus;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var appoinment = _context.Appointments.Find(id);
            if (appoinment == null)
            {
                return NotFound();
            }
            _context.Appointments.Remove(appoinment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}