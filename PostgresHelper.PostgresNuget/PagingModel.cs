namespace PostgresHelper.PostgresNuget;

public class PagingModel<TModel>(ICollection<TModel> models, long totalAmount)
{
    public ICollection<TModel> Models { get; init; } = models;

    public long TotalAmount { get; init; } = totalAmount;
}