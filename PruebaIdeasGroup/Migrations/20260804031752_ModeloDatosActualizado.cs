using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PruebaIdeasGroup.Migrations
{
    /// <inheritdoc />
    public partial class ModeloDatosActualizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_Usuarios_UsuarioId",
                table: "Proyectos");

            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Usuarios_UsuarioId",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_UsuarioId",
                table: "Tareas");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Tareas",
                newName: "Prioridad");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Proyectos",
                newName: "CreadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_Proyectos_UsuarioId",
                table: "Proyectos",
                newName: "IX_Proyectos_CreadoPorId");

            migrationBuilder.CreateTable(
                name: "TareaUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TareaId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareaUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TareaUsuario_Tareas_TareaId",
                        column: x => x.TareaId,
                        principalTable: "Tareas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareaUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TareaUsuario_TareaId",
                table: "TareaUsuario",
                column: "TareaId");

            migrationBuilder.CreateIndex(
                name: "IX_TareaUsuario_UsuarioId",
                table: "TareaUsuario",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_Usuarios_CreadoPorId",
                table: "Proyectos",
                column: "CreadoPorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_Usuarios_CreadoPorId",
                table: "Proyectos");

            migrationBuilder.DropTable(
                name: "TareaUsuario");

            migrationBuilder.RenameColumn(
                name: "Prioridad",
                table: "Tareas",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "CreadoPorId",
                table: "Proyectos",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Proyectos_CreadoPorId",
                table: "Proyectos",
                newName: "IX_Proyectos_UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_UsuarioId",
                table: "Tareas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_Usuarios_UsuarioId",
                table: "Proyectos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Usuarios_UsuarioId",
                table: "Tareas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
