namespace Plugin.Repository
{
    public partial class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Radius { get; set; }
        public int Size { get; set; }
        public string Path { get; set; }

        public virtual ICollection<ImageEffect> ImageEffects { get; set; }
    }
}
