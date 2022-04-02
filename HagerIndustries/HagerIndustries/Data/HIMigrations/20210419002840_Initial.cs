using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HagerIndustries.Data.HIMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HI");

            migrationBuilder.CreateTable(
                name: "Announcements",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BillingTerms",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TermName = table.Column<string>(maxLength: 50, nullable: false),
                    TermDetails = table.Column<string>(maxLength: 100, nullable: true),
                    SortIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingTerms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SortIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Contractors",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContractName = table.Column<string>(maxLength: 100, nullable: true),
                    ContractDescription = table.Column<string>(maxLength: 500, nullable: true),
                    SortIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    countryName = table.Column<string>(maxLength: 50, nullable: false),
                    countryPostalFormat = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurrencyName = table.Column<string>(maxLength: 50, nullable: false),
                    CurrencySymbol = table.Column<string>(maxLength: 10, nullable: true),
                    SortIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerDescription = table.Column<string>(maxLength: 500, nullable: true),
                    SortIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employments",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmplType = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Start = table.Column<string>(nullable: false),
                    End = table.Column<string>(nullable: false),
                    AllDay = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PosName = table.Column<string>(maxLength: 50, nullable: false),
                    SortIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SkillName = table.Column<string>(maxLength: 100, nullable: false),
                    SortIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VendorName = table.Column<string>(maxLength: 100, nullable: true),
                    VendorDescription = table.Column<string>(maxLength: 500, nullable: true),
                    SortIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    provName = table.Column<string>(maxLength: 50, nullable: false),
                    CountryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "HI",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cityName = table.Column<string>(maxLength: 50, nullable: false),
                    ProvinceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalSchema: "HI",
                        principalTable: "Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    AddressOne = table.Column<string>(maxLength: 100, nullable: true),
                    AddressTwo = table.Column<string>(maxLength: 100, nullable: true),
                    Postal = table.Column<string>(maxLength: 20, nullable: true),
                    CellPhone = table.Column<long>(nullable: true),
                    HomePhone = table.Column<long>(nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    DateJoined = table.Column<DateTime>(nullable: true),
                    Wage = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    Expense = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    KeyFobNumber = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsUser = table.Column<bool>(nullable: false),
                    EmergencyContactName = table.Column<string>(maxLength: 100, nullable: true),
                    EmergencyContactPhone = table.Column<long>(nullable: true),
                    Note = table.Column<string>(maxLength: 255, nullable: true),
                    PositionID = table.Column<int>(nullable: true),
                    EmploymentID = table.Column<int>(nullable: true),
                    CountryID = table.Column<int>(nullable: true),
                    ProvinceID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Countries_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "HI",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Employments_EmploymentID",
                        column: x => x.EmploymentID,
                        principalSchema: "HI",
                        principalTable: "Employments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionID",
                        column: x => x.PositionID,
                        principalSchema: "HI",
                        principalTable: "Positions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalSchema: "HI",
                        principalTable: "Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompName = table.Column<string>(maxLength: 50, nullable: false),
                    CompLocation = table.Column<string>(maxLength: 100, nullable: true),
                    CreditCheck = table.Column<bool>(nullable: false),
                    CreditCheckDate = table.Column<DateTime>(nullable: true),
                    CompPhone = table.Column<long>(nullable: true),
                    CompWebsite = table.Column<string>(maxLength: 100, nullable: true),
                    BillingAddressOne = table.Column<string>(maxLength: 100, nullable: true),
                    BillingAddressTwo = table.Column<string>(maxLength: 100, nullable: true),
                    BillingPostal = table.Column<string>(maxLength: 20, nullable: true),
                    ShippingAddressOne = table.Column<string>(maxLength: 100, nullable: true),
                    ShippingAddressTwo = table.Column<string>(maxLength: 100, nullable: true),
                    ShippingPostal = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(maxLength: 255, nullable: true),
                    BillingCountryID = table.Column<int>(nullable: true),
                    BillingProvinceID = table.Column<int>(nullable: true),
                    BillingCityID = table.Column<int>(nullable: true),
                    ShippingCountryID = table.Column<int>(nullable: true),
                    ShippingProvinceID = table.Column<int>(nullable: true),
                    ShippingCityID = table.Column<int>(nullable: true),
                    CurrencyID = table.Column<int>(nullable: true),
                    BillingTermID = table.Column<int>(nullable: true),
                    IsContractor = table.Column<bool>(nullable: false),
                    IsVendor = table.Column<bool>(nullable: false),
                    IsCustomer = table.Column<bool>(nullable: false),
                    PrimaryCompID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Companies_Cities_BillingCityID",
                        column: x => x.BillingCityID,
                        principalSchema: "HI",
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Countries_BillingCountryID",
                        column: x => x.BillingCountryID,
                        principalSchema: "HI",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Provinces_BillingProvinceID",
                        column: x => x.BillingProvinceID,
                        principalSchema: "HI",
                        principalTable: "Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_BillingTerms_BillingTermID",
                        column: x => x.BillingTermID,
                        principalSchema: "HI",
                        principalTable: "BillingTerms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Currencies_CurrencyID",
                        column: x => x.CurrencyID,
                        principalSchema: "HI",
                        principalTable: "Currencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Cities_ShippingCityID",
                        column: x => x.ShippingCityID,
                        principalSchema: "HI",
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Countries_ShippingCountryID",
                        column: x => x.ShippingCountryID,
                        principalSchema: "HI",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Provinces_ShippingProvinceID",
                        column: x => x.ShippingProvinceID,
                        principalSchema: "HI",
                        principalTable: "Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSkills",
                schema: "HI",
                columns: table => new
                {
                    SkillID = table.Column<int>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSkills", x => new { x.EmployeeID, x.SkillID });
                    table.ForeignKey(
                        name: "FK_EmployeeSkills_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalSchema: "HI",
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSkills_Skills_SkillID",
                        column: x => x.SkillID,
                        principalSchema: "HI",
                        principalTable: "Skills",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyContractors",
                schema: "HI",
                columns: table => new
                {
                    ContractorID = table.Column<int>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContractors", x => new { x.CompanyID, x.ContractorID });
                    table.ForeignKey(
                        name: "FK_CompanyContractors_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "HI",
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyContractors_Contractors_ContractorID",
                        column: x => x.ContractorID,
                        principalSchema: "HI",
                        principalTable: "Contractors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCustomers",
                schema: "HI",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCustomers", x => new { x.CompanyID, x.CustomerID });
                    table.ForeignKey(
                        name: "FK_CompanyCustomers_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "HI",
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyCustomers_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "HI",
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyVendors",
                schema: "HI",
                columns: table => new
                {
                    VendorID = table.Column<int>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyVendors", x => new { x.CompanyID, x.VendorID });
                    table.ForeignKey(
                        name: "FK_CompanyVendors_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "HI",
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyVendors_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalSchema: "HI",
                        principalTable: "Vendors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "HI",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    JobTitle = table.Column<string>(maxLength: 100, nullable: true),
                    CellPhone = table.Column<long>(nullable: true),
                    WorkPhone = table.Column<long>(nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(maxLength: 255, nullable: true),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contacts_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "HI",
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactCategories",
                schema: "HI",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false),
                    ContactID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactCategories", x => new { x.ContactID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_ContactCategories_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalSchema: "HI",
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactCategories_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "HI",
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactContractors",
                schema: "HI",
                columns: table => new
                {
                    ContractorID = table.Column<int>(nullable: false),
                    ContactID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactContractors", x => new { x.ContactID, x.ContractorID });
                    table.ForeignKey(
                        name: "FK_ContactContractors_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "HI",
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactContractors_Contractors_ContractorID",
                        column: x => x.ContractorID,
                        principalSchema: "HI",
                        principalTable: "Contractors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactCustomers",
                schema: "HI",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false),
                    ContactID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactCustomers", x => new { x.ContactID, x.CustomerID });
                    table.ForeignKey(
                        name: "FK_ContactCustomers_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "HI",
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactCustomers_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "HI",
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactVendors",
                schema: "HI",
                columns: table => new
                {
                    VendorID = table.Column<int>(nullable: false),
                    ContactID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactVendors", x => new { x.ContactID, x.VendorID });
                    table.ForeignKey(
                        name: "FK_ContactVendors_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "HI",
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactVendors_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalSchema: "HI",
                        principalTable: "Vendors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingTerms_TermName",
                schema: "HI",
                table: "BillingTerms",
                column: "TermName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                schema: "HI",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceID_cityName",
                schema: "HI",
                table: "Cities",
                columns: new[] { "ProvinceID", "cityName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_BillingCityID",
                schema: "HI",
                table: "Companies",
                column: "BillingCityID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_BillingCountryID",
                schema: "HI",
                table: "Companies",
                column: "BillingCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_BillingProvinceID",
                schema: "HI",
                table: "Companies",
                column: "BillingProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_BillingTermID",
                schema: "HI",
                table: "Companies",
                column: "BillingTermID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompName",
                schema: "HI",
                table: "Companies",
                column: "CompName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CurrencyID",
                schema: "HI",
                table: "Companies",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ShippingCityID",
                schema: "HI",
                table: "Companies",
                column: "ShippingCityID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ShippingCountryID",
                schema: "HI",
                table: "Companies",
                column: "ShippingCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ShippingProvinceID",
                schema: "HI",
                table: "Companies",
                column: "ShippingProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContractors_ContractorID",
                schema: "HI",
                table: "CompanyContractors",
                column: "ContractorID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCustomers_CustomerID",
                schema: "HI",
                table: "CompanyCustomers",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyVendors_VendorID",
                schema: "HI",
                table: "CompanyVendors",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactCategories_CategoryID",
                schema: "HI",
                table: "ContactCategories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactContractors_ContractorID",
                schema: "HI",
                table: "ContactContractors",
                column: "ContractorID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactCustomers_CustomerID",
                schema: "HI",
                table: "ContactCustomers",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CompanyID",
                schema: "HI",
                table: "Contacts",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Email",
                schema: "HI",
                table: "Contacts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_FirstName_LastName",
                schema: "HI",
                table: "Contacts",
                columns: new[] { "FirstName", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactVendors_VendorID",
                schema: "HI",
                table: "ContactVendors",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_countryName",
                schema: "HI",
                table: "Countries",
                column: "countryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_CurrencyName",
                schema: "HI",
                table: "Currencies",
                column: "CurrencyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CountryID",
                schema: "HI",
                table: "Employees",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                schema: "HI",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmploymentID",
                schema: "HI",
                table: "Employees",
                column: "EmploymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_KeyFobNumber",
                schema: "HI",
                table: "Employees",
                column: "KeyFobNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionID",
                schema: "HI",
                table: "Employees",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProvinceID",
                schema: "HI",
                table: "Employees",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FirstName_LastName",
                schema: "HI",
                table: "Employees",
                columns: new[] { "FirstName", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSkills_SkillID",
                schema: "HI",
                table: "EmployeeSkills",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_EmplType",
                schema: "HI",
                table: "Employments",
                column: "EmplType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PosName",
                schema: "HI",
                table: "Positions",
                column: "PosName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryID_provName",
                schema: "HI",
                table: "Provinces",
                columns: new[] { "CountryID", "provName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillName",
                schema: "HI",
                table: "Skills",
                column: "SkillName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "CompanyContractors",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "CompanyCustomers",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "CompanyVendors",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "ContactCategories",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "ContactContractors",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "ContactCustomers",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "ContactVendors",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "EmployeeSkills",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Events",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Contractors",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Vendors",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Skills",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Employments",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Positions",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "BillingTerms",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Provinces",
                schema: "HI");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "HI");
        }
    }
}
