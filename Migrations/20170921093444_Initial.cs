using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AspNetCoreIHostedService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherData",
                columns: table => new
                {
                    CityName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lon = table.Column<double>(type: "float", nullable: false),
                    MainWeather = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pressure = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    WeatherDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => x.CityName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherData");
        }
    }
}
