using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickFireApi.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "t_sys_config",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300,
                oldComment: "备注");

            migrationBuilder.AlterColumn<string>(
                name: "field3",
                table: "t_sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "备用字段3",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "备用字段3");

            migrationBuilder.AlterColumn<string>(
                name: "field2",
                table: "t_sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "备用字段2",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "备用字段2");

            migrationBuilder.AlterColumn<string>(
                name: "field1",
                table: "t_sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "备用字段1",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "备用字段1");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "sys_config",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true,
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300,
                oldComment: "备注");

            migrationBuilder.AlterColumn<string>(
                name: "field3",
                table: "sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "备用字段3",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "备用字段3");

            migrationBuilder.AlterColumn<string>(
                name: "field2",
                table: "sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "备用字段2",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "备用字段2");

            migrationBuilder.AlterColumn<string>(
                name: "field1",
                table: "sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "备用字段1",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "备用字段1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "t_sys_config",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300,
                oldNullable: true,
                oldComment: "备注");

            migrationBuilder.AlterColumn<string>(
                name: "field3",
                table: "t_sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "备用字段3",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "备用字段3");

            migrationBuilder.AlterColumn<string>(
                name: "field2",
                table: "t_sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "备用字段2",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "备用字段2");

            migrationBuilder.AlterColumn<string>(
                name: "field1",
                table: "t_sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "备用字段1",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "备用字段1");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "sys_config",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                comment: "备注",
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300,
                oldNullable: true,
                oldComment: "备注");

            migrationBuilder.AlterColumn<string>(
                name: "field3",
                table: "sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "备用字段3",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "备用字段3");

            migrationBuilder.AlterColumn<string>(
                name: "field2",
                table: "sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "备用字段2",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "备用字段2");

            migrationBuilder.AlterColumn<string>(
                name: "field1",
                table: "sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "备用字段1",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "备用字段1");
        }
    }
}
