using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic_Appointment_System.Migrations
{
    /// <inheritdoc />
    public partial class changespellingofAvailable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvaiable",
                table: "Doctors",
                newName: "IsAvailable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Doctors",
                newName: "IsAvaiable");
        }
    }
}
