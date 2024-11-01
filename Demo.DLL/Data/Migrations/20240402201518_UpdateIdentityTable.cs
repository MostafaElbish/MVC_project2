﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.DAL.Data.Migrations
{
    public partial class UpdateIdentityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Isagree",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isagree",
                table: "AspNetUsers");
        }
    }
}
