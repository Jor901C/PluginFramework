namespace Plugin.Repository
{
    public partial class Effect
    {
        public Effect()
        {
            ImageEffects = new HashSet<ImageEffect>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ImageEffect> ImageEffects { get; set; }
    }
}
