using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_permission_permission_permission_id",
                table: "role_permission");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permission_role_role_id",
                table: "role_permission");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_role_role_id",
                table: "user_role");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_user_user_id",
                table: "user_role");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "user_role",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "user_role",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_user_id",
                table: "user_role",
                newName: "IX_user_role_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_role_id",
                table: "user_role",
                newName: "IX_user_role_RoleId");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "role_permission",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "permission_id",
                table: "role_permission",
                newName: "PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_role_permission_role_id",
                table: "role_permission",
                newName: "IX_role_permission_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_role_permission_permission_id",
                table: "role_permission",
                newName: "IX_role_permission_PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_role_permission_permission_PermissionId",
                table: "role_permission",
                column: "PermissionId",
                principalTable: "permission",
                principalColumn: "permission_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permission_role_RoleId",
                table: "role_permission",
                column: "RoleId",
                principalTable: "role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_role_RoleId",
                table: "user_role",
                column: "RoleId",
                principalTable: "role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_user_UserId",
                table: "user_role",
                column: "UserId",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_permission_permission_PermissionId",
                table: "role_permission");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permission_role_RoleId",
                table: "role_permission");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_role_RoleId",
                table: "user_role");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_user_UserId",
                table: "user_role");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_role",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "user_role",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_UserId",
                table: "user_role",
                newName: "IX_user_role_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_RoleId",
                table: "user_role",
                newName: "IX_user_role_role_id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "role_permission",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "role_permission",
                newName: "permission_id");

            migrationBuilder.RenameIndex(
                name: "IX_role_permission_RoleId",
                table: "role_permission",
                newName: "IX_role_permission_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_role_permission_PermissionId",
                table: "role_permission",
                newName: "IX_role_permission_permission_id");

            migrationBuilder.AddForeignKey(
                name: "FK_role_permission_permission_permission_id",
                table: "role_permission",
                column: "permission_id",
                principalTable: "permission",
                principalColumn: "permission_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permission_role_role_id",
                table: "role_permission",
                column: "role_id",
                principalTable: "role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_role_role_id",
                table: "user_role",
                column: "role_id",
                principalTable: "role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_user_user_id",
                table: "user_role",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
