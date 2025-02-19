namespace Domain
{
    public class Cat(string catId, int width, int height, byte[] image, List<Tag> tags) : BaseEntity, IComparable<Cat>
    {
        private Cat(): this(string.Empty, 0, 0, [], []) { }
        public string CatId { get; private set; } = catId;
        public int Width { get; private set; } = width;
        public int Height { get; private set; } = height;
        public byte[] Image { get; private set; } = image;
        public List<Tag> Tags { get; private set; } = tags;

        public int CompareTo(Cat? other)
        {
            if (other == null) return 1;
            return string.Compare(CatId, other.CatId, StringComparison.Ordinal);
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return obj is Cat cat && CatId == cat.CatId;
        }
        public override int GetHashCode()
        {
            return CatId.GetHashCode();
        }
    }
}
