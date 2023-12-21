using TopShop.Data.Dtos;
using TopShop.Data.Entities;
using TopShop.Exceptions;
using TopShop.Repositories;

namespace TopShop.Services
{
    public class ItemService
    {
        private readonly ItemRepository _itemRepository;

        public ItemService(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<Item> Get(Guid id)
        {
            return await _itemRepository.Get(id);
        }

        public async Task<IEnumerable<Item>> Get()
        {
            return await _itemRepository.Get();
        }

        public ResponseItem Add(AddItem addItem)
        {
            Item item = new Item
            {
                Name = addItem.Name,
                Price = addItem.Price,
            };

            Item result = _itemRepository.Add(item);
            ResponseItem response = new ResponseItem()
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price
            };

            return response;
        }

        public ResponseItem Edit(EditItem editItem, Guid id)
        {
            if (Get(id) == null)
            {
                throw new ItemNotFoundException();
            }
            var item = new Item
            {
                Id = id,
                Name = editItem.Name,
                Price = editItem.Price
            };
            var result = _itemRepository.Edit(item);

            var response = new ResponseItem()
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price
            };

            return response;
        }

        public void Delete(Guid id)
        {
            if (Get(id) == null)
            {
                throw new ItemNotFoundException();
            }
            _itemRepository.Delete(id);

        }
    }
}
