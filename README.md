# Clinic Appointment System

A web-based CRUD application built in ASP.NET Core MVC (.NET 10), using Entity Framework Core and a local SQL Server database to manage doctors and clinic appointments. Built to practice core MVC concepts (Controllers, Models, Razor Views, routing, LINQ, CRUD operations, database handling, and client-side validation) in C#.

## Features

* **Doctor Management** — add, view, edit, and delete doctors, each with personal and professional details (name, specialization, email, phone number, qualification, consultation fee, gender, availability)
* **Appointment Management** — add, view, edit, and delete appointments, each with patient details, appointment date, appointment time, reason, payment status, and assigned doctor
* **Doctor Availability** — only available doctors appear in the Add Appointment dropdown
* **Appointment Conflict Prevention** — prevents booking the same doctor at the exact same date and time
* **Appointment Edit Conflict Checking** — prevents editing an appointment into another appointment's occupied doctor/date/time slot while ignoring the appointment itself
* **Search Patient** — filters appointments by patient name using a partial substring match
* **View Appointments (Paginated)** — displays appointments in a table, 4 per page, with Next / Previous navigation
* **Sort By Appointment Date** — sorts appointments by date, with sort state preserved across pagination
* **Doctor Delete Protection** — prevents deleting a doctor who already has appointments
* **Delete Confirmation** — confirmation popup before deleting a doctor or appointment
* **Client-Side Validation** — JavaScript validation in `site.js` checking required fields and numeric values on Add/Edit forms
* **Custom Styling** — tables, forms, pagination, and action links styled via `site.css`

## Tech Stack

* C# (.NET 10)
* ASP.NET Core MVC
* Entity Framework Core 10
* SQL Server
* Razor Views (`.cshtml`)
* HTML / CSS / JavaScript (vanilla)

## Architecture / MVC Concepts Used

* **Model** — `Doctor` and `Appointment` are classes defining the shape of the application data (`FullName`, `Specialization`, `ConsultationFee`, `IsAvailable`, `PatientName`, `AppointmentDate`, `AppointmentTime`, etc.)
* **Controller** — `DoctorController` and `AppointmentController` hold CRUD logic, appointment validation, search, sorting, pagination, and database operations
* **View** — Razor views (`Index.cshtml`, `Add.cshtml`, `Edit.cshtml`) render database records and HTML forms, using `@model` and Razor syntax
* **Routing** — the default route `{controller}/{action}/{id?}` maps URLs like `/Doctor/Edit/3` and `/Appointment/Edit/3` to the correct action
* **Entity Framework Core** — `AppDbContext` provides `DbSet<Doctor>` and `DbSet<Appointment>` for database access and CRUD operations
* **SQL Server** — application data is stored in a local SQL Server database named `ClinicDB`
* **LINQ** — used for filtering available doctors, checking appointment conflicts, searching patient names, sorting appointment dates, and pagination using `Skip()` and `Take()`
* **ViewBag** — used to pass small values to views alongside the main model, including available doctors, search state, sort state, current page, total pages, and error messages

## Project Structure


Clinic Appointment System/
├── Controllers/
│   ├── AppointmentController.cs   — appointment CRUD + availability check + conflict check + search + sort + pagination
│   ├── DoctorController.cs        — doctor CRUD + delete protection
│   └── HomeController.cs          — home page actions
│
├── Models/
│   ├── Appointment.cs              — patient and appointment information
│   ├── Doctor.cs                   — doctor information and availability
│   └── ErrorViewModel.cs            — error view model
│
├── Config/
│   └── AppDbContext.cs             — Entity Framework Core database context
│
├── Views/
│   ├── Appointment/
│   │   ├── Index.cshtml            — appointment list + search + sort + pagination
│   │   ├── Add.cshtml              — add appointment form
│   │   └── Edit.cshtml             — edit appointment form
│   ├── Doctor/
│   │   ├── Index.cshtml             — doctor list
│   │   ├── Add.cshtml                — add doctor form
│   │   └── Edit.cshtml               — edit doctor form
│   ├── Home/
│   │   ├── Index.cshtml              — home page
│   │   └── Privacy.cshtml            — privacy page
│   └── Shared/
│       ├── _Layout.cshtml            — master layout
│       ├── _ValidationScriptsPartial.cshtml
│       └── Error.cshtml              — error page
│
├── wwwroot/
│   ├── css/site.css                  — custom styling
│   └── js/site.js                    — client-side form validation
│
├── appsettings.json                  — SQL Server connection string
├── Program.cs                        — application startup / database configuration
└── Clinic Appointment System.csproj  — project configuration and EF Core packages


## How to Run

1. Clone the repository
2. Open the `.slnx` file in Visual Studio
3. Make sure SQL Server is running and the connection string in `appsettings.json` matches your local SQL Server setup
4. Create/apply the Entity Framework Core database migration
5. Run with `F5` / the green Run button, or `dotnet run`
6. Navigate to `/Doctor` or `/Appointment` in the browser to reach the respective sections

## Sample Usage


/Doctor
1 - View all doctors
2 - Add doctor
3 - Edit doctor
4 - Delete doctor

/Appointment
1 - View all appointments
2 - Search patient by name
3 - Sort appointments by date
4 - Next / Previous page
5 - Add appointment
6 - Edit appointment
7 - Delete appointment


## What I Learned

This project ties together Models, Controllers, Razor Views, Entity Framework Core, and SQL Server into a working two-entity MVC application, along with practical patterns like CRUD operations with EF Core, doctor availability filtering, appointment conflict detection using `Any`, patient search using `Contains`, sorting with `OrderBy`, pagination using `Skip` and `Take`, preserving search/sort state across pagination links, and connecting hand-written HTML/CSS/JavaScript to database-backed Razor views.

## Notes

The project uses a local SQL Server database through Entity Framework Core. Appointment booking includes doctor availability and duplicate date/time conflict checks, while doctor deletion is prevented when existing appointments are present. Client-side validation is implemented in `site.js` for the Add/Edit forms.
