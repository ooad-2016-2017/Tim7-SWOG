using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace SPARKMigrations
{
    public partial class InitialMigration : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    Id = table.Column(type: "INTEGER", nullable: false),
//                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column(type: "TEXT", nullable: true),
                    Name = table.Column(type: "TEXT", nullable: true),
                    Password = table.Column(type: "TEXT", nullable: true),
                    Surname = table.Column(type: "TEXT", nullable: true),
                    Username = table.Column(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.Id);
                });
            migration.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    Id = table.Column(type: "INTEGER", nullable: false),
//                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column(type: "TEXT", nullable: true),
                    Capacity = table.Column(type: "INTEGER", nullable: false),
                    CoordX = table.Column(type: "REAL", nullable: false),
                    CoordY = table.Column(type: "REAL", nullable: false),
                    MinCredits = table.Column(type: "INTEGER", nullable: false),
                    MonthlyProfit = table.Column(type: "REAL", nullable: false),
                    Name = table.Column(type: "TEXT", nullable: true),
                    NumTakenSpaces = table.Column(type: "INTEGER", nullable: false),
                    Price = table.Column(type: "REAL", nullable: false),
                    TodaysProfit = table.Column(type: "REAL", nullable: false),
                    Zone = table.Column(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.Id);
                });
            migration.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column(type: "INTEGER", nullable: false),
//                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column(type: "TEXT", nullable: true),
                    Name = table.Column(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });
            migration.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column(type: "INTEGER", nullable: false),
               //         .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column(type: "TEXT", nullable: true),
                    Name = table.Column(type: "TEXT", nullable: true),
                    Password = table.Column(type: "TEXT", nullable: true),
                    Surname = table.Column(type: "TEXT", nullable: true),
                    Username = table.Column(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("Owner");
            migration.DropTable("Parking");
            migration.DropTable("PaymentMethod");
            migration.DropTable("User");
        }
    }
}
