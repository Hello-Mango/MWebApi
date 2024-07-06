using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace QuickFireApi.Migrations
{
    /// <inheritdoc />
    public partial class adjust2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sys_user_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false, comment: "用户Id"),
                    role_id = table.Column<long>(type: "bigint", nullable: false, comment: "角色Id"),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false, comment: "租户Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_user_role", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_user_role");
        }
    }
}
