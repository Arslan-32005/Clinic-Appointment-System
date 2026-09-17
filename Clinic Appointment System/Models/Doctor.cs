namespace Clinic_Appointment_System.Models
{
    public class Doctor
    {
        public int Id {  get; set; }
        public string FullName { get; set; }
        public string Specialization {  get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Qualification { get; set; }
        public double ConsultationFee {  get; set; }
        public string Gender { get; set; }
        public bool IsAvailable { get; set; }
        public List<Appointment>Appointments { get; set; }
    }
}
