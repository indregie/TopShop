using TopShop.Data.Dtos;
using TopShop.Data.Entities;
using TopShop.Exceptions;
using TopShop.Interfaces;

namespace TopShop.Services;

public class ItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<ResponseItem> Get(Guid id)
    {
        Item item = new Item()
        {
            Id = id
        };
        Item result = await _itemRepository.Get(id);
        if (result == null)
        {
            throw new ItemNotFoundException();
        }
        ResponseItem response = new ResponseItem()
        {
            Id = result.Id,
            Name = result.Name,
            Price = result.Price
        };
        return response;
    }

    public async Task<IEnumerable<Item>> Get()
    {
        return await _itemRepository.Get();
    }

    public async Task<ResponseItem> Add(AddItem addItem)
    {
        Item item = new Item()
        {
            Name = addItem.Name,
            Price = addItem.Price,
        };

        Item result = await _itemRepository.Add(item) ?? throw new Exception();
        ResponseItem response = new ResponseItem()
        {
            Id = result.Id,
            Name = result.Name,
            Price = result.Price
        };

        return response;
    }

    public async Task<ResponseItem> Edit(EditItem editItem, Guid id)
    {
        if (await Get(id) == null)
        {
            throw new ItemNotFoundException();
        }
        var item = new Item()
        {
            Id = id,
            Name = editItem.Name,
            Price = editItem.Price
        };
        var result = await _itemRepository.Edit(item);

        var response = new ResponseItem()
        {
            Id = result.Id,
            Name = result.Name,
            Price = result.Price
        };

        return response;
    }

    public async Task Delete(Guid id)
    {
        if (await Get(id) == null)
        {
            throw new ItemNotFoundException();
        }
        await _itemRepository.Delete(id);
    }
}
