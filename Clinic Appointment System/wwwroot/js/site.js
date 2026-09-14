const forms = document.querySelectorAll('#AppointmentForm, #DoctorForm');

forms.forEach(form => {
    form.addEventListener('submit', function (event) {

        if (form.id === 'AppointmentForm') {
            const patientNameInput = document.getElementById('PatientName');
            if (patientNameInput.value.trim() === '') {
                alert('Patient Name is required');
                event.preventDefault();
                return;
            }

            const phoneInput = document.getElementById('PatientPhone');
            if (phoneInput.value.trim() === '' || isNaN(phoneInput.value) || Number(phoneInput.value) <= 0) {
                alert('Phone must be a positive number');
                event.preventDefault();
                return;
            }

            const dateInput = document.getElementById('AppointmentDate');
            if (dateInput.value.trim() === '') {
                alert('Appointment Date is required');
                event.preventDefault();
                return;
            }

            const timeInput = document.getElementById('AppointmentTime');
            if (timeInput.value.trim() === '') {
                alert('Appointment Time is required');
                event.preventDefault();
                return;
            }

            const doctorInput = document.getElementById('DoctorId');
            if (doctorInput.value.trim() === '') {
                alert('Doctor is required');
                event.preventDefault();
                return;
            }
        }

        if (form.id === 'DoctorForm') {
            const fullNameInput = document.getElementById('FullName');
            if (fullNameInput.value.trim() === '') {
                alert('Full Name is required');
                event.preventDefault();
                return;
            }

            const specializationInput = document.getElementById('Specialization');
            if (specializationInput.value.trim() === '') {
                alert('Specialization is required');
                event.preventDefault();
                return;
            }

            const feeInput = document.getElementById('ConsultationFee');
            if (feeInput.value.trim() === '' || isNaN(feeInput.value) || Number(feeInput.value) <= 0) {
                alert('Consultation Fee must be a positive number');
                event.preventDefault();
                return;
            }
        }
    });
});