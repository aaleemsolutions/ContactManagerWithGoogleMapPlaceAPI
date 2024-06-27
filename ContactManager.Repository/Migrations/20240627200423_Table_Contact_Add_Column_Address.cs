using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManager.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Table_Contact_Add_Column_Address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Address",
                value: "Test");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Contacts");

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CreatedBy", "Email", "FirstName", "LastName", "ModifiedDateTime", "Status" },
                values: new object[] { 2, "", "jane.smith@example.com", "Jane", "Smith", null, false });
        }
    }
}
