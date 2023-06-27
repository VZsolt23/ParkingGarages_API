using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingGarages_API.Migrations
{
    public partial class DefaultDataInsertion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Color", "Plate", "Type" },
                values: new object[,]
                {
                    { 1, "Piros", "LOL-404", "Mazda CX-30" },
                    { 2, "Fehér", "BAD-400", "Toyota Corolla" },
                    { 3, "Szürke", "OKE-200", "Mercedes-Benz CLA 250" }
                });

            migrationBuilder.InsertData(
                table: "ParkingGarages",
                columns: new[] { "Id", "Address", "NumberOfSpaces" },
                values: new object[,]
                {
                    { 1, "Cím 1", 10 },
                    { 2, "Cím 2", 30 },
                    { 3, "Cím 3", 15 }
                });

            migrationBuilder.InsertData(
                table: "Parkings",
                columns: new[] { "Id", "CarId", "EndOfParking", "ParkingGarageId", "StartOfParking" },
                values: new object[,]
                {
                    { 1, 1, new DateTimeOffset(new DateTime(2023, 6, 26, 16, 12, 9, 583, DateTimeKind.Unspecified).AddTicks(3658), new TimeSpan(0, 0, 0, 0, 0)), 1, new DateTimeOffset(new DateTime(2023, 6, 26, 15, 12, 9, 583, DateTimeKind.Unspecified).AddTicks(3606), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, 3, new DateTimeOffset(new DateTime(2023, 6, 26, 16, 12, 9, 583, DateTimeKind.Unspecified).AddTicks(3658), new TimeSpan(0, 0, 0, 0, 0)), 2, new DateTimeOffset(new DateTime(2023, 6, 26, 15, 12, 9, 583, DateTimeKind.Unspecified).AddTicks(3606), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3, 2, new DateTimeOffset(new DateTime(2023, 6, 26, 16, 12, 9, 583, DateTimeKind.Unspecified).AddTicks(3658), new TimeSpan(0, 0, 0, 0, 0)), 3, new DateTimeOffset(new DateTime(2023, 6, 26, 15, 12, 9, 583, DateTimeKind.Unspecified).AddTicks(3606), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parkings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Parkings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Parkings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkingGarages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkingGarages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkingGarages",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
