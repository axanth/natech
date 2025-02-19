using System.ComponentModel.DataAnnotations;

namespace Api.Requests
{
    public class SearchByTagRequest
    {
        [Required]
        [Range(0,100)]
        public int Page { get; set; }

        [Required]
        [Range(1, 100)]
        public int PageSize { get; set; }
        
        public string? TagName { get; set; }
    }
}