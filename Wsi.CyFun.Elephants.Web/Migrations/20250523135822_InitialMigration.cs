using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Wsi.CyFun.Elephants.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAssessor = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Functions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maturities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Threshold = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maturities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Municipalities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFr = table.Column<bool>(type: "bit", nullable: false),
                    IsDe = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    FunctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Functions_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Functions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaturityLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Documentation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Implementation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaturityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaturityLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaturityLevels_Maturities_MaturityId",
                        column: x => x.MaturityId,
                        principalTable: "Maturities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MunicipalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaturityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessments_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assessments_ApplicationUsers_AssessorId",
                        column: x => x.AssessorId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assessments_Maturities_MaturityId",
                        column: x => x.MaturityId,
                        principalTable: "Maturities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessments_Municipalities_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsKeyMeasurment = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requirements_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guidances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    RequirementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsTitle = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guidances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guidances_Requirements_RequirementId",
                        column: x => x.RequirementId,
                        principalTable: "Requirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    AssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequirementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentationMaturityScore = table.Column<int>(type: "int", nullable: false),
                    ImplementationMaturityScore = table.Column<int>(type: "int", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssessorComment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => new { x.AssessmentId, x.RequirementId });
                    table.ForeignKey(
                        name: "FK_Scores_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scores_Requirements_RequirementId",
                        column: x => x.RequirementId,
                        principalTable: "Requirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "Created", "Deleted", "IsAdmin", "IsAssessor", "Updated", "Username" },
                values: new object[,]
                {
                    { new Guid("6f2c8a91-3d47-4f1b-ae9c-71b3d5c4e8f2"), new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9900), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bart" },
                    { new Guid("a93e1d4b-2c58-4f76-87a1-ccf5a9d1b3e4"), new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9900), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Peter" },
                    { new Guid("b7f4c2d1-98ea-4a65-9e20-17d6b3a8f0c9"), new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9900), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Milleto" }
                });

            migrationBuilder.InsertData(
                table: "Functions",
                columns: new[] { "Id", "Code", "Created", "Deleted", "Description", "Name", "Order", "Updated" },
                values: new object[,]
                {
                    { new Guid("2a3b4c5d-6e7f-48a9-b0c1-d2e3f4a5b6c7"), "RC", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9670), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Recover", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7e6d5c4b-3a2f-41e0-d9c8-b7a6f5e4d3c2"), "DE", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9660), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Detect", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8f7d6c5e-b4a3-48d2-9f1e-0c8b7a6d5e4f"), "ID", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9620), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Identify", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9d8c7b6a-5f4e-49d3-c2b1-a0b9c8d7e6f5"), "RS", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9670), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Respond", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a1b2c3d4-e5f6-47a8-b9c0-d1e2f3a4b5c6"), "PR", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9660), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Protect", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Maturities",
                columns: new[] { "Id", "Created", "Deleted", "Description", "Name", "Threshold", "Updated" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Initial", null, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4fa76e53-6826-5673-c4ed-3d874e77b0b7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Repeatable", null, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5fb67d42-7935-6784-d5fa-4e985d88c1c8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Defined", null, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Municipalities",
                columns: new[] { "Id", "Created", "Deleted", "Name", "Updated" },
                values: new object[,]
                {
                    { new Guid("6ec58c31-8a44-7895-e60b-5f096e99d2d9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brugge", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7fd49b20-9b55-89a6-f71c-601a7faad3ea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Knokke", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8ae30a09-ac66-9ab7-082d-612bc0bbe4fb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gent", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "Created", "Deleted", "Description", "FunctionId", "Name", "Order", "Updated" },
                values: new object[,]
                {
                    { new Guid("1a2b3c4d-5e6f-47a8-b9c0-d1e2f3a4b5c6"), "ID.RA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("8f7d6c5e-b4a3-48d2-9f1e-0c8b7a6d5e4f"), "Risk Assessment", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3c4d5e6f-7a8b-49c0-a1b2-c3d4e5f6a7b8"), "ID.BE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("8f7d6c5e-b4a3-48d2-9f1e-0c8b7a6d5e4f"), "Business Environment ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4a5b6c7d-8e9f-40a1-b2c3-d4e5f6a7b8c9"), "ID.RM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("8f7d6c5e-b4a3-48d2-9f1e-0c8b7a6d5e4f"), "Risk Management Strategy", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5f4e3d2c-1b0a-49a8-b7c6-d5e4f3a2b1c0"), "ID.AM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("8f7d6c5e-b4a3-48d2-9f1e-0c8b7a6d5e4f"), "Asset Management", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6a7b8c9d-0e1f-42a3-b4c5-d6e7f8a9b0c1"), "ID.GV", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("8f7d6c5e-b4a3-48d2-9f1e-0c8b7a6d5e4f"), "Governance", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a3b2c1d0-e9f8-48a8-b7c6-d5e4f3a2b1c0"), "DE.AE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("7e6d5c4b-3a2f-41e0-d9c8-b7a6f5e4d3c2"), "Anomalies and Events", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a4b3c2d1-e0f9-48a8-b7c6-d5e4f3a2b1c0"), "PR.AC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("a1b2c3d4-e5f6-47a8-b9c0-d1e2f3a4b5c6"), "Identity Management, Authentication and Access Control ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a7b6c5d4-e3f2-83a2-b1c0-d9e8f7a6b5c4"), "RS.MI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("9d8c7b6a-5f4e-49d3-c2b1-a0b9c8d7e6f5"), "Mitigation", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b2c1d0e9-f8a7-37a6-b5c4-d3e2f1a0b9c8"), "DE.CM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("7e6d5c4b-3a2f-41e0-d9c8-b7a6f5e4d3c2"), "Security Continuous Monitoring", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b6c5d4e3-f2a1-94a1-b2c3-d4e5f6a7b8c9"), "RS.IM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("9d8c7b6a-5f4e-49d3-c2b1-a0b9c8d7e6f5"), "Improvements", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b7c6d5e4-f3a2-49d5-e4f3-a2b1c0d9e8f7"), "PR.AT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("a1b2c3d4-e5f6-47a8-b9c0-d1e2f3a4b5c6"), "Awareness and Training", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b9c8d7e6-f5a4-43b3-a2c1-b0a9c8d7e6f5"), "ID.SC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("8f7d6c5e-b4a3-48d2-9f1e-0c8b7a6d5e4f"), "Supply Chain Risk Management", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c1d0e9f8-a7b6-78a9-b0c1-d2e3f4a5b6c7"), "DE.DP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("7e6d5c4b-3a2f-41e0-d9c8-b7a6f5e4d3c2"), "Detection Processes", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c5d4e3f2-a1b0-10a2-b1c9-d8e7f6a5b4c3"), "RC.RP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("2a3b4c5d-6e7f-48a9-b0c1-d2e3f4a5b6c7"), "Recovery Planning", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c6d5e4f3-a2b1-44a0-b9c8-d7e6f5a4b3c2"), "PR.DS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("a1b2c3d4-e5f6-47a8-b9c0-d1e2f3a4b5c6"), "Data Security", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d0e9f8a7-b6c5-26a3-b2c1-d0e9f8a7b6c5"), "RS.RP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("9d8c7b6a-5f4e-49d3-c2b1-a0b9c8d7e6f5"), "Detection Processes", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d4e3f2a1-b0c9-21a4-b3c2-d1e0f9e8d7c6"), "RC.IM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("2a3b4c5d-6e7f-48a9-b0c1-d2e3f4a5b6c7"), "Improvements", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d5e4f3a2-b1c0-47a1-b0c9-d8e7f6a5b4c3"), "PR.IP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("a1b2c3d4-e5f6-47a8-b9c0-d1e2f3a4b5c6"), "Information Protection Processes and Procedures", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e3f2a1b0-c9d8-32a8-b9c0-d1e2f3a4b5c6"), "RC.CO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("2a3b4c5d-6e7f-48a9-b0c1-d2e3f4a5b6c7"), "Communications", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e4f3a2b1-c0d9-45a7-b6c5-d4e3f2a1b0c9"), "PR.MA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("a1b2c3d4-e5f6-47a8-b9c0-d1e2f3a4b5c6"), "Maintenance", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e9f8a7b6-c5d4-59a7-b6c5-d4e3f2a1b0c9"), "RS.CO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("9d8c7b6a-5f4e-49d3-c2b1-a0b9c8d7e6f5"), "Communications", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f3a2b1c0-d9e8-40a1-b2c3-d4e5f6a7b8c9"), "PR.PT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("a1b2c3d4-e5f6-47a8-b9c0-d1e2f3a4b5c6"), "Protective Technology", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f8a7b6c5-d4e3-71a8-b9c0-d1e2f3a4b5c6"), "RS.AN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", new Guid("9d8c7b6a-5f4e-49d3-c2b1-a0b9c8d7e6f5"), "Analysis", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Code", "Created", "Deleted", "Description", "Name", "Order", "Updated" },
                values: new object[,]
                {
                    { new Guid("12345678-90ab-cdef-1234-567890abcdef"), new Guid("3c4d5e6f-7a8b-49c0-a1b2-c3d4e5f6a7b8"), "ID.BE-1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9720), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De rol van de organisatie in de toeleveringsketen wordt geïdentificeerd en gecommuniceerd", "", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("13579bdf-2468-ace0-1357-9bdf2468ace0"), new Guid("3c4d5e6f-7a8b-49c0-a1b2-c3d4e5f6a7b8"), "ID.BE-5", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9730), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De veerkrachtvereisten ter ondersteuning van de levering van kritieke diensten zijn vastgesteld voor alle operationele toestanden (bijv. onder dwang/aanval, tijdens herstel, normale activiteiten)", "", 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2468ace0-1357-9bdf-2468-ace01357bdf2"), new Guid("6a7b8c9d-0e1f-42a3-b4c5-d6e7f8a9b0c1"), "ID.GV-1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9730), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Het cyberbeveiligingsbeleid van de organisatie wordt vastgesteld en gecommuniceerd", "", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3a1b9c5d-7e8f-4a2b-9c3d-5e6f7a8b9c0d"), new Guid("5f4e3d2c-1b0a-49a8-b7c6-d5e4f3a2b1c0"), "ID.AM-4", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9710), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Externe informatiesystemen worden gecatalogiseerd", "", 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3301"), new Guid("5f4e3d2c-1b0a-49a8-b7c6-d5e4f3a2b1c0"), "ID.AM-6", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9720), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cyberbeveiligingsrollen, -verantwoordelijkheden en -bevoegdheden voor het voltallige personeel en externe belanghebbenden zijn vastgesteld", "", 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4d7f2b98-1e6c-4a3d-90e1-7b2a5fc31d49"), new Guid("b9c8d7e6-f5a4-43b3-a2c1-b0a9c8d7e6f5"), "ID.SC-2", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9760), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leveranciers en externe partners van informatiesystemen, onderdelen en diensten worden geïdentificeerd, geprioriteerd en beoordeeld met behulp van een proces voor de beoordeling van cyberrisico's in de toeleveringsketen.", "", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("98765432-10fe-dcba-9876-543210fedcba"), new Guid("3c4d5e6f-7a8b-49c0-a1b2-c3d4e5f6a7b8"), "ID.BE-3", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9730), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prioriteiten voor de missie, doelstellingen en activiteiten van de organisatie worden vastgesteld en gecommuniceerd", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9b8c7d6e-5f4e-4d3c-b2a1-0f9e8d7c6b5a"), new Guid("5f4e3d2c-1b0a-49a8-b7c6-d5e4f3a2b1c0"), "ID.AM-5", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9720), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hulpbronnen (bijv. hardware, apparaten, gegevens, tijd, personeel en software) worden geprioriteerd op basis van hun classificatie, kriticiteit en bedrijfswaarde.", "", 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9c3e7a2f-6d41-4b19-ae5f-83f1c728d2b0"), new Guid("b9c8d7e6-f5a4-43b3-a2c1-b0a9c8d7e6f5"), "ID.SC-3", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9760), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Contracten met leveranciers en externe partners worden gebruikt om passende maatregelen te implementeren die zijn ontworpen om te voldoen aan de doelstellingen van het cyberbeveiligingsprogramma en het risicobeheerplan voor de cyberketen van een organisatie.", "", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890"), new Guid("6a7b8c9d-0e1f-42a3-b4c5-d6e7f8a9b0c1"), "ID.GV-3", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9740), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wettelijke en regelgevende vereisten met betrekking tot cyberbeveiliging, inclusief verplichtingen op het gebied van privacy en burgerlijke vrijheden, worden begrepen en beheerd.", "", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a1b2c3d4-e5f6-a1b2-c3d4-e5f6a1b2c3d4"), new Guid("4a5b6c7d-8e9f-40a1-b2c3-d4e5f6a7b8c9"), "ID.RM-1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9750), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Risicomaatregelen worden geïdentificeerd en geprioriteerd", "", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a2d4c7f9-8b3e-45a0-b1c2-73f0e4d19f6e"), new Guid("b9c8d7e6-f5a4-43b3-a2c1-b0a9c8d7e6f5"), "ID.SC-4", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9760), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leveranciers en externe partners worden routinematig beoordeeld met behulp van audits, testresultaten of andere vormen van evaluaties om te bevestigen dat ze aan hun contractuele verplichtingen voldoen.", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("abcdef12-3456-7890-abcd-ef1234567890"), new Guid("3c4d5e6f-7a8b-49c0-a1b2-c3d4e5f6a7b8"), "ID.BE-2", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9720), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De plaats van de organisatie in kritieke infrastructuur en haar bedrijfstak wordt geïdentificeerd en gecommuniceerd", "", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b2c3d4e5-f6a1-b2c3-d4e5-f67890123456"), new Guid("4a5b6c7d-8e9f-40a1-b2c3-d4e5f6a7b8c9"), "ID.RM-2", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9750), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Organisatorische risicotolerantie wordt bepaald en duidelijk uitgedrukt", "", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b2c3d4e5-f6a1-b2c3-d4e5-f6a1b2c3d4e5"), new Guid("6a7b8c9d-0e1f-42a3-b4c5-d6e7f8a9b0c1"), "ID.GV-4", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9740), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Governance- en risicobeheerprocessen richten zich op cyberbeveiligingsrisico's", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c3d4e5f6-a1b2-c3d4-e5f6-789012345678"), new Guid("4a5b6c7d-8e9f-40a1-b2c3-d4e5f6a7b8c9"), "ID.RM-3", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9760), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De organisatie bepaalt haar risicotolerantie op basis van haar rol in kritieke infrastructuur en sectorspecifieke risicoanalyse.", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c3d4e5f6-a1b2-c3d4-e5f6-a1b2c3d4e5f6"), new Guid("1a2b3c4d-5e6f-47a8-b9c0-d1e2f3a4b5c6"), "ID.RA-1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9740), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kwetsbaarheden van bedrijfsmiddelen worden geïdentificeerd en gedocumenteerd", "", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d2c19e8a-4b76-4d3f-9e3c-840a123b5d22"), new Guid("5f4e3d2c-1b0a-49a8-b7c6-d5e4f3a2b1c0"), "ID.AM-3", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9710), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Organisatiecommunicatie en gegevensstromen worden in kaart gebracht\n", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d4e5f6a1-b2c3-d4e5-f6a1-b2c3d4e5f6a1"), new Guid("1a2b3c4d-5e6f-47a8-b9c0-d1e2f3a4b5c6"), "ID.RA-2", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9740), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Informatie over cyberdreigingen wordt ontvangen van forums en bronnen waar informatie wordt gedeeld", "", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e5f6a1b2-c3d4-e5f6-a1b2-c3d4e5f6a1b2"), new Guid("1a2b3c4d-5e6f-47a8-b9c0-d1e2f3a4b5c6"), "ID.RA-5", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9750), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bedreigingen, kwetsbaarheden, waarschijnlijkheden en gevolgen worden gebruikt om risico's te bepalen.", "", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e6f5c4d3-a2b1-44c0-b9d8-e7f6a5b4c3d2"), new Guid("5f4e3d2c-1b0a-49a8-b7c6-d5e4f3a2b1c0"), "ID.AM-1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9700), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fysieke apparaten en systemen die binnen de organisatie worden gebruikt, worden geïnventariseerd", "", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f5b1a8c3-2e4d-4c76-9d9f-2a6e1c9b8a71"), new Guid("b9c8d7e6-f5a4-43b3-a2c1-b0a9c8d7e6f5"), "ID.SC-5", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9770), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reactie- en herstelplanning en tests worden uitgevoerd met leveranciers en externe leveranciers", "", 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f6a1b2c3-d4e5-f6a1-b2c3-d4e5f6a1b2c3"), new Guid("1a2b3c4d-5e6f-47a8-b9c0-d1e2f3a4b5c6"), "ID.RA-6", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9750), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Risicomaatregelen worden geïdentificeerd en geprioriteerd", "", 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fedcba98-7654-3210-fedc-ba9876543210"), new Guid("3c4d5e6f-7a8b-49c0-a1b2-c3d4e5f6a7b8"), "ID.BE-4", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9730), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Afhankelijkheden en kritieke functies voor de levering van kritieke diensten zijn vastgesteld", "", 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ff81c45e-f75e-4c45-a47c-b5c5055c2161"), new Guid("5f4e3d2c-1b0a-49a8-b7c6-d5e4f3a2b1c0"), "ID.AM-2", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9710), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Softwareplatformen, en applicaties die binnen de organisatie worden gebruikt, worden geïnventariseerd", "", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Requirements",
                columns: new[] { "Id", "Code", "Created", "Deleted", "Description", "IsKeyMeasurment", "Order", "SubCategoryId", "Updated" },
                values: new object[,]
                {
                    { new Guid("12a4c7d9-8f30-462e-981a-6a5bd2e93f5e"), "ID.SC-4.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9880), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De organisatie controleert beoordelingen van de naleving van contractuele verplichtingen door leveranciers en externe partners door routinematig audits, testresultaten en andere evaluaties te controleren.", false, 35, new Guid("a2d4c7f9-8b3e-45a0-b1c2-73f0e4d19f6e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), "ID.AM-2.4", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9800), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wanneer niet-geautoriseerde software wordt gedetecteerd, wordt deze in quarantaine geplaatst voor mogelijke uitzonderingsbehandeling, verwijderd of vervangen en wordt de inventaris dienovereenkomstig bijgewerkt.", false, 7, new Guid("ff81c45e-f75e-4c45-a47c-b5c5055c2161"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1a7f6c3d-b452-44f9-8e01-9c2a17d3fcb4"), "ID.GV-4.2", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9850), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Informatiebeveiligings- en cyberbeveiligingsrisico's worden gedocumenteerd, formeel goedgekeurd en bijgewerkt wanneer zich wijzigingen voordoen.", false, 23, new Guid("b2c3d4e5-f6a1-b2c3-d4e5-f6a1b2c3d4e5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1d2c3b4a-5f6e-7d8c-9b0a-1d2e3f4c5d6e"), "ID.GV-1.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9830), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beleid en procedures voor informatiebeveiliging en cyberveiligheid worden opgesteld, gedocumenteerd, beoordeeld, goedgekeurd en bijgewerkt wanneer zich wijzigingen voordoen.", false, 18, new Guid("2468ace0-1357-9bdf-2468-ace01357bdf2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2a3b4c5d-6e7f-8a9b-0c1d-2e3f4a5b6c7d"), "ID.AM-4.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9810), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De organisatie moet alle externe services en de verbindingen die ermee zijn gemaakt in kaart brengen, documenteren, autoriseren en bij wijzigingen bijwerken.", false, 10, new Guid("3a1b9c5d-7e8f-4a2b-9c3d-5e6f7a8b9c0d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2e3f4d5c-6b7a-8c9d-0e1f-2d3e4f5a6b7c"), "ID.AM-2.2", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9790), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De inventarisatie van softwareplatforms en applicaties die verband houden met informatie en informatieverwerking moet veranderingen in de  context van de organisatie weerspiegelen  en alle informatie bevatten die nodig is voor effectieve verantwoording.", false, 5, new Guid("ff81c45e-f75e-4c45-a47c-b5c5055c2161"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3a4b5c6d-7e8f-9a0b-1c2d-3e4f5a6b7c8d"), "ID.GV-3.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9840), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wettelijke en regelgevende vereisten met betrekking tot informatie-/cyberbeveiliging, waaronder privacyverplichtingen, worden begrepen en geïmplementeerd.", false, 20, new Guid("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3c4d5e6f-7a8b-9c0d-1e2f-3a4b5c6d7e8f"), "ID.RM-1.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9870), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Een cyberrisicobeheerproces dat de belangrijkste interne en externe belanghebbenden identificeert en het aanpakken van risicogerelateerde kwesties en informatie vergemakkelijkt, moet worden gecreëerd, gedocumenteerd, herzien, goedgekeurd en bijgewerkt wanneer zich wijzigingen voordoen.", false, 30, new Guid("a1b2c3d4-e5f6-a1b2-c3d4-e5f6a1b2c3d4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3e4f5a6b-7c8d-9e0f-1a2b-3c4d5e6f7a8b"), "ID.BE-3.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9820), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prioriteiten voor de missie, doelstellingen en activiteiten van de organisatie worden vastgesteld en gecommuniceerd.", false, 15, new Guid("98765432-10fe-dcba-9876-543210fedcba"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3e5a9c84-7b1f-4d0c-8fa1-92c4b7e3d2a9"), "ID.SC-2.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9870), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De organisatie moet minstens een keer per jaar risicobeoordelingen van de cybertoeleveringsketen uitvoeren of wanneer zich een wijziging voordoet in de kritieke systemen, de operationele omgeving of de toeleveringsketen van de organisatie; deze beoordelingen moeten worden gedocumenteerd en de resultaten moeten worden verspreid onder relevante belanghebbenden, waaronder degenen die verantwoordelijk zijn voor ICT/OT-systemen.", false, 33, new Guid("4d7f2b98-1e6c-4a3d-90e1-7b2a5fc31d49"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4b5c6d7e-8f9a-0b1c-2d3e-4f5a6b7c8d9e"), "ID.AM-6.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9820), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De rollen, verantwoordelijkheden en bevoegdheden op het gebied van informatiebeveiliging en cyberbeveiliging binnen de organisatie moeten worden gedocumenteerd.", true, 12, new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3301"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5a8e1d9c-7f4b-4c6f-b2e3-1d9a7f6e4c8b"), "ID.AM-1.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9780), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Een inventarisatie van bedrijfsmiddelen die verband houden met informatie en informatieverwerkingsfaciliteiten binnen de organisatie moet worden gedocumenteerd, herzien en bijgewerkt wanneer zich wijzigingen voordoen.", false, 1, new Guid("e6f5c4d3-a2b1-44c0-b9d8-e7f6a5b4c3d2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5e4d3c2b-1a9f-8e7d-6c5b-4a3f2e1d0c9b"), "ID.AM-3.3", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9810), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alle verbindingen binnen de ICT/OT-omgeving van de organisatie en met andere interne platforms van de organisatie moeten in kaart worden gebracht, gedocumenteerd, goedgekeurd en waar nodig bijgewerkt.", false, 9, new Guid("d2c19e8a-4b76-4d3f-9e3c-840a123b5d22"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5e6f7a8b-9c0d-1e2f-3a4b-5c6d7e8f9a0b"), "ID.RM-3.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9870), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De rol van de organisatie in kritieke infrastructuur en de sector bepalen de risicobereidheid van de organisatie.", false, 32, new Guid("c3d4e5f6-a1b2-c3d4-e5f6-789012345678"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5f6e7d8c-9b0a-1d2e-3f4c-5d6e7f8a9b0c"), "ID.BE-5.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9830), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Om de cyberweerbaarheid te ondersteunen en de levering van kritieke diensten te beveiligen, worden de nodige vereisten geïdentificeerd, gedocumenteerd en hun implementatie getest en goedgekeurd.", false, 17, new Guid("13579bdf-2468-ace0-1357-9bdf2468ace0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6d3f9b12-4c7e-42a3-9fd2-b5a0e61c2784"), "ID.RA-6.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9860), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Er moet een allesomvattende strategie worden ontwikkeld en ge誰mplementeerd om de risico's voor de kritieke systemen van de organisatie te beheren, inclusief de identificatie en prioritering van risicomaatregelen.", false, 29, new Guid("f6a1b2c3-d4e5-f6a1-b2c3-d4e5f6a1b2c3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d"), "ID.BE-2.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9820), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De plaats van de organisatie in kritieke infrastructuur en haar bedrijfstak moet worden vastgesteld en gecommuniceerd.", false, 14, new Guid("abcdef12-3456-7890-abcd-ef1234567890"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7b6e2a0c-6f42-4e2c-9a2a-0a1b2c3d4e5f"), "ID.AM-1.2", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9790), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De inventaris van bedrijfsmiddelen die verband houden met informatie en informatieverwerkingsfaciliteiten moet veranderingen in de  context van de organisatie weerspiegelen  en alle informatie bevatten die nodig is voor effectieve verantwoording.", false, 2, new Guid("e6f5c4d3-a2b1-44c0-b9d8-e7f6a5b4c3d2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7c2f4b91-5de3-470a-bc12-e8f3d6a9c014"), "ID.RA-1.2", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9850), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Er moet een proces worden vastgesteld om kwetsbaarheden van de bedrijfskritische systemen van de organisatie continu te bewaken, te identificeren en te documenteren.", false, 25, new Guid("c3d4e5f6-a1b2-c3d4-e5f6-a1b2c3d4e5f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7f8e9d0c-1b2a-3d4c-5e6f-7a8b9c0d1e2f"), "ID.GV-1.2", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9830), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Er moet een informatiebeveiligings- en cyberbeveiligingsbeleid voor de hele organisatie worden opgesteld, gedocumenteerd, bijgewerkt wanneer zich wijzigingen voordoen, verspreid en goedgekeurd door het senior management.", false, 19, new Guid("2468ace0-1357-9bdf-2468-ace01357bdf2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8f9e0d1c-2b3a-4d5e-6f7a-8b9c0d1e2f3a"), "ID.AM-5.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9810), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De middelen van de organisatie (hardware, apparaten, gegevens, tijd, personeel, informatie en software) moeten worden geprioriteerd op basis van hun classificatie, kriticiteit en bedrijfswaarde.", false, 11, new Guid("9b8c7d6e-5f4e-4d3c-b2a1-0f9e8d7c6b5a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9a0b1c2d-3e4f-5a6b-7c8d-9e0f1a2b3c4d"), "ID.RM-2.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9870), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De organisatie moet duidelijk haar risicobereidheid bepalen.", false, 31, new Guid("b2c3d4e5-f6a1-b2c3-d4e5-f67890123456"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9c0b1a2d-3e4f-5c6b-7a8d-9e0f1a2b3c4d"), "ID.BE-4.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9830), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Afhankelijkheden en missiekritische functies voor de levering van kritieke diensten worden geïdentificeerd, gedocumenteerd en geprioriteerd op basis van hun kriticiteit als onderdeel van het risicobeoordelingsproces.", false, 16, new Guid("fedcba98-7654-3210-fedc-ba9876543210"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9d8c7b6a-5f4e-3d2c-1b0a-9f8e7d6c5b4a"), "ID.SC-5.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9880), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De organisatie moet de belangrijkste medewerkers van leveranciers en externe partners identificeren en documenteren om hen als belanghebbenden te betrekken bij de reactie- en herstelplanningsactiviteiten.", false, 36, new Guid("f5b1a8c3-2e4d-4c76-9d9f-2a6e1c9b8a71"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9e0f1a2b-3c4d-5e6f-7a8b-9c0d1e2f3a4b"), "ID.GV-3.2", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9840), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wettelijke en regelgevende vereisten met betrekking tot informatie-/cyberbeveiliging, waaronder privacyverplichtingen, worden beheerd.", false, 21, new Guid("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9fd2713a-bd4e-4f16-a802-3c7f14e0d6c1"), "ID.SC-3.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9880), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Op basis van de resultaten van de risicobeoordeling van de cybertoeleveringsketen wordt een contractueel kader voor leveranciers en externe partners opgesteld om het delen van gevoelige informatie en gedistribueerde en onderling verbonden ICT/OT-producten en -diensten aan te pakken.", false, 34, new Guid("9c3e7a2f-6d41-4b19-ae5f-83f1c728d2b0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a1b2c3d4-e5f6-4a5b-9c8d-7e6f5d4c3b2a"), "ID.AM-2.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9790), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Een inventaris die weergeeft welke softwareplatforms en applicaties in de organisatie worden gebruikt, moet worden gedocumenteerd, herzien en bijgewerkt wanneer zich wijzigingen voordoen.", false, 4, new Guid("e6f5c4d3-a2b1-44c0-b9d8-e7f6a5b4c3d2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a9b8c7d6-e5f4-3a2b-1c0d-9e8f7a6b5c4d"), "ID.AM-3.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9800), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Informatie die de organisatie opslaat en gebruikt, moet worden geïdentificeerd.", false, 8, new Guid("d2c19e8a-4b76-4d3f-9e3c-840a123b5d22"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ae42b790-1fd3-4e88-b7c6-8c4e2d1f9a35"), "ID.RA-5.2", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9860), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De organisatie moet risicobeoordelingen uitvoeren en documenteren waarin het risico wordt bepaald door bedreigingen, kwetsbaarheden, de impact op bedrijfsprocessen en bedrijfsmiddelen en de waarschijnlijkheid dat deze zich voordoen.", false, 28, new Guid("e5f6a1b2-c3d4-e5f6-a1b2-c3d4e5f6a1b2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b19e3a74-2f5c-495e-a4c1-03ea47b6e2d3"), "ID.RA-2.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9860), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Er moet een bewustwordingsprogramma voor bedreigingen en kwetsbaarheden worden geïmplementeerd dat de mogelijkheid omvat om informatie uit te wisselen tussen organisaties.", false, 26, new Guid("d4e5f6a1-b2c3-d4e5-f6a1-b2c3d4e5f6a1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c1d2e3f4-a5b6-c7d8-e9f0-a1b2c3d4e5f6"), "ID.BE-1.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9820), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De rol van de organisatie in de toeleveringsketen moet worden vastgesteld, gedocumenteerd en gecommuniceerd.", false, 13, new Guid("12345678-90ab-cdef-1234-567890abcdef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c4f3e2d1-b0a9-4876-9c8b-1a2b3c4d5e6f"), "ID.AM-1.3", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9790), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wanneer ongeautoriseerde hardware wordt gedetecteerd, wordt deze in quarantaine geplaatst voor mogelijke uitzonderingsbehandeling, verwijderd of vervangen en wordt de inventaris bijgewerkt.", false, 3, new Guid("e6f5c4d3-a2b1-44c0-b9d8-e7f6a5b4c3d2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d3a5f6c2-8b71-4e4f-9c3a-1f25b7e2a6d8"), "ID.RA-1.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9850), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bedreigingen en kwetsbaarheden moeten worden geïdentificeerd.", false, 24, new Guid("c3d4e5f6-a1b2-c3d4-e5f6-a1b2c3d4e5f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e84b7f21-3c9a-4d5e-91af-2d7c3b8a4e90"), "ID.GV-4.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9850), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Als onderdeel van het algehele risicobeheer van het bedrijf moet een alomvattende strategie voor het beheer van informatiebeveiliging en cyberbeveiligingsrisico's worden ontwikkeld en bijgewerkt wanneer zich veranderingen voordoen.", false, 22, new Guid("b2c3d4e5-f6a1-b2c3-d4e5-f6a1b2c3d4e5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f1c8d2e4-3a9b-4d62-95b0-2f7a8c1d6e53"), "ID.RA-5.1", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9860), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De organisatie moet risicobeoordelingen uitvoeren waarbij het risico wordt bepaald door bedreigingen, kwetsbaarheden en de impact op bedrijfsprocessen en bedrijfsmiddelen.", false, 27, new Guid("e5f6a1b2-c3d4-e5f6-a1b2-c3d4e5f6a1b2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f1e2d3c4-b5a6-7987-6543-210fedcba987"), "ID.AM-2.3", new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9800), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "De personen die verantwoordelijk en aansprakelijk zijn voor het beheer van softwareplatforms en applicaties binnen de organisatie moeten worden geïdentificeerd.", false, 6, new Guid("ff81c45e-f75e-4c45-a47c-b5c5055c2161"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Guidances",
                columns: new[] { "Id", "Created", "Deleted", "Description", "IsTitle", "Order", "RequirementId", "Updated" },
                values: new object[,]
                {
                    { new Guid("7f9e1d24-5a3f-4e6a-9b2d-0f1e2c3a4b5c"), new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9890), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deze inventaris moet alle bedrijfsmiddelen bevatten, ongeacht of ze zijn aangesloten op het netwerk van de organisatie.", false, 2, new Guid("5a8e1d9c-7f4b-4c6f-b2e3-1d9a7f6e4c8b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c1a8e3f0-2b3c-4d95-8f66-9a7b12345678"), new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9890), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deze inventaris omvat vaste en draagbare computers, tablets, mobiele telefoons, PLC's (Programmable Logic Controllers), sensoren, actuatoren, robots, bewerkingsmachines, firmware, netwerkschakelaars, routers, voedingen en andere netwerkcomponenten of -apparaten.", false, 1, new Guid("5a8e1d9c-7f4b-4c6f-b2e3-1d9a7f6e4c8b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d7b91c8f-b5a2-4b42-bfeb-395bbaaa89f0"), new DateTime(2025, 5, 23, 15, 58, 22, 613, DateTimeKind.Local).AddTicks(9890), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Het gebruik van een IT asset management tool kan worden overwogen.", false, 3, new Guid("5a8e1d9c-7f4b-4c6f-b2e3-1d9a7f6e4c8b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_ApplicationUserId",
                table: "Assessments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_AssessorId",
                table: "Assessments",
                column: "AssessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_MaturityId",
                table: "Assessments",
                column: "MaturityId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_MunicipalityId",
                table: "Assessments",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_FunctionId",
                table: "Categories",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Guidances_RequirementId",
                table: "Guidances",
                column: "RequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_MaturityLevels_MaturityId",
                table: "MaturityLevels",
                column: "MaturityId");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_SubCategoryId",
                table: "Requirements",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_RequirementId",
                table: "Scores",
                column: "RequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guidances");

            migrationBuilder.DropTable(
                name: "MaturityLevels");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Requirements");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Maturities");

            migrationBuilder.DropTable(
                name: "Municipalities");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Functions");
        }
    }
}
