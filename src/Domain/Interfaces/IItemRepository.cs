using Domain.Data.Entities;

namespace Domain.Interfaces;

public interface IItemRepository
{
    Task<Item?> Add(Item item);
    Task Delete(Guid id);
    Task<Item> Edit(Item item);
    Task<IEnumerable<Item>> Get();
    Task<Item?> Get(Guid id);
}