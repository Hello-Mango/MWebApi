using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickFireApi.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "tenant_id",
                table: "t_sys_config",
                type: "bigint",
                nullable: false,
                comment: "租户ID",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "config_value",
                table: "t_sys_config",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "配置值",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "config_key",
                table: "t_sys_config",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "配置键",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<string>(
                name: "field1",
                table: "t_sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "备用字段1");

            migrationBuilder.AddColumn<string>(
                name: "field2",
                table: "t_sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "备用字段2");

            migrationBuilder.AddColumn<string>(
                name: "field3",
                table: "t_sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "备用字段3");

            migrationBuilder.AddColumn<string>(
                name: "remark",
                table: "t_sys_config",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                comment: "备注");

            migrationBuilder.AlterColumn<long>(
                name: "user_id",
                table: "sys_user_tenant",
                type: "bigint",
                nullable: false,
                comment: "用户Id",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<long>(
                name: "tenant_id",
                table: "sys_user_tenant",
                type: "bigint",
                nullable: false,
                comment: "租户Id",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "config_value",
                table: "sys_config",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "配置值",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "config_key",
                table: "sys_config",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "配置键",
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<string>(
                name: "field1",
                table: "sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "备用字段1");

            migrationBuilder.AddColumn<string>(
                name: "field2",
                table: "sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "备用字段2");

            migrationBuilder.AddColumn<string>(
                name: "field3",
                table: "sys_config",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "备用字段3");

            migrationBuilder.AddColumn<string>(
                name: "remark",
                table: "sys_config",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                comment: "备注");

            migrationBuilder.AlterColumn<bool>(
                name: "is_operation",
                table: "sys_app",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否运营系统",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否运行");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "field1",
                table: "t_sys_config");

            migrationBuilder.DropColumn(
                name: "field2",
                table: "t_sys_config");

            migrationBuilder.DropColumn(
                name: "field3",
                table: "t_sys_config");

            migrationBuilder.DropColumn(
                name: "remark",
                table: "t_sys_config");

            migrationBuilder.DropColumn(
                name: "field1",
                table: "sys_config");

            migrationBuilder.DropColumn(
                name: "field2",
                table: "sys_config");

            migrationBuilder.DropColumn(
                name: "field3",
                table: "sys_config");

            migrationBuilder.DropColumn(
                name: "remark",
                table: "sys_config");

            migrationBuilder.AlterColumn<long>(
                name: "tenant_id",
                table: "t_sys_config",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "租户ID");

            migrationBuilder.AlterColumn<string>(
                name: "config_value",
                table: "t_sys_config",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldComment: "配置值");

            migrationBuilder.AlterColumn<string>(
                name: "config_key",
                table: "t_sys_config",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldComment: "配置键");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "sys_user_tenant",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "用户Id");

            migrationBuilder.AlterColumn<string>(
                name: "tenant_id",
                table: "sys_user_tenant",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "租户Id");

            migrationBuilder.AlterColumn<string>(
                name: "config_value",
                table: "sys_config",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldComment: "配置值");

            migrationBuilder.AlterColumn<string>(
                name: "config_key",
                table: "sys_config",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldComment: "配置键");

            migrationBuilder.AlterColumn<bool>(
                name: "is_operation",
                table: "sys_app",
                type: "tinyint(1)",
                nullable: false,
                comment: "是否运行",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "是否运营系统");
        }
    }
}
