using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace FlyballRaceDay.ApiService.Services;

public abstract class DataService<TData,TCreate,TView>(ILoggerFactory loggerFactory, FlyballRaceDayDbContext context) where TData: DataModel,new() where TCreate: new() where TView: new() 
{
    protected async Task<TView> Create(TCreate create)
    {
        var newDataRecord = Mapper.Map<TCreate,TData>(create);
        var dataRecord = context.Set<TData>().Add(newDataRecord);
        await context.SaveChangesAsync();
        return Mapper.Map<TData, TView>(dataRecord.Entity);
    }

    protected async Task<List<TView>> Where(Expression<Func<TData,bool>> query)
    {
        var queryResults = await context.Set<TData>().Where(query).ToListAsync();
        return queryResults.MapList(Mapper.Map<TData, TView>);
    }

    protected async Task<TView> Update(TCreate create, int id)
    {
        var objectToUpdate = Mapper.Map<TCreate,TData>(create);
        objectToUpdate.Id = id;
        context.Update(objectToUpdate);
        await context.SaveChangesAsync();
        return Mapper.Map<TData, TView>(objectToUpdate);
    }

    protected async Task Delete(int id)
    {
        var objectToDelete = context.Set<TData>().FirstAsync(x => x.Id == id);
        context.Remove(objectToDelete);
        await context.SaveChangesAsync();
    }
}