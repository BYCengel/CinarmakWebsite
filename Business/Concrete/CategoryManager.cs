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
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Create(Category entity)
        {
            _unitOfWork.CategoryRepository.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Category entity)
        {
            _unitOfWork.CategoryRepository.Delete(entity);
            _unitOfWork.Save();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return categories;
        }

        public Category GetById(int id)
        {
            return _unitOfWork.CategoryRepository.GetById(id);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            return category;
        }

        public async Task<Category> GetByIdWithProductsAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdWithProductsAsync(id);
            return category;
        }

		public void IncreaseCategoryViewCount(string categoryUrl)
        {
            _unitOfWork.CategoryRepository.IncreaseCategoryViewCount(categoryUrl);
            _unitOfWork.Save();
        }

        public void RemoveFromCategory(int productId, int categoryId)
        {
            _unitOfWork.CategoryRepository.RemoveFromCategory(productId, categoryId);
            _unitOfWork.Save();
        }

        public void Update(Category entity)
        {
            _unitOfWork.CategoryRepository.Update(entity);
            _unitOfWork.Save();
        }

        public bool Validation(Category entity)
        {
            return true;
        }
    }
}
