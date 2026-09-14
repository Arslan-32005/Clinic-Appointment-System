namespace Clinic_Appointment_System.Models
{
    public class Doctor
    {
        public int Id {  get; set; }
        public string FullName { get; set; }
        public string Specialization {  get; set; }
        public double ConsultationFee {  get; set; }
        public bool IsAvailable { get; set; }
        public List<Appointment>Appointments { get; set; }
    }
}
