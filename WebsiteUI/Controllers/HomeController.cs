using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebsiteUI.FunctionsLibrary;
using WebsiteUI.Models;
using WebsiteUI.ViewModels;

namespace WebsiteUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductService _productService;
        private IImageService _imageService;
        private AssistantFunctions helperLib = new AssistantFunctions();

        public HomeController(ILogger<HomeController> logger, IProductService productService, IImageService imageService)
        {
            _productService = productService;
            _logger = logger;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            HomePageViewModel viewModel = new HomePageViewModel();

            List<Product> popularProducts = await _productService.GetPopularProductsAsync();
            List<Product> suggestedProducts = await _productService.GetSuggestedProductsAsync();
            List<Image> homeDisplayImages = await _imageService.GetHomeDisplayImagesAsync();

            foreach (var product in popularProducts)
            {
                var model = (helperLib.CreateProductModel(product));
                model.ImageModels.AddRange(helperLib.CreateImageModels(_productService.GetProductImages(product.Id)));
                viewModel.PopularProductModels.Add(model);
            }

            foreach (var product in suggestedProducts)
            {
                var model = (helperLib.CreateProductModel(product));
                model.ImageModels.AddRange(helperLib.CreateImageModels(_productService.GetProductImages(product.Id)));
                viewModel.SuggestedProductModels.Add(model);
            }

            foreach (var image in homeDisplayImages)
            {
                viewModel.HomeDisplayImageModels.Add(helperLib.CreateImageModel(image));
            }

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}