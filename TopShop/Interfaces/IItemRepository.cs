using TopShop.Data.Entities;

namespace TopShop.Interfaces
{
    public interface IItemRepository
    {
        Item Add(Item item);
        void Delete(Guid id);
        Item Edit(Item item);
        Task<IEnumerable<Item>> Get();
        Task<Item> Get(Guid id);
    }
}