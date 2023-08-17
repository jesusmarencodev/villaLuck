using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VillaLuck.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle Villa 1", new DateTime(2023, 8, 17, 11, 11, 4, 124, DateTimeKind.Local).AddTicks(1140), new DateTime(2023, 8, 17, 11, 11, 4, 124, DateTimeKind.Local).AddTicks(1127), "", 200, "Villa 1", 5, 200.0 },
                    { 2, "", "Detalle Villa 2", new DateTime(2023, 8, 17, 11, 11, 4, 124, DateTimeKind.Local).AddTicks(1143), new DateTime(2023, 8, 17, 11, 11, 4, 124, DateTimeKind.Local).AddTicks(1142), "", 300, "Villa 2", 7, 300.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
