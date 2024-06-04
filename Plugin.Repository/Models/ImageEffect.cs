namespace Plugin.Repository
{
    public partial class ImageEffect
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public int EffectId { get; set; }
        public virtual Effect Effect { get; set; }
        public virtual Image Image { get; set; }
    }
}
