namespace Domain.Data.Dtos;

public class ResponseItemUserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int UserId { get; set; }
}
