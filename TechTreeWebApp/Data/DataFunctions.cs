using Microsoft.EntityFrameworkCore;
using TechTreeWebApp.Areas.Admin.Models;
using TechTreeWebApp.Entities;

namespace TechTreeWebApp.Data
{
    public class DataFunctions : IDataFunctions
    {
        private readonly ApplicationDbContext _context;

        public DataFunctions(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task UpdataUserCategoryEntityAsync(List<UserCategory> userCategoriesToAdd, List<UserCategory> userCategoriesToDelete) //check
        {
            using (var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.RemoveRange(userCategoriesToDelete);
                    await _context.SaveChangesAsync();   //<-- Must save the changes in order to reflect back to UsersToCategory.retrieveUsers()
                    if (userCategoriesToAdd != null) //<-- Check if there are selected checkboxes 
                    {
                        _context.AddRange(userCategoriesToAdd);
                        await _context.SaveChangesAsync();
                    }
                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.DisposeAsync();
                }
            }
        }
    }
}
