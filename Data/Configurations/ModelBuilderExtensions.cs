
using Entities;
using Entities.UserEntities.SiteUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public static class ModelBuilderExtensions
    {
        //Seed data

        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Product>().HasData
                (
                new Product()
                {
                    Id = 1,
                    Name = "Çim Biçme Makinesi",
                    ProductUrl = "cim-bicme-makinesi",
                    //BaseDolarPrice = 10,
                    BasePrice = 10,
                    SalePrice = 200,
                    Quantity = 15,
                    //ImageUrl = "1.jpg",
                    MainImageUrl = "1.jpg",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi non nunc ac risus consequat imperdiet malesuada quis diam. Sed augue eros, tempor sed tempus sed, venenatis ut odio. Aenean sagittis, lorem ac tristique ultrices, purus tellus sollicitudin purus, eget bibendum massa orci ut magna. Etiam vitae efficitur orci. Pellentesque pulvinar risus eget metus dictum accumsan. In pulvinar faucibus nibh, nec congue ipsum rhoncus at. Fusce vel fermentum lectus, id rutrum mi. Nullam ornare nisl ex, vitae pulvinar tortor viverra a. Vestibulum egestas",
                    CardDescription = "iyi bir çim biçme makinesi",
                    IsApproved = true,
                    IsSuggested = true,
                    StockState = StockState.available,
                    BasePriceType = BasePriceType.Dollar
                },
                new Product()
                {
                    Id = 2,
                    Name = "Tırpan",
                    ProductUrl = "tirpan",
                    //BaseDolarPrice = 5,
                    BasePrice = 5,
                    SalePrice = 100,
                    Quantity = 0,
                    //ImageUrl = "2.jpg",
                    MainImageUrl = "5.jpg",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi non nunc ac risus consequat imperdiet malesuada quis diam. Sed augue eros, tempor sed tempus sed, venenatis ut odio. Aenean sagittis, lorem ac tristique ultrices, purus tellus sollicitudin purus, eget bibendum massa orci ut magna. Etiam vitae efficitur orci. Pellentesque pulvinar risus eget metus dictum accumsan. In pulvinar faucibus nibh, nec congue ipsum rhoncus at. Fusce vel fermentum lectus, id rutrum mi. Nullam ornare nisl ex, vitae pulvinar tortor viverra a. Vestibulum egestas",
                    CardDescription = "İyi bir tırpan",
                    IsApproved = true,
                    IsSuggested = false,
                    StockState = StockState.notAvailable,
                    BasePriceType = BasePriceType.Euro
                },
                new Product()
                {
                    Id = 3,
                    Name = "Misina",
                    ProductUrl = "misina",
                    //BaseDolarPrice = 2,
                    BasePrice = 2,
                    SalePrice = 40,
                    Quantity = 200,
                    //ImageUrl = "3.jpg",
                    MainImageUrl = "7.jpg",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi non nunc ac risus consequat imperdiet malesuada quis diam. Sed augue eros, tempor sed tempus sed, venenatis ut odio. Aenean sagittis, lorem ac tristique ultrices, purus tellus sollicitudin purus, eget bibendum massa orci ut magna. Etiam vitae efficitur orci. Pellentesque pulvinar risus eget metus dictum accumsan. In pulvinar faucibus nibh, nec congue ipsum rhoncus at. Fusce vel fermentum lectus, id rutrum mi. Nullam ornare nisl ex, vitae pulvinar tortor viverra a. Vestibulum egestas",
                    CardDescription = "İyi bir misina",
                    IsApproved = true,
                    IsSuggested = true,
                    StockState = StockState.available,
                    BasePriceType = BasePriceType.DontChange
                },
                new Product()
                {
                    Id = 4,
                    Name = "Çim Biçme Traktörü",
                    ProductUrl = "cim-bicme-traktoru",
                    //BaseDolarPrice = 50,
                    BasePrice = 50,
                    SalePrice = 1000,
                    Quantity = 0,
                    //ImageUrl = "4.jpg",
                    MainImageUrl = "3.jpg",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi non nunc ac risus consequat imperdiet malesuada quis diam. Sed augue eros, tempor sed tempus sed, venenatis ut odio. Aenean sagittis, lorem ac tristique ultrices, purus tellus sollicitudin purus, eget bibendum massa orci ut magna. Etiam vitae efficitur orci. Pellentesque pulvinar risus eget metus dictum accumsan. In pulvinar faucibus nibh, nec congue ipsum rhoncus at. Fusce vel fermentum lectus, id rutrum mi. Nullam ornare nisl ex, vitae pulvinar tortor viverra a. Vestibulum egestas",
                    CardDescription = "iyi bir çim biçme traktörü",
                    IsApproved = true,
                    IsSuggested = true,
                    StockState = StockState.onOrder,
                    BasePriceType = BasePriceType.Dollar
                },
                new Product()
                {
                    Id = 5,
                    Name = "silindir piston",
                    ProductUrl = "silindir-piston",
                    //BaseDolarPrice = 4,
                    BasePrice = 4,
                    SalePrice = 80,
                    Quantity = 30,
                    //ImageUrl = "5.jpg",
                    MainImageUrl = "1.jpg",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi non nunc ac risus consequat imperdiet malesuada quis diam. Sed augue eros, tempor sed tempus sed, venenatis ut odio. Aenean sagittis, lorem ac tristique ultrices, purus tellus sollicitudin purus, eget bibendum massa orci ut magna. Etiam vitae efficitur orci. Pellentesque pulvinar risus eget metus dictum accumsan. In pulvinar faucibus nibh, nec congue ipsum rhoncus at. Fusce vel fermentum lectus, id rutrum mi. Nullam ornare nisl ex, vitae pulvinar tortor viverra a. Vestibulum egestas",
                    CardDescription = "iyi bir silindir piston",
                    IsApproved = false,
                    IsSuggested = true,
                    StockState = StockState.available,
                    BasePriceType = BasePriceType.Euro
                },
                new Product()
                {
                    Id = 6,
                    Name = "karbüratör",
                    ProductUrl = "karburator",
                    //BaseDolarPrice = 3,
                    BasePrice = 3,
                    SalePrice = 40,
                    Quantity = 20,
                    //ImageUrl = "6.jpg",
                    MainImageUrl = "7.jpg",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi non nunc ac risus consequat imperdiet malesuada quis diam. Sed augue eros, tempor sed tempus sed, venenatis ut odio. Aenean sagittis, lorem ac tristique ultrices, purus tellus sollicitudin purus, eget bibendum massa orci ut magna. Etiam vitae efficitur orci. Pellentesque pulvinar risus eget metus dictum accumsan. In pulvinar faucibus nibh, nec congue ipsum rhoncus at. Fusce vel fermentum lectus, id rutrum mi. Nullam ornare nisl ex, vitae pulvinar tortor viverra a. Vestibulum egestas",
                    CardDescription = "iyi bir karbüratör",
                    IsApproved = true,
                    IsSuggested = false,
                    StockState = StockState.available,
                    BasePriceType = BasePriceType.Euro
                }
                );

            builder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Çim Biçme Makineleri", CategoryUrl = "cim-bicme-makineleri" },
                new Category() { Id = 2, Name = "Tırpan", CategoryUrl = "tirpan" },
                new Category() { Id = 3, Name = "Yedek Parça", CategoryUrl = "yedek-parca" },
                new Category() { Id = 4, Name = "Yan Ürünler", CategoryUrl = "yan-urunler" },
                new Category() { Id = 5, Name = "Çim Biçme Traktörleri", CategoryUrl = "cim-bicme-traktoru" }
                );

            builder.Entity<ProductCategory>().HasData(
                new ProductCategory() { ProductId = 1, CategoryId = 1 },
                new ProductCategory() { ProductId = 5, CategoryId = 3 },
                new ProductCategory() { ProductId = 6, CategoryId = 3 },
                new ProductCategory() { ProductId = 2, CategoryId = 2 },
                new ProductCategory() { ProductId = 3, CategoryId = 4 },
                new ProductCategory() { ProductId = 4, CategoryId = 5 }
                );

            builder.Entity<Image>().HasData(
                new Image() {Id = 1, Url = "1.jpg", IsHomeDisplay = false, IsMainImageOfProduct = true },
                new Image() {Id = 2, Url = "2.jpg", IsHomeDisplay = false, IsMainImageOfProduct = false },
                new Image() {Id = 3, Url = "3.jpg", IsHomeDisplay = false, IsMainImageOfProduct = false },
                new Image() {Id = 4, Url = "4.jpg", IsHomeDisplay = false, IsMainImageOfProduct = false },
                new Image() {Id = 5, Url = "5.jpg", IsHomeDisplay = false, IsMainImageOfProduct = true },
                new Image() {Id = 6, Url = "6.jpg", IsHomeDisplay = false, IsMainImageOfProduct = false },
                new Image() {Id = 7, Url = "7.jpg", IsHomeDisplay = false, IsMainImageOfProduct = true },
                new Image() {Id = 8, Url = "example1.png", IsHomeDisplay = true, IsMainImageOfProduct = false },
                new Image() {Id = 9, Url = "example2.png", IsHomeDisplay = true, IsMainImageOfProduct = false },
                new Image() {Id = 10, Url = "example3.png", IsHomeDisplay = true, IsMainImageOfProduct = false },
                new Image() {Id = 11, Url = "example4.png", IsHomeDisplay = true, IsMainImageOfProduct = false }
                );

            builder.Entity<ProductImage>().HasData(
                new ProductImage() { ProductId = 1, ImageId = 1},
                new ProductImage() { ProductId = 1, ImageId = 2},
                new ProductImage() { ProductId = 1, ImageId = 3},
                new ProductImage() { ProductId = 2, ImageId = 5},
                new ProductImage() { ProductId = 2, ImageId = 6},
                new ProductImage() { ProductId = 3, ImageId = 7},
                new ProductImage() { ProductId = 4, ImageId = 4},
                new ProductImage() { ProductId = 4, ImageId = 3},
                new ProductImage() { ProductId = 5, ImageId = 1},
                new ProductImage() { ProductId = 6, ImageId = 5},
                new ProductImage() { ProductId = 6, ImageId = 6},
                new ProductImage() { ProductId = 6, ImageId = 7},
                new ProductImage() { ProductId = 6, ImageId = 2}
                );
        }
    }
}
