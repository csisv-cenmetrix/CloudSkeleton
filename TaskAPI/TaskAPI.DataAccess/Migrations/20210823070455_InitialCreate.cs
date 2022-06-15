using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskAPI.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AddressNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JobRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Due = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todos_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "AddressNo", "City", "FullName", "JobRole", "Street" },
                values: new object[,]
                {
                    { 1, "45", "Belgium", "Trevor	Duncan", "Developer", "Street 1" },
                    { 2, "35", "Brazil", "Carol	McLean", "Systems Engineer", "Street 2" },
                    { 3, "25", "Netherlands", "Alexander	Pullman", "Developer", "Street 3" },
                    { 4, "15", "Ukraine", "Adrian	Parr", "QA", "Street 4" }
                });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "AuthorId", "Created", "Description", "Due", "Status", "Title" },
                values: new object[] { 1, 1, new DateTime(2021, 8, 23, 12, 34, 55, 225, DateTimeKind.Local).AddTicks(630), "Get some text books for school", new DateTime(2021, 8, 28, 12, 34, 55, 226, DateTimeKind.Local).AddTicks(3719), 0, "Get books for school - DB" });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "AuthorId", "Created", "Description", "Due", "Status", "Title" },
                values: new object[] { 2, 1, new DateTime(2021, 8, 23, 12, 34, 55, 226, DateTimeKind.Local).AddTicks(4578), "Go to supermarket and by some stuff", new DateTime(2021, 8, 28, 12, 34, 55, 226, DateTimeKind.Local).AddTicks(4583), 0, "Need some grocceries" });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "AuthorId", "Created", "Description", "Due", "Status", "Title" },
                values: new object[] { 3, 2, new DateTime(2021, 8, 23, 12, 34, 55, 226, DateTimeKind.Local).AddTicks(4589), "Buy new camera", new DateTime(2021, 8, 28, 12, 34, 55, 226, DateTimeKind.Local).AddTicks(4590), 0, "Purchase Camera" });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_AuthorId",
                table: "Todos",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
