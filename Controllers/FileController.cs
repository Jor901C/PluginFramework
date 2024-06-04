using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Plugin.Core.Interfaces;
using Plugin.Core.Models;
using PluginFramework.Models.Requests;
using PluginFramework.Models.Responses;

namespace InterviewTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ImageResponse>> Get()
        {
            var images = await _imageService.GetImages();
            return _mapper.Map<List<ImageInfo>, List<ImageResponse>>(images);
        }

        [HttpGet("{id}")]
        public async Task<ImageResponse> Get(int id)
        {
            var image = await _imageService.GetImageById(id);
            return _mapper.Map<ImageInfo, ImageResponse>(image);
        }

        [HttpPost]
        public async Task<bool> Post([FromForm] ImageRequest request)
        {
            var imageInfo = _mapper.Map<ImageRequest, ImageInfo>(request);
            await _imageService.AddImage(imageInfo);
            return true;
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _imageService.DeleteImage(id);
        }
    }
}
