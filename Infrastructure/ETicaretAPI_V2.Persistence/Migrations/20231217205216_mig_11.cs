using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretAPI_V2.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingCompanies_ShippingCompanyId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingCompanyId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CargoTrackingCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingCompanyId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "CargoTrackingCode",
                table: "CompletedOrders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShippingCompanyId",
                table: "CompletedOrders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompletedOrders_ShippingCompanyId",
                table: "CompletedOrders",
                column: "ShippingCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedOrders_ShippingCompanies_ShippingCompanyId",
                table: "CompletedOrders",
                column: "ShippingCompanyId",
                principalTable: "ShippingCompanies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedOrders_ShippingCompanies_ShippingCompanyId",
                table: "CompletedOrders");

            migrationBuilder.DropIndex(
                name: "IX_CompletedOrders_ShippingCompanyId",
                table: "CompletedOrders");

            migrationBuilder.DropColumn(
                name: "CargoTrackingCode",
                table: "CompletedOrders");

            migrationBuilder.DropColumn(
                name: "ShippingCompanyId",
                table: "CompletedOrders");

            migrationBuilder.AddColumn<string>(
                name: "CargoTrackingCode",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShippingCompanyId",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingCompanyId",
                table: "Orders",
                column: "ShippingCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingCompanies_ShippingCompanyId",
                table: "Orders",
                column: "ShippingCompanyId",
                principalTable: "ShippingCompanies",
                principalColumn: "Id");
        }
    }
}
