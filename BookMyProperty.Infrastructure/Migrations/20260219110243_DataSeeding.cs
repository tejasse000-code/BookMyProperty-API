using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookMyProperty.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Locations_LocationId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "PropertyImages",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Locations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "ContactInquiries",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ContactInquiries",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "ContactInquiries",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ContactInquiries",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Amenities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Swimming Pool" },
                    { 2, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Gym" },
                    { 3, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Parking" },
                    { 4, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Security" },
                    { 5, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Garden" },
                    { 6, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Balcony" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "City", "Country", "CreatedBy", "CreatedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "State", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Mumbai", "India", null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Maharashtra", "400001" },
                    { 2, "Bangalore", "India", null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Karnataka", "560001" },
                    { 3, "Delhi", "India", null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Delhi", "110001" },
                    { 4, "Pune", "India", null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Maharashtra", "411001" }
                });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Apartment" },
                    { 2, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "House" },
                    { 3, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Villa" },
                    { 4, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Penthouse" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "FirstName", "IsActive", "IsDeleted", "LastName", "ModifiedBy", "ModifiedDate", "PasswordHash", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { 2, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.smith@bookmyproperty.com", "John", true, false, "Smith", null, null, "$2a$11$9fF5Qw1F6qH8nVJX6Vx9bOZqVxUuD9nC2RjF5eTqK8JXk9bVxYwzW", "+919876543210", 2 },
                    { 3, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sarah.johnson@bookmyproperty.com", "Sarah", true, false, "Johnson", null, null, "$2a$11$9fF5Qw1F6qH8nVJX6Vx9bOZqVxUuD9nC2RjF5eTqK8JXk9bVxYwzW", "+918765432109", 2 }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "AgentId", "AreaSqFt", "Bathrooms", "Bedrooms", "CreatedBy", "CreatedDate", "Description", "IsDeleted", "IsFeatured", "LocationId", "ModifiedBy", "ModifiedDate", "Parking", "Price", "PropertyTypeId", "Status", "Title" },
                values: new object[,]
                {
                    { 1, 2, 1500.0, 2, 3, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beautiful 3 BHK apartment with all modern amenities and sea view.", false, true, 1, null, null, 2, 15000000m, 1, "Available", "Modern Apartment in Downtown Mumbai" },
                    { 2, 3, 3500.0, 4, 5, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spacious 5 BHK villa with private pool and garden.", false, true, 2, null, null, 3, 25000000m, 3, "Available", "Luxury Villa in Bangalore" },
                    { 3, 2, 1200.0, 2, 2, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Well-maintained 2 BHK house in a peaceful locality.", false, false, 3, null, null, 1, 8000000m, 2, "Available", "Cozy House in Delhi" },
                    { 4, 3, 2800.0, 3, 4, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ultra-modern penthouse with panoramic city views.", false, true, 4, null, null, 2, 20000000m, 4, "Available", "Premium Penthouse in Pune" },
                    { 5, 2, 600.0, 1, 1, null, new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Affordable 1 BHK apartment near railway station.", false, false, 1, null, null, 0, 4500000m, 1, "Available", "Budget-Friendly Apartment in Mumbai" }
                });

            migrationBuilder.InsertData(
                table: "PropertyAmenities",
                columns: new[] { "AmenityId", "PropertyId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 5, 2 },
                    { 3, 3 },
                    { 4, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 6, 4 }
                });

            migrationBuilder.InsertData(
                table: "PropertyImages",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "ImageUrl", "IsDeleted", "IsPrimary", "ModifiedBy", "ModifiedDate", "PropertyId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://via.placeholder.com/800x600?text=Modern+Apartment+View1", false, true, null, null, 1 },
                    { 2, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://via.placeholder.com/800x600?text=Modern+Apartment+View2", false, false, null, null, 1 },
                    { 3, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://via.placeholder.com/800x600?text=Luxury+Villa+View1", false, true, null, null, 2 },
                    { 4, null, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://via.placeholder.com/800x600?text=Luxury+Villa+View2", false, false, null, null, 2 },
                    { 5, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://via.placeholder.com/800x600?text=House+View1", false, true, null, null, 3 },
                    { 6, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://via.placeholder.com/800x600?text=Penthouse+View1", false, true, null, null, 4 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Locations_LocationId",
                table: "Properties",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Locations_LocationId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties");

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PropertyAmenities",
                keyColumns: new[] { "AmenityId", "PropertyId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PropertyAmenities",
                keyColumns: new[] { "AmenityId", "PropertyId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "PropertyAmenities",
                keyColumns: new[] { "AmenityId", "PropertyId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "PropertyAmenities",
                keyColumns: new[] { "AmenityId", "PropertyId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "PropertyAmenities",
                keyColumns: new[] { "AmenityId", "PropertyId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "PropertyAmenities",
                keyColumns: new[] { "AmenityId", "PropertyId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "PropertyAmenities",
                keyColumns: new[] { "AmenityId", "PropertyId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "PropertyAmenities",
                keyColumns: new[] { "AmenityId", "PropertyId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "PropertyAmenities",
                keyColumns: new[] { "AmenityId", "PropertyId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "PropertyAmenities",
                keyColumns: new[] { "AmenityId", "PropertyId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "PropertyAmenities",
                keyColumns: new[] { "AmenityId", "PropertyId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "PropertyAmenities",
                keyColumns: new[] { "AmenityId", "PropertyId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "PropertyImages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "ContactInquiries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ContactInquiries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "ContactInquiries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ContactInquiries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Amenities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Locations_LocationId",
                table: "Properties",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
