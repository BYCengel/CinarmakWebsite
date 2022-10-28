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
    public class ImageManager : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Create(Image entity)
        {
            _unitOfWork.ImageRepository.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Image entity)
        {
            _unitOfWork.ImageRepository.Delete(entity);
            _unitOfWork.Save();
        }

        public async Task<List<Image>> GetHomeDisplayImagesAsync()
        {
            return await _unitOfWork.ImageRepository.GetHomeDisplayImagesAsync();
        }

        public void Update(Image entity)
        {
            _unitOfWork.ImageRepository.Update(entity);
            _unitOfWork.Save();
        }

        public void RemoveImageFromProduct(int productId, string imageUrl)
		{
            _unitOfWork.ImageRepository.RemoveImageFromProduct(productId, imageUrl);
            _unitOfWork.Save();
		}

        public bool Validation(Image entity)
        {
            throw new NotImplementedException();
        }
    }
}
