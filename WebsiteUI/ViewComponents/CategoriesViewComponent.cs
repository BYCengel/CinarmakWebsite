using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using WebsiteUI.FunctionsLibrary;
using WebsiteUI.Models;

namespace WebsiteUI.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private ICategoryService _categoryService;

        private AssistantFunctions helperLib = new AssistantFunctions();

        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (RouteData.Values["category"] != null)
            {
                ViewBag.SelectedCategory = RouteData?.Values["category"];
            }
            var categories = await _categoryService.GetAllAsync();
            var listModel = new CategoryListModel();

            foreach (var category in categories)
            {
                listModel.categoryModels.Add(helperLib.CreateCategoryModel(category));
            }
            return View(listModel);
        }
    }
}

