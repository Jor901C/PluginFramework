namespace PluginFramework.Models.Requests
{
    public class ImageRequest
    {
        public int Size { get; set; }
        public int Radius { get; set; }
        public List<int> Effects { get; set; }
        public IFormFile Image { get; set; }
    }
}
