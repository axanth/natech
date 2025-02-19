using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

using Domain;
using Application.Interfaces;

namespace Infrastructure
{
    public class CatsRepository(CatsDbContext context) : ICatsRepository
    {
        private readonly CatsDbContext _context = context;
       
        public async Task<List<Cat>> GetPagedByTagAsync(int page, int pageSize, string? tagName)
        {
            int skip = (page - 1) * pageSize;

            var filteredCats = await _context.Cats
                .AsNoTracking()
                .Include(x => x.Tags)
                .Where(x => tagName == null || x.Tags.Any(t => t.Name == tagName))
                .Skip(skip)
                .Take(pageSize).ToListAsync();

            return filteredCats;
        }
        public async Task<Cat?> GetCatByIdAsync(int id)
        {
            return await _context.Cats
                .AsNoTracking()
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<string>> GetNonExistingCats(List<string> fetchedCatsIds)
        {
           var dbIds = await _context.Cats
                .AsNoTracking()
                .Select(x => x.CatId)
                .ToListAsync();

            var catIds = fetchedCatsIds.Except(dbIds);
            return catIds.ToList();
        }
        public async Task<int> GetCatCountAsync()
        {
            return await _context.Cats.CountAsync();
        }
        public async Task AddRangeAsync(IEnumerable<Cat> cats)
        {
            await _context.AddRangeAsync(cats);
        }
        public async Task<List<Tag>> HandleTagsAsync(IEnumerable<string> tagNames)
        {
            var distinctTagNames = tagNames.Distinct().ToList();
            var existingTags = await _context.Tags.ToListAsync();
            var existingTagNames = new HashSet<string>(existingTags.Select(t => t.Name));
            var tagsToReturn = new List<Tag>();

            using var transaction = await _context.Database.BeginTransactionAsync();
            foreach (var tagName in distinctTagNames)
            {
                if (existingTagNames.Contains(tagName))
                {
                    tagsToReturn.Add(existingTags.First(t => t.Name == tagName));
                }
                else
                {
                    var newTag = new Tag(tagName);
                    _context.Tags.Add(newTag);
                    tagsToReturn.Add(newTag);
                }
            }

            if (tagsToReturn.Count != 0)
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            return tagsToReturn;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}