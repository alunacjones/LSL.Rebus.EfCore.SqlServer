using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LSL.Rebus.EfCore.SqlServer.TestApp.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sagas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    revision = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sagas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SagaIndexes",
                columns: table => new
                {
                    key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    sagatype = table.Column<string>(name: "saga_type", type: "nvarchar(40)", maxLength: 40, nullable: false),
                    sagaid = table.Column<Guid>(name: "saga_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SagaIndexes", x => new { x.key, x.value, x.sagatype });
                    table.ForeignKey(
                        name: "FK_SagaIndexes_Sagas_saga_id",
                        column: x => x.sagaid,
                        principalTable: "Sagas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SagaIndexes_saga_id",
                table: "SagaIndexes",
                column: "saga_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SagaIndexes");

            migrationBuilder.DropTable(
                name: "Sagas");
        }
    }
}
