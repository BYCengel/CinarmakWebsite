using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations.Shop
{
    public partial class CartsOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHomeDisplay = table.Column<bool>(type: "bit", nullable: false),
                    IsMainImageOfProduct = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderState = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePrice = table.Column<double>(type: "float", nullable: false),
                    BasePriceType = table.Column<int>(type: "int", nullable: false),
                    SalePrice = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    PurchaseCount = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsSuggested = table.Column<bool>(type: "bit", nullable: false),
                    IsPopular = table.Column<bool>(type: "bit", nullable: false),
                    IsAltered = table.Column<bool>(type: "bit", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    StockState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_Id",
                        column: x => x.Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.CategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => new { x.ProductId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_ProductImage_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductImage_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryUrl", "Name", "ViewCount" },
                values: new object[,]
                {
                    { 1, "cim-bicme-makineleri", "Çim Biçme Makineleri", 0 },
                    { 2, "tirpan", "Tırpan", 0 },
                    { 3, "yedek-parca", "Yedek Parça", 0 },
                    { 4, "yan-urunler", "Yan Ürünler", 0 },
                    { 5, "cim-bicme-traktoru", "Çim Biçme Traktörleri", 0 }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "IsHomeDisplay", "IsMainImageOfProduct", "Url" },
                values: new object[,]
                {
                    { 1, false, true, "1.jpg" },
                    { 2, false, false, "2.jpg" },
                    { 3, false, false, "3.jpg" },
                    { 4, false, false, "4.jpg" },
                    { 5, false, true, "5.jpg" },
                    { 6, false, false, "6.jpg" },
                    { 7, false, true, "7.jpg" },
                    { 8, true, false, "example1.png" },
                    { 9, true, false, "example2.png" },
                    { 10, true, false, "example3.png" },
                    { 11, true, false, "example4.png" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BasePrice", "BasePriceType", "CardDescription", "Description", "IsAltered", "IsApproved", "IsPopular", "IsSuggested", "MainImageUrl", "Name", "ProductUrl", "PurchaseCount", "Quantity", "SalePrice", "StockState", "ViewCount" },
                values: new object[,]
                {
                    { 1, 10.0, 1, "iyi bir çim biçme makinesi", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi non nunc ac risus consequat imperdiet malesuada quis diam. Sed augue eros, tempor sed tempus sed, venenatis ut odio. Aenean sagittis, lorem ac tristique ultrices, purus tellus sollicitudin purus, eget bibendum massa orci ut magna. Etiam vitae efficitur orci. Pellentesque pulvinar risus eget metus dictum accumsan. In pulvinar faucibus nibh, nec congue ipsum rhoncus at. Fusce vel fermentum lectus, id rutrum mi. Nullam ornare nisl ex, vitae pulvinar tortor viverra a. Vestibulum egestas", false, true, false, true, "1.jpg", "Çim Biçme Makinesi", "cim-bicme-makinesi", 0, 15, 200.0, 1, 0 },
                    { 2, 5.0, 2, "İyi bir tırpan", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi non nunc ac risus consequat imperdiet malesuada quis diam. Sed augue eros, tempor sed tempus sed, venenatis ut odio. Aenean sagittis, lorem ac tristique ultrices, purus tellus sollicitudin purus, eget bibendum massa orci ut magna. Etiam vitae efficitur orci. Pellentesque pulvinar risus eget metus dictum accumsan. In pulvinar faucibus nibh, nec congue ipsum rhoncus at. Fusce vel fermentum lectus, id rutrum mi. Nullam ornare nisl ex, vitae pulvinar tortor viverra a. Vestibulum egestas", false, true, false, false, "5.jpg", "Tırpan", "tirpan", 0, 0, 100.0, 3, 0 },
                    { 3, 2.0, -1, "İyi bir misina", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi non nunc ac risus consequat imperdiet malesuada quis diam. Sed augue eros, tempor sed tempus sed, venenatis ut odio. Aenean sagittis, lorem ac tristique ultrices, purus tellus sollicitudin purus, eget bibendum massa orci ut magna. Etiam vitae efficitur orci. Pellentesque pulvinar risus eget metus dictum accumsan. In pulvinar faucibus nibh, nec congue ipsum rhoncus at. Fusce vel fermentum lectus, id rutrum mi. Nullam ornare nisl ex, vitae pulvinar tortor viverra a. Vestibulum egestas", false, true, false, true, "7.jpg", "Misina", "misina", 0, 200, 40.0, 1, 0 },
                    { 4, 50.0, 1, "iyi bir çim biçme traktörü", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi non nunc ac risus consequat imperdiet malesuada quis diam. Sed augue eros, tempor sed tempus sed, venenatis ut odio. Aenean sagittis, lorem ac tristique ultrices, purus tellus sollicitudin purus, eget bibendum massa orci ut magna. Etiam vitae efficitur orci. Pellentesque pulvinar risus eget metus dictum accumsan. In pulvinar faucibus nibh, nec congue ipsum rhoncus at. Fusce vel fermentum lectus, id rutrum mi. Nullam ornare nisl ex, vitae pulvinar tortor viverra a. Vestibulum egestas", false, true, false, true, "3.jpg", "Çim Biçme Traktörü", "cim-bicme-traktoru", 0, 0, 1000.0, 2, 0 },
                    { 5, 4.0, 2, "iyi bir silindir piston", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi non nunc ac risus consequat imperdiet malesuada quis diam. Sed augue eros, tempor sed tempus sed, venenatis ut odio. Aenean sagittis, lorem ac tristique ultrices, purus tellus sollicitudin purus, eget bibendum massa orci ut magna. Etiam vitae efficitur orci. Pellentesque pulvinar risus eget metus dictum accumsan. In pulvinar faucibus nibh, nec congue ipsum rhoncus at. Fusce vel fermentum lectus, id rutrum mi. Nullam ornare nisl ex, vitae pulvinar tortor viverra a. Vestibulum egestas", false, false, false, true, "1.jpg", "silindir piston", "silindir-piston", 0, 30, 80.0, 1, 0 },
                    { 6, 3.0, 2, "iyi bir karbüratör", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi non nunc ac risus consequat imperdiet malesuada quis diam. Sed augue eros, tempor sed tempus sed, venenatis ut odio. Aenean sagittis, lorem ac tristique ultrices, purus tellus sollicitudin purus, eget bibendum massa orci ut magna. Etiam vitae efficitur orci. Pellentesque pulvinar risus eget metus dictum accumsan. In pulvinar faucibus nibh, nec congue ipsum rhoncus at. Fusce vel fermentum lectus, id rutrum mi. Nullam ornare nisl ex, vitae pulvinar tortor viverra a. Vestibulum egestas", false, true, false, false, "7.jpg", "karbüratör", "karburator", 0, 20, 40.0, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 5 },
                    { 3, 6 },
                    { 4, 3 },
                    { 5, 4 }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "ImageId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 5, 2 },
                    { 6, 2 },
                    { 7, 3 },
                    { 3, 4 },
                    { 4, 4 },
                    { 1, 5 },
                    { 2, 6 },
                    { 5, 6 },
                    { 6, 6 },
                    { 7, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductId",
                table: "ProductCategory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ImageId",
                table: "ProductImage",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
