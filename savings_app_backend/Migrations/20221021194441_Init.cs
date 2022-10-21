using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace savings_app_backend.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    buyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pickupTimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pickup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    productId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    startTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pickup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountPerUnit = table.Column<float>(type: "real", nullable: false),
                    AmountOfUnits = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    PictureURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShelfLife = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAuth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuth", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buyer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserAuthId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buyer_UserAuth_UserAuthId",
                        column: x => x.UserAuthId,
                        principalTable: "UserAuth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Open = table.Column<bool>(type: "bit", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAuthId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurant_UserAuth_UserAuthId",
                        column: x => x.UserAuthId,
                        principalTable: "UserAuth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "Status", "buyerId", "pickupTimeId", "productId", "sellerId" },
                values: new object[,]
                {
                    { new Guid("b335acb9-985b-47cb-bae5-eb649f3101f6"), "AwaitingPickup", new Guid("b603db5e-6f2d-4751-b3d3-d1c864db8016"), new Guid("00000000-0000-0000-0000-000000000000"), "fd6ad331-3d86-49d1-a6f7-f4ae85371e97", new Guid("a2e5346e-b246-4578-b5cd-993af7f77d06") },
                    { new Guid("ca5dbb9c-60a8-43c5-9da0-a3f0d24e5108"), "AwaitingPickup", new Guid("b603db5e-6f2d-4751-b3d3-d1c864db8016"), new Guid("00000000-0000-0000-0000-000000000000"), "fd6ad331-3d86-49d1-a6f7-f4ae85371e97", new Guid("a2e5346e-b246-4578-b5cd-993af7f77d06") },
                    { new Guid("ca5dbb9c-60a8-43c5-9da0-a3f0d24e5120"), "AwaitingPickup", new Guid("ca5dbb9c-60a8-43c5-9da0-a3f0d24e5108"), new Guid("00000000-0000-0000-0000-000000000000"), "fd6ad331-3d86-49d1-a6f7-f4ae85371e97", new Guid("a2e5346e-b246-4578-b5cd-993af7f77d06") },
                    { new Guid("fd6ad331-3d86-49d1-a6f7-f4ae85371e97"), "AwaitingPickup", new Guid("b335acb9-985b-47cb-bae5-eb649f3101f6"), new Guid("00000000-0000-0000-0000-000000000000"), "fd6ad331-3d86-49d1-a6f7-f4ae85371e97", new Guid("a2e5346e-b246-4578-b5cd-993af7f77d06") }
                });

            migrationBuilder.InsertData(
                table: "Pickup",
                columns: new[] { "Id", "endTime", "productId", "startTime", "status" },
                values: new object[,]
                {
                    { new Guid("168cba18-9541-4e71-8e5f-4c78be6a7c2c"), new DateTime(2017, 9, 8, 19, 1, 55, 0, DateTimeKind.Unspecified), new Guid("1163fde0-8dae-458f-8e25-860dda25dd29"), new DateTime(2017, 9, 8, 19, 1, 45, 0, DateTimeKind.Unspecified), "Available" },
                    { new Guid("39392fad-a761-45c1-89b9-3d26f6ac96b3"), new DateTime(2017, 9, 8, 19, 1, 55, 0, DateTimeKind.Unspecified), new Guid("42fd75ec-1d4d-40ee-ae0a-e3ab79c69f02"), new DateTime(2017, 9, 8, 19, 1, 45, 0, DateTimeKind.Unspecified), "Available" },
                    { new Guid("39392fad-a761-45c1-89b9-3d26f6ac96e2"), new DateTime(2017, 9, 8, 19, 1, 55, 0, DateTimeKind.Unspecified), new Guid("42fd75ec-1d4d-40ee-ae0a-e3ab79c69f02"), new DateTime(2017, 9, 8, 19, 1, 45, 0, DateTimeKind.Unspecified), "Available" },
                    { new Guid("39392fad-a761-45c1-89b9-3d26f6ac96f1"), new DateTime(2017, 9, 8, 19, 1, 55, 0, DateTimeKind.Unspecified), new Guid("db7d2e61-077d-40ca-a888-9e05e59dce83"), new DateTime(2017, 9, 8, 19, 1, 45, 0, DateTimeKind.Unspecified), "Available" },
                    { new Guid("42fd75ec-1d4d-40ee-ae0a-e3ab79c69f02"), new DateTime(2017, 9, 8, 19, 1, 55, 0, DateTimeKind.Unspecified), new Guid("1163fde0-8dae-458f-8e25-860dda25dd29"), new DateTime(2017, 9, 8, 19, 1, 45, 0, DateTimeKind.Unspecified), "Available" },
                    { new Guid("a2e5346e-b246-4578-b5cd-993af7f77d06"), new DateTime(2017, 9, 8, 19, 1, 55, 0, DateTimeKind.Unspecified), new Guid("168cba18-9541-4e71-8e5f-4c78be6a7c2c"), new DateTime(2017, 9, 8, 19, 1, 45, 0, DateTimeKind.Unspecified), "Available" },
                    { new Guid("fd6ad331-3d86-49d1-a6f7-f4ae85371e97"), new DateTime(2017, 9, 8, 19, 1, 55, 0, DateTimeKind.Unspecified), new Guid("db7d2e61-077d-40ca-a888-9e05e59dce83"), new DateTime(2017, 9, 8, 19, 1, 45, 0, DateTimeKind.Unspecified), "Available" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "AmountOfUnits", "AmountPerUnit", "AmountType", "Category", "Description", "Name", "PictureURL", "Price", "RestaurantID", "ShelfLife" },
                values: new object[,]
                {
                    { new Guid("1163fde0-8dae-458f-8e25-860dda25dd29"), 5, 12f, "unit", "Protein", ".....", "eggs", "https://images.pexels.com/photos/2487443/pexels-photo-2487443.jpeg?cs=srgb&dl=pexels-matheus-cenali-2487443.jpg&fm=jpg", 2.2f, new Guid("fd6ad331-3d86-49d1-a6f7-f4ae85371e97"), new DateTime(2017, 9, 8, 19, 1, 55, 714, DateTimeKind.Local).AddTicks(9420) },
                    { new Guid("168cba18-9541-4e71-8e5f-4c78be6a7c2c"), 13, 0.2f, "kilogram", "Snack", ".....", "french-fries", "https://images.pexels.com/photos/2487443/pexels-photo-2487443.jpeg?cs=srgb&dl=pexels-matheus-cenali-2487443.jpg&fm=jpg", 0.5f, new Guid("168cba18-9541-4e71-8e5f-4c78be6a7c2c"), new DateTime(2017, 9, 8, 19, 1, 55, 714, DateTimeKind.Local).AddTicks(9420) },
                    { new Guid("42fd75ec-1d4d-40ee-ae0a-e3ab79c69f02"), 9, 2f, "kilogram", "Fruit", ".....", "apples", "https://images.pexels.com/photos/2487443/pexels-photo-2487443.jpeg?cs=srgb&dl=pexels-matheus-cenali-2487443.jpg&fm=jpg", 3.2f, new Guid("fd6ad331-3d86-49d1-a6f7-f4ae85371e97"), new DateTime(2017, 9, 8, 19, 1, 55, 714, DateTimeKind.Local).AddTicks(9420) },
                    { new Guid("a2e5346e-b246-4578-b5cd-993af7f77d06"), 2, 1f, "kilogram", "Protein", "asdas", "Burger patties", "https://media.istockphoto.com/photos/stack-of-fresh-raw-burger-patty-picture-id1268023262?k=20&m=1268023262&s=612x612&w=0&h=aLMpdDBCzc31AJPfxZOFFG90HvdaZso0fpHmaJf7fw0=", 5f, new Guid("39392fad-a761-45c1-89b9-3d26f6ac96e2"), new DateTime(2017, 9, 8, 19, 1, 55, 714, DateTimeKind.Local).AddTicks(9420) },
                    { new Guid("db7d2e61-077d-40ca-a888-9e05e59dce83"), 5, 0.5f, "litre", "Dairy", ",,,,", "milkas", "linkas", 0.8f, new Guid("42fd75ec-1d4d-40ee-ae0a-e3ab79c69f02"), new DateTime(2017, 9, 8, 19, 1, 55, 714, DateTimeKind.Local).AddTicks(9420) },
                    { new Guid("fd6ad331-3d86-49d1-a6f7-f4ae85371e97"), 5, 0.5f, "kilogram", "Vegetable", ".....", "potatoes", "https://media.istockphoto.com/photos/three-potatoes-picture-id157430678?k=20&m=157430678&s=612x612&w=0&h=dfYLuPYwA50ojI90hZ4jCgKZd5TGnqf24UCVBszoZIA=", 1.5f, new Guid("a2e5346e-b246-4578-b5cd-993af7f77d06"), new DateTime(2017, 9, 8, 19, 1, 55, 714, DateTimeKind.Local).AddTicks(9420) }
                });

            migrationBuilder.InsertData(
                table: "UserAuth",
                columns: new[] { "Id", "Email", "Password" },
                values: new object[,]
                {
                    { new Guid("1163fde0-8dae-458f-8e25-860dda25dd29"), "asdasd@gmail.com", "aadsadas" },
                    { new Guid("168cba18-9541-4e71-8e5f-4c78be6a7c2c"), "kazkas@email.com", "password" },
                    { new Guid("39392fad-a761-45c1-89b9-3d26f6ac96e2"), "kazkascia@email.com", "password" },
                    { new Guid("42fd75ec-1d4d-40ee-ae0a-e3ab79c69f02"), "asdasd@gmail.com", "a" },
                    { new Guid("42fd75ec-1d4d-40ee-ae0a-e3ab79c69f33"), "email@email.com", "password" },
                    { new Guid("7b3ed2a5-4257-48a8-ae1a-13da39e343d8"), "asaasddf@gmail.com", "klkl@1KL" },
                    { new Guid("8b2ea1b7-12f8-4afc-9064-25181379ae30"), "asaasddddf@gmail.com", "klkl@1KL" },
                    { new Guid("91227b09-b2a6-474d-a396-acb0a8a39f1b"), "hello@gmail.com", "Hello@1458" },
                    { new Guid("a2e5346e-b246-4578-b5cd-993af7f77d06"), "kaz@kas@email.com", "password" },
                    { new Guid("b335acb9-985b-47cb-bae5-eb649f3101f6"), "User1@gmail.com", "USER" },
                    { new Guid("b603db5e-6f2d-4751-b3d3-d1c864db8016"), "aaaaa@gmail.com", "aaaaaaa" },
                    { new Guid("ca5dbb9c-60a8-43c5-9da0-a3f0d24e5108"), "bbb@gmail.com", "bbbbbbbbb" },
                    { new Guid("fd6ad331-3d86-49d1-a6f7-f4ae85371e97"), "TestRest@mail.ru", "qwert" }
                });

            migrationBuilder.InsertData(
                table: "Restaurant",
                columns: new[] { "Id", "Description", "Name", "Open", "Picture", "Rating", "ShortDescription", "SiteRef", "UserAuthId" },
                values: new object[,]
                {
                    { new Guid("168cba18-9541-4e71-8e5f-4c78be6a7c2c"), "descriptiondescr iptiondescriptiond escriptiondes criptiondescript iondescriptiondescript iondescriptio ndescriptiondescription", "Cafe Bilhanes", true, null, 4.5, "______________________________", "???", new Guid("fd6ad331-3d86-49d1-a6f7-f4ae85371e97") },
                    { new Guid("39392fad-a761-45c1-89b9-3d26f6ac96e2"), "descriptiondescr iptiondescriptiond escriptiondes criptiondescript iondescriptiondescript iondescriptio ndescriptiondescription", "Corner Bistro", true, null, 4.5, "______________________________", "???", new Guid("42fd75ec-1d4d-40ee-ae0a-e3ab79c69f02") },
                    { new Guid("42fd75ec-1d4d-40ee-ae0a-e3ab79c69f02"), "descriptiondescr iptiondescriptiond escriptiondes criptiondescript iondescriptiondescript iondescriptio ndescriptiondescription", "Local", true, null, 4.5, "______________________________", "???", new Guid("b335acb9-985b-47cb-bae5-eb649f3101f6") },
                    { new Guid("a2e5346e-b246-4578-b5cd-993af7f77d05"), "descriptiondescr iptiondescriptiond escriptiondes criptiondescript iondescriptiondescript iondescriptio ndescriptiondescription", "Ba2rca", true, null, 4.0999999999999996, "______________________________", "???", new Guid("1163fde0-8dae-458f-8e25-860dda25dd29") },
                    { new Guid("a2e5346e-b246-4578-b5cd-993af7f77d06"), "descriptiondescr iptiondescriptiond escriptiondes criptiondescript iondescriptiondescript iondescriptio ndescriptiondescription", "Huye Magoos Fender", true, null, 4.5, "______________________________", "???", new Guid("b603db5e-6f2d-4751-b3d3-d1c864db8016") },
                    { new Guid("fd6ad331-3d86-49d1-a6f7-f4ae85371e97"), "descriptiondescr iptiondescriptiond escriptiondes criptiondescript iondescriptiondescript iondescriptio ndescriptiondescription", "Barca", true, null, 3.0, "______________________________", "???", new Guid("ca5dbb9c-60a8-43c5-9da0-a3f0d24e5108") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buyer_UserAuthId",
                table: "Buyer",
                column: "UserAuthId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_UserAuthId",
                table: "Restaurant",
                column: "UserAuthId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buyer");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Pickup");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Restaurant");

            migrationBuilder.DropTable(
                name: "UserAuth");
        }
    }
}
