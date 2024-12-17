using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GusPizza.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationFromTransactionDetailToPizza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "TransactionDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "PizzaId",
                table: "TransactionDetails",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_PizzaId",
                table: "TransactionDetails",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_Pizzas_PizzaId",
                table: "TransactionDetails",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_Pizzas_PizzaId",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_PizzaId",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "TransactionDetails");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "TransactionDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
