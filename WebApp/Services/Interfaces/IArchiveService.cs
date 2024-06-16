using WebApp.Models;

namespace WebApp.Services.Interfaces
{
    public interface IArchiveService
    {
        Task<IEnumerable<ArchiveCategory>> GetAllArchiveCategoriesAsync();
        Task<ArchiveCategory> GetArchiveCategoryByIdAsync(int id);
        Task<ArchiveCategory> CreateArchiveCategoryAsync(ArchiveCategory archiveCategory);
        Task<bool> DeleteArchiveCategoryAsync(int id);
        Task<bool> RestoreCategoryAsync(int id);
        Task<IEnumerable<ArchiveCategory>> SearchArchiveCategoriesAsync(string query);
    }
}
