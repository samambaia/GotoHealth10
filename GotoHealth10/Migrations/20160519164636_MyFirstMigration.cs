using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace GotoHealth10.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Age = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Height = table.Column<string>(nullable: true),
                    InitialWeigth = table.Column<string>(nullable: true),
                    Logged = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    TargetWeight = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "WeighingModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    Difference = table.Column<string>(nullable: true),
                    IMC = table.Column<string>(nullable: true),
                    UpDown = table.Column<string>(nullable: true),
                    Weight = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeighingModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("UserModel");
            migrationBuilder.DropTable("WeighingModel");
        }
    }
}
