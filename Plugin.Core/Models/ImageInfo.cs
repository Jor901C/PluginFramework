using Microsoft.AspNetCore.Http;


namespace Plugin.Core.Models
{
    public class ImageInfo
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public int Radius { get; set; }
        public List<int> Effects { get; set; }
        public string Path { get; set; }
        public IFormFile Image { get; set; }
    }
}
