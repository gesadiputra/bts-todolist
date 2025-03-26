using Microsoft.EntityFrameworkCore;
using RecruitmentTest.Db;
using RecruitmentTest.Db.Entities;
using RecruitmentTest.Model.Checklist;

namespace RecruitmentTest.Services;

public interface IChecklistServices
{
    #region Checklist
    public Task Create(ChecklistCreateRequest request);
    public Task Delete(int Id);
    public Task<List<ChecklistGetResponse>> GetAll();
    public Task<ChecklistGetResponse> GetById(int Id);
    #endregion
    #region ChecklistItem
    
    public Task ItemCreate(int ChecklistId, ChecklistItemRequest request);
    public Task ItemUpdate(int ChecklistId, int ItemId, ChecklistItemRequest request);
    public Task ItemUpdteStatus(int ChecklistId, int ItemId);
    public Task ItemDelete(int ChecklistId, int ItemId);
    public Task<List<ChecklistItemGetResponse>> ItemGetList(int ChecklistId);
    public Task<ChecklistItemGetResponse> ItemGetById(int ChecklistId, int ItemId);
    #endregion
}

public class ChecklistServices : IChecklistServices
{
    private readonly ApplicationDbContext _dbContext;
    public ChecklistServices(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(ChecklistCreateRequest request)
    {
        var checklist = new Checklist()
        {
            Name = request.Name,
            ColorCode = request.ColorCode,
            CreatedDate = DateTime.UtcNow
        };
        await _dbContext.AddAsync(checklist);
    }

    private async Task<Checklist> GetChecklistAsync(int Id)
    {
        var checklist = await _dbContext
            .Set<Checklist>()
            .Where(p => p.Id == Id)
            .FirstOrDefaultAsync();
        if (checklist == null) throw new Exception("Checklist Not Found");

        return checklist;
    }

    public async Task Delete(int Id)
    {
        var checklist = await GetChecklistAsync(Id);

        var checklistItem = await _dbContext
            .Set<ChecklistItem>()
            .Where(p => p.ChecklistId == Id)
            .ToListAsync();
        foreach(var item in checklistItem)
        {
            _dbContext.Remove(item);
        }

        _dbContext.Remove(checklist);
    }

    public async Task<List<ChecklistGetResponse>> GetAll()
    {
        var checklist = await _dbContext
            .Set<Checklist>()
            .Select(p => new ChecklistGetResponse
            {
                ChecklistId = p.Id,
                Name = p.Name,
                ColorCode = p.ColorCode,
            })
            .ToListAsync();
        return checklist;
    }

    public async Task<ChecklistGetResponse> GetById(int Id)
    {
        var checklist = await _dbContext
            .Set<Checklist>()
            .Where(p => p.Id == Id)
            .Select(p => new ChecklistGetResponse
            {
                ChecklistId = p.Id,
                Name = p.Name,
                ColorCode = p.ColorCode,
            })
            .FirstOrDefaultAsync();
        if (checklist == null) throw new Exception("Checklist Not Found");

        return checklist;
    }

    public async Task ItemCreate(int ChecklistId, ChecklistItemRequest request)
    {
        var checklist = await GetChecklistAsync(ChecklistId);

        var item = new ChecklistItem()
        {
            ChecklistId = ChecklistId,
            Name = request.ItemName,
            IsChecked = false,
        };

        await _dbContext.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<ChecklistItem> GetChecklistItemAsync(int ChecklistId, int ItemId)
    {
        var item = await _dbContext
            .Set<ChecklistItem>()
            .Where(p => p.ChecklistId == ChecklistId && p.Id == ItemId)
            .FirstOrDefaultAsync();
        if (item == null) throw new Exception("Item Not Found");
        return item;
    }

    public async Task ItemDelete(int ChecklistId, int ItemId)
    {
        var item = await GetChecklistItemAsync(ChecklistId, ItemId);
        _dbContext.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ChecklistItemGetResponse> ItemGetById(int ChecklistId, int ItemId)
    {
        var item = await GetChecklistItemAsync(ChecklistId, ItemId);
        return new ChecklistItemGetResponse()
        {
            ItemId = item.Id,
            ChecklistId = item.ChecklistId,
            Name = item.Name,
            IsChecked = item.IsChecked
        };
    }

    public async Task<List<ChecklistItemGetResponse>> ItemGetList(int ChecklistId)
    {
        var items = await _dbContext
            .Set<ChecklistItem>()
            .Where(p => p.ChecklistId == ChecklistId)
            .Select(p => new ChecklistItemGetResponse
            {
                ItemId = p.Id,
                ChecklistId = p.ChecklistId,
                Name = p.Name,
                IsChecked = p.IsChecked
            })
            .ToListAsync();
        return items;
    }

    public async Task ItemUpdate(int ChecklistId, int ItemId, ChecklistItemRequest request)
    {
        var item = await GetChecklistItemAsync(ChecklistId, ItemId);
        _dbContext.Attach(item);
        item.Name = request.ItemName;
        await _dbContext.SaveChangesAsync();
    }

    public async Task ItemUpdteStatus(int ChecklistId, int ItemId)
    {
        var item = await GetChecklistItemAsync(ChecklistId, ItemId);
        _dbContext.Attach(item);
        item.IsChecked = !item.IsChecked;
        await _dbContext.SaveChangesAsync();
    }
}
