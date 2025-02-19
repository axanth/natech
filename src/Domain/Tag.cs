namespace Domain
{
    public class Tag : BaseEntity,IComparable<Tag>
    {
        private Tag() { }
        public Tag(string name)
        {
            Name = name;
            Cats = [];
        }

        public string Name { get; set; } = string.Empty;
        public List<Cat> Cats { get; set; } = [];
        public int CompareTo(Tag? other)
        {
            if (other == null) return 1;
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return obj is Tag tag && Name == tag.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

    }
}
