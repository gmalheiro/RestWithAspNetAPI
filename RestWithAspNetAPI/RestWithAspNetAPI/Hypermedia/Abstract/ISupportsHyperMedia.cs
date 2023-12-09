namespace RestWithAspNetAPI.Hypermedia.Abstract
{
    public interface ISupportsHyperMedia
    {
        public List<HyperMediaLink> Links { get; set; }
    }
}
