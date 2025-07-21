using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _MedTrackAPI_.Migrations
{
    /// <inheritdoc />
    public partial class MedicosDB_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Medicos",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Medicos",
                newName: "id");
        }
    }
}
