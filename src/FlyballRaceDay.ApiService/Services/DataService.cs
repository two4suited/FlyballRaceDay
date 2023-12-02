using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FlyballRaceDay.ApiService.Services;

public abstract class DataService<TData,TCreate,TView>(ILoggerFactory loggerFactory, FlyballRaceDayDbContext context) where TData: DataModel,new() where TCreate: new() where TView: new() 
{
    protected async Task<IResult> Create(TCreate create)
    {
        var newDataRecord = Mapper.Map<TCreate,TData>(create);
        var dataRecord = context.Set<TData>().Add(newDataRecord);
        await context.SaveChangesAsync();
        return Results.Created("Item Created",Mapper.Map<TData, TView>(dataRecord.Entity));
    }

    protected async Task<IResult> Where(Expression<Func<TData,bool>> query)
    {
        var queryResults = await context.Set<TData>().Where(query).ToListAsync();
        return Results.Ok(queryResults.MapList(Mapper.Map<TData, TView>));
    }

    protected async Task<IResult> GetById(int id)
    {
        var objectReturn = await context.Set<TData>().FindAsync(id);
        return objectReturn != null ? Results.Ok(Mapper.Map<TData, TView>(objectReturn)) : Results.NotFound(); 
    }
    
    protected async Task<IResult> Update(TCreate create, int id)
    {
        var objectToUpdate = Mapper.Map<TCreate,TData>(create);
        objectToUpdate.Id = id;
        context.Update(objectToUpdate);
        await context.SaveChangesAsync();
        return Results.Ok(Mapper.Map<TData, TView>(objectToUpdate));
    }

    protected async Task<IResult> Delete(int id)
    {
        if (await context.Set<TData>().FindAsync(id) is not { } objectToDelete) return Results.NotFound();
        context.Remove(objectToDelete);
        await context.SaveChangesAsync();
        return Results.Ok();
    }
}