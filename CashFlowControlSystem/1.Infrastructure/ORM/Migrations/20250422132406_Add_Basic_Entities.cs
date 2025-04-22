using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORM.Migrations
{
    /// <inheritdoc />
    public partial class Add_Basic_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cashflowsystem_schema");

            migrationBuilder.CreateTable(
                name: "Merchants",
                schema: "cashflowsystem_schema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TaxId = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    ContactEmail = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ContactPhone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MERCHANT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "cashflowsystem_schema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "varchar(8)", nullable: false),
                    Status = table.Column<string>(type: "varchar(9)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyBalance",
                schema: "cashflowsystem_schema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uuid", nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalCredits = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalDebits = table.Column<decimal>(type: "numeric", nullable: false),
                    ClosingBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionCount = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DAILY_BALANCE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyBalance_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalSchema: "cashflowsystem_schema",
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "cashflowsystem_schema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    PaymentMethod = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    Status = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSACTION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalSchema: "cashflowsystem_schema",
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DAILY_BALANCE_MERCHANT_ID",
                schema: "cashflowsystem_schema",
                table: "DailyBalance",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_MERCHANT_NAME",
                schema: "cashflowsystem_schema",
                table: "Merchants",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACTION_MERCHANTID",
                schema: "cashflowsystem_schema",
                table: "Transactions",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_NAME",
                schema: "cashflowsystem_schema",
                table: "Users",
                column: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyBalance",
                schema: "cashflowsystem_schema");

            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "cashflowsystem_schema");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "cashflowsystem_schema");

            migrationBuilder.DropTable(
                name: "Merchants",
                schema: "cashflowsystem_schema");
        }
    }
}
