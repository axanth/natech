namespace Application.ExternalServices
{
    public class Breed
    {
        public Weight Weight { get; set; } = new();
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CfaUrl { get; set; } = string.Empty;
        public string VetstreetUrl { get; set; } = string.Empty;
        public string VcahospitalsUrl { get; set; } = string.Empty;
        public string Temperament { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string CountryCodes { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LifeSpan { get; set; } = string.Empty;
        public int Indoor { get; set; }
        public int Lap { get; set; }
        public string AltNames { get; set; } = string.Empty;
        public int Adaptability { get; set; }
        public int AffectionLevel { get; set; }
        public int ChildFriendly { get; set; }
        public int DogFriendly { get; set; }
        public int EnergyLevel { get; set; }
        public int Grooming { get; set; }
        public int HealthIssues { get; set; }
        public int Intelligence { get; set; }
        public int SheddingLevel { get; set; }
        public int SocialNeeds { get; set; }
        public int StrangerFriendly { get; set; }
        public int Vocalisation { get; set; }
        public int Experimental { get; set; }
        public int Hairless { get; set; }
        public int Natural { get; set; }
        public int Rare { get; set; }
        public int Rex { get; set; }
        public int SuppressedTail { get; set; }
        public int ShortLegs { get; set; }
        public string WikipediaUrl { get; set; } = string.Empty;
        public int Hypoallergenic { get; set; }
        public string ReferenceImageId { get; set; } = string.Empty;
        public int? CatFriendly { get; set; }
        public int? Bidability { get; set; }
    }
    public class ApiCatsResult
    {
        public List<Breed> Breeds { get; set; } = [];
        public string Id { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
    }
    public class Weight
    {
        public string Imperial { get; set; } = string.Empty;
        public string Metric { get; set; } = string.Empty;
    }
}
