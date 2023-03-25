using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPAddressManagement.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID_Customer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Company = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID_Customer);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    ID_Group = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.ID_Group);
                });

            migrationBuilder.CreateTable(
                name: "IPAddress",
                columns: table => new
                {
                    ID_IPAddress = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IPAddressName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPAddress", x => x.ID_IPAddress);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PassWord = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ID_Group = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID_User);
                    table.ForeignKey(
                        name: "FK_User_Group_ID_Group",
                        column: x => x.ID_Group,
                        principalTable: "Group",
                        principalColumn: "ID_Group");
                });

            migrationBuilder.CreateTable(
                name: "RentalContract ",
                columns: table => new
                {
                    ID_RentalContract = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_IPAddress = table.Column<int>(type: "int", nullable: true),
                    ID_Customer = table.Column<int>(type: "int", nullable: true),
                    ID_User = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalContract ", x => x.ID_RentalContract);
                    table.ForeignKey(
                        name: "FK_RentalContract _Customer_ID_Customer",
                        column: x => x.ID_Customer,
                        principalTable: "Customer",
                        principalColumn: "ID_Customer");
                    table.ForeignKey(
                        name: "FK_RentalContract _IPAddress_ID_IPAddress",
                        column: x => x.ID_IPAddress,
                        principalTable: "IPAddress",
                        principalColumn: "ID_IPAddress");
                    table.ForeignKey(
                        name: "FK_RentalContract _User_ID_User",
                        column: x => x.ID_User,
                        principalTable: "User",
                        principalColumn: "ID_User");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_PhoneNumber",
                table: "Customer",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_Name",
                table: "Group",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IPAddress_IPAddressName",
                table: "IPAddress",
                column: "IPAddressName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalContract _ID_Customer",
                table: "RentalContract ",
                column: "ID_Customer");

            migrationBuilder.CreateIndex(
                name: "IX_RentalContract _ID_IPAddress",
                table: "RentalContract ",
                column: "ID_IPAddress");

            migrationBuilder.CreateIndex(
                name: "IX_RentalContract _ID_User",
                table: "RentalContract ",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ID_Group",
                table: "User",
                column: "ID_Group");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalContract ");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "IPAddress");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Group");
        }
    }
}
