using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PostgresHelper.PostgresNuget;

public static class QueryableExtensions
{
    private static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, BasePagingParam param)
    {
        query = query.Skip(param.Start);

        return param.Count <= 0 ? query : query.Take(param.Count);
    }

    public static Task<PagingModel<T>> GetPagingModelAsync<T, TV>(this IQueryable<T> query, BasePagingParam param, Expression<Func<T, TV>> order, bool ascending = true)
    {
        var orderedQuery = ascending ? query.OrderBy(order) : query.OrderByDescending(order);
        return orderedQuery.GetPagingModelAsync(param);
    }

    private static async Task<PagingModel<T>> GetPagingModelAsync<T>(this IOrderedQueryable<T> query, BasePagingParam param)
    {
        var count = await query.CountAsync();
        var data = await query.ApplyPagination(param).ToArrayAsync();
        return new PagingModel<T>(data, count);
    }

    public static async Task<PagingModel<T>> GetPagingModelAsync<T>(this IQueryable<T> query, BasePagingParam param)
    {
        if (query is not IOrderedQueryable<T>)
            throw new ArgumentException("Query is not ordered");

        var count = await query.CountAsync();
        var data = await query.ApplyPagination(param).ToArrayAsync();
        return new PagingModel<T>(data, count);
    }
}