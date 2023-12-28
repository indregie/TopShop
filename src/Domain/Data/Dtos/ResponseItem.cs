using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Data.Dtos;

public class ResponseItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

}
