namespace PostgresHelper.DataAccess.Entities;

public class User
{
    public int Id { get; init; }
    
    public required string Name { get; set; }
    
    public required string Email { get; set; }
}