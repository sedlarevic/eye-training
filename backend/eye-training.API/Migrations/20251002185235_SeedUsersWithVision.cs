using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eye_training.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersWithVision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i <= 10; i++)
            {
                migrationBuilder.InsertData(
                    table: "Users",
                    columns: new[] { "Id", "FirstName", "LastName", "UserName", "Password", "DateOfBirth" },
                    values: new object[] { i, $"User{i}", $"Test{i}", $"user{i}", "password123", new DateTime(1990, 1, 1) }
                );

                migrationBuilder.InsertData(
                    table: "UserVisions",
                    columns: new[] { "Id", "UserId", "VisionLeftEye", "VisionRightEye", "CylinderLeftEye", "CylinderRightEye", "CreationDate" },
                    values: new object[] { i, i, 1.0m + i * 0.01m, 1.0m + i * 0.01m, 0.25m, 0.25m, DateTime.UtcNow }
                );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i <= 10; i++)
            {
                migrationBuilder.DeleteData(
                    table: "UserVisions",
                    keyColumn: "Id",
                    keyValue: i
                );

                migrationBuilder.DeleteData(
                    table: "Users",
                    keyColumn: "Id",
                    keyValue: i
                );
            }
        }
    }
}
