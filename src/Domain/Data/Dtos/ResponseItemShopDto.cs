namespace Domain.Data.Dtos;

public class ResponseItemShopDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Guid ShopId { get; set; }
}
