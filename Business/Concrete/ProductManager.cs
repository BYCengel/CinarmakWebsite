using Business.Abstract;
using Data.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    /*
     - Check if a product has no images 
     */
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Create(Product entity)
        {
            if (Validation(entity))
            {
                _unitOfWork.ProductRepository.Create(entity);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public void Delete(Product entity)
        {
            _unitOfWork.ProductRepository.Delete(entity);
            _unitOfWork.Save();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            Console.WriteLine("in product manager");
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task<Product> GetByIdWithCategoriesAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdWithCategoriesAsync(id);
        }

        public int GetCountByCategory(string categoryUrl)
        {
            return _unitOfWork.ProductRepository.GetCountByCategory(categoryUrl);
        }

        public Product GetProductDetails(string productUrl)
        {
            return _unitOfWork.ProductRepository.GetProductDetails(productUrl);
        }

        public List<Image> GetProductImages(int productId)
        {
            return _unitOfWork.ProductRepository.GetProductImages(productId);
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(string categoryUrl, int pageSize, int page, bool isAdmin)
        {
            return await _unitOfWork.ProductRepository.GetProductsByCategoryAsync(categoryUrl, pageSize, page, isAdmin);
        }

        public async Task<List<Product>> GetSearchResultsAsync(string searchString)
        {
            return await _unitOfWork.ProductRepository.GetSearchResultsAsync(searchString);
        }

        public async Task<List<Product>> GetPopularProductsAsync()
        {
            return await _unitOfWork.ProductRepository.GetPopularProductsAsync();
        }

        public async Task<List<Product>> GetSuggestedProductsAsync()
        {
            return await _unitOfWork.ProductRepository.GetSuggestedProductsAsync();
        }

        public void IncreaseProductViewCount(string productUrl)
        {
            _unitOfWork.ProductRepository.IncreaseProductViewCount(productUrl);
            _unitOfWork.Save();
        }

        public bool Update(Product entity, int[] categoryIds)
        {
            _unitOfWork.ProductRepository.Update(entity, categoryIds);
            _unitOfWork.Save();
            return true;
        }

        public bool Update(Product entity)
        {
            _unitOfWork.ProductRepository.Update(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool Validation(Product entity)
        {//TODO IMPLEMENT
            return true;
        }

        public async Task AddCategoriesAsync(int productId, int[] categoryIds)
        {
            await _unitOfWork.ProductRepository.AddCategoriesAsync(productId, categoryIds);
            _unitOfWork.SaveAsync();
        }

        public void AddCategories(int productId, int[] categoryIds)
        {
            _unitOfWork.ProductRepository.AddCategories(productId, categoryIds);
            _unitOfWork.Save();
        }

        public void AddImage(int productId, int imageId)
        {
            _unitOfWork.ProductRepository.AddImage(productId, imageId);
            _unitOfWork.Save();
        }

        public Product GetById(int id)
        {
            return _unitOfWork.ProductRepository.GetById(id);
        }

        public List<Category> GetCategoriesOfProduct(int productId)
        {
            return _unitOfWork.ProductRepository.GetCategoriesOfProduct(productId);
        }

        public Task<List<Product>> GetSortedProducts(int sortingIndex)
        {
            return _unitOfWork.ProductRepository.GetSortedProducts(sortingIndex);
        }

        public int GetProductsCount()
        {
            return _unitOfWork.ProductRepository.GetProductsCount();
        }

        public void AssignNewPopularProducts(int[] newProductIds)
        {
            _unitOfWork.ProductRepository.AssignNewPopularProducts(newProductIds);
            _unitOfWork.Save();
        }

        public List<Product> GetRandomPopularProducts(int numberOfProducts, int range)
        {
            return _unitOfWork.ProductRepository.GetRandomPopularProducts(numberOfProducts, range);
        }
    }
}
