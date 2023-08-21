using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaLuck.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 18, 16, 36, 0, 468, DateTimeKind.Local).AddTicks(6989), new DateTime(2023, 8, 18, 16, 36, 0, 468, DateTimeKind.Local).AddTicks(6975) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 18, 16, 36, 0, 468, DateTimeKind.Local).AddTicks(6991), new DateTime(2023, 8, 18, 16, 36, 0, 468, DateTimeKind.Local).AddTicks(6990) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 18, 11, 30, 52, 153, DateTimeKind.Local).AddTicks(2321), new DateTime(2023, 8, 18, 11, 30, 52, 153, DateTimeKind.Local).AddTicks(2309) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 8, 18, 11, 30, 52, 153, DateTimeKind.Local).AddTicks(2325), new DateTime(2023, 8, 18, 11, 30, 52, 153, DateTimeKind.Local).AddTicks(2324) });
        }
    }
}
