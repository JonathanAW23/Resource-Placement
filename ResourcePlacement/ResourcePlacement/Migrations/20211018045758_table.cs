using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcePlacement.Migrations
{
    public partial class table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_m_jobs_tb_m_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tb_m_companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    EmploymentStatus = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_m_employees_tb_m_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "tb_m_departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_accounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_m_accounts_tb_m_employees_Id",
                        column: x => x.Id,
                        principalTable: "tb_m_employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_job_histories",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_job_histories", x => new { x.EmployeeId, x.JobId });
                    table.ForeignKey(
                        name: "FK_tb_tr_job_histories_tb_m_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tb_m_employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_job_histories_tb_m_jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "tb_m_jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_jobs_employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InterviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InterviewTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interviewer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterviewResult = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_jobs_employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_tr_jobs_employees_tb_m_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tb_m_employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_jobs_employees_tb_m_jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "tb_m_jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_accounts_roles",
                columns: table => new
                {
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_accounts_roles", x => new { x.AccountId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tb_tr_accounts_roles_tb_m_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "tb_m_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_accounts_roles_tb_m_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_m_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employees_DepartmentId",
                table: "tb_m_employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_jobs_CompanyId",
                table: "tb_m_jobs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_accounts_roles_RoleId",
                table: "tb_tr_accounts_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_job_histories_JobId",
                table: "tb_tr_job_histories",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_jobs_employees_EmployeeId",
                table: "tb_tr_jobs_employees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_jobs_employees_JobId",
                table: "tb_tr_jobs_employees",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_tr_accounts_roles");

            migrationBuilder.DropTable(
                name: "tb_tr_job_histories");

            migrationBuilder.DropTable(
                name: "tb_tr_jobs_employees");

            migrationBuilder.DropTable(
                name: "tb_m_accounts");

            migrationBuilder.DropTable(
                name: "tb_m_roles");

            migrationBuilder.DropTable(
                name: "tb_m_jobs");

            migrationBuilder.DropTable(
                name: "tb_m_employees");

            migrationBuilder.DropTable(
                name: "tb_m_companies");

            migrationBuilder.DropTable(
                name: "tb_m_departments");
        }
    }
}
