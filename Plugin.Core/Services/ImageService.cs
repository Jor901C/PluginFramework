using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Plugin.Core.Interfaces;
using Plugin.Core.Models;
using Plugin.Repository;
using Plugin.Repository.Repositories;

namespace Plugin.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository _baseRepository;
        private readonly IConfiguration _configuration;

        public ImageService(
            IBaseRepository baseRepository, 
            IConfiguration configuration, 
            IMapper mapper
            )
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
            _configuration = configuration;
        }

        public async Task<bool> AddImage(ImageInfo model)
        {
            var image = _mapper.Map<ImageInfo, Image>(model);
            image.Name = model.Image.FileName;
            var filePath = _configuration["FilePath"];
            image.Path = filePath + "/" + model.Image.FileName;
            model.Effects.ForEach(item =>  image.ImageEffects.Add(new ImageEffect { EffectId = item }));

            using (var fs = File.Create(filePath))
            {
                model.Image.CopyTo(fs);
            }
            await _baseRepository.AddAsync(image);
            
            return true;
        }

        public async Task<bool> DeleteImage(int id)
        {
            var effects = await _baseRepository.GetAll<ImageEffect>(item => item.ImageId == id).ToListAsync();
            await _baseRepository.DeleteRangeAsync(effects);
            await _baseRepository.DeleteAsync<Image>(id);

            return true;
        }

        public async Task<ImageInfo> GetImageById(int id)
        {
            var image = await _baseRepository.FirstOrDefaultAsync<Image>(item => item.Id == id);
            return _mapper.Map<Image, ImageInfo>(image);
        }

        public async Task<List<ImageInfo>> GetImages()
        {
            var Images = await _baseRepository.GetAll<Image>().ToListAsync();
            return _mapper.Map<List<Image>, List<ImageInfo>>(Images);
        }
    }
}
