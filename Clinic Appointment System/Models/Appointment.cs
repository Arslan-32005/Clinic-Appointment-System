namespace Clinic_Appointment_System.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string PatientName {  get; set; }
        public string PatientPhone {  get; set; }
        public DateTime AppointmentDate {  get; set; }
        public string AppointmentTime {  get; set; }
        public string Reason { get; set; }
        public string PaymentStatus { get; set; }
        public int DoctorId {  get; set; }
        public Doctor Doctor { get; set; }
    }
}
