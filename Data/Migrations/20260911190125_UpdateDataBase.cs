using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "PostComments",
                newName: "Comment");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BasicInfo_DateOfBirth",
                table: "UserProfiles",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostType",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrivacySetting",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileId",
                table: "PostInterActions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PostInterActions_UserProfileId",
                table: "PostInterActions",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_UserProfileId",
                table: "PostComments",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_UserProfiles_UserProfileId",
                table: "PostComments",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostInterActions_UserProfiles_UserProfileId",
                table: "PostInterActions",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_UserProfiles_UserProfileId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostInterActions_UserProfiles_UserProfileId",
                table: "PostInterActions");

            migrationBuilder.DropIndex(
                name: "IX_PostInterActions_UserProfileId",
                table: "PostInterActions");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_UserProfileId",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostType",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PrivacySetting",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "PostInterActions");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "PostComments",
                newName: "Text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BasicInfo_DateOfBirth",
                table: "UserProfiles",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
