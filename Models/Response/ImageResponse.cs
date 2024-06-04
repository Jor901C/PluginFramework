namespace PluginFramework.Models.Responses
{
    public class ImageResponse
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public int Radius { get; set; }
        public List<int> Effects { get; set; }
        public string Path { get; set; }
    }
}
