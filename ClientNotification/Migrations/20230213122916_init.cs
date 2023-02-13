using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ClientNotification.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    EMail = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    CreditNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SendGridResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    TemplateId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Response = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendGridResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SendGridResponses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SendGridResponses_MessageTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "MessageTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Amount", "CreditNumber", "DueDate", "EMail", "Name" },
                values: new object[] { 1, 500.0, "AA12345", new DateTime(2023, 2, 13, 13, 29, 16, 366, DateTimeKind.Local).AddTicks(6775), "Random@example.com", "Jone Dou" });

            migrationBuilder.InsertData(
                table: "MessageTemplates",
                columns: new[] { "Id", "Message", "Name" },
                values: new object[,]
                {
                    { 1, "{Creditnumber}\r\n\r\n \r\n\r\nDear {Name}\r\n\r\nPlease pay by {dueDate}\r\n\r\nThe amount {amount}\r\n\r\n \r\n\r\nGreetings Vexcash", "Reminder" },
                    { 2, "Dear {Name}\r\n\r\nThank you for your recent application for a VEXCASH credit. Unfortunately, you do not meet our current criteria for credit approval. \r\n\r\nYour request for {Amount} euro credit was declined\r\n\r\nIf you feel that you have information that will make a difference in these two considerations, please write to us.\r\n", "Cancellation" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreditNumber",
                table: "Customers",
                column: "CreditNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_EMail",
                table: "Customers",
                column: "EMail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Id",
                table: "Customers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MessageTemplates_Name",
                table: "MessageTemplates",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SendGridResponses_CustomerId",
                table: "SendGridResponses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SendGridResponses_Id",
                table: "SendGridResponses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SendGridResponses_TemplateId",
                table: "SendGridResponses",
                column: "TemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SendGridResponses");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "MessageTemplates");
        }
    }
}
