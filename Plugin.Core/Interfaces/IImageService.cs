using Plugin.Core.Models;

namespace Plugin.Core.Interfaces
{
    public interface IImageService
    {
        Task<List<ImageInfo>> GetImages();
        Task<ImageInfo> GetImageById(int id);
        Task<bool> AddImage(ImageInfo model);
        Task<bool> DeleteImage(int id);
    }
}
