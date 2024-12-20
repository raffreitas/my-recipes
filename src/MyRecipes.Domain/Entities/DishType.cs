namespace MyRecipes.Domain.Entities;

public class DishType : EntityBase
{
    public Enums.DishType Type { get; set; }
    public long UserId { get; set; }
}