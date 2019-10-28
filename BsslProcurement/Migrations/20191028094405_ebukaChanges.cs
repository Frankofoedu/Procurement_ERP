using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class ebukaChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Requisitions_RequisitionId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_RequisitionId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "RequisitionId",
                table: "Attachments");

            migrationBuilder.AlterColumn<string>(
                name: "ProcurementType",
                table: "Requisitions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "AttachmentId",
                table: "RequisitionItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequisitionItems_AttachmentId",
                table: "RequisitionItems",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequisitionItems_Attachments_AttachmentId",
                table: "RequisitionItems",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequisitionItems_Attachments_AttachmentId",
                table: "RequisitionItems");

            migrationBuilder.DropIndex(
                name: "IX_RequisitionItems_AttachmentId",
                table: "RequisitionItems");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "RequisitionItems");

            migrationBuilder.AlterColumn<string>(
                name: "ProcurementType",
                table: "Requisitions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequisitionId",
                table: "Attachments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_RequisitionId",
                table: "Attachments",
                column: "RequisitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Requisitions_RequisitionId",
                table: "Attachments",
                column: "RequisitionId",
                principalTable: "Requisitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
