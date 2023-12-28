using Domain.Data.Dtos;
using Domain.Data.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ShopService
    {
        private readonly IShopRepository _shopRepository;

        public ShopService(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<ResponseShopDto> Get(Guid id)
        {
            Shop shop = new Shop()
            {
                Id = id
            };
            Shop result = await _shopRepository.Get(id);
            if (result == null)
            {
                throw new ItemNotFoundException();
            }
            ResponseShopDto response = new ResponseShopDto()
            {
                Id = result.Id,
                Name = result.Name,
                Address = result.Address
            };
            return response;
        }

        public async Task<IEnumerable<ResponseShopDto>> Get()
        {
            IEnumerable<Shop> shops = await _shopRepository.Get();
            IEnumerable<ResponseShopDto> responseShops = shops.Select(shop => new ResponseShopDto
            {
                Id = shop.Id,
                Name = shop.Name,
                Address = shop.Address
            });
            return responseShops;
        }

        public async Task<ResponseShopDto> Add(AddItem addItem)
        {
            Item item = new Item()
            {
                Name = addItem.Name,
                Price = addItem.Price,
            };

            Item result = await _itemRepository.Add(item) ?? throw new Exception();
            ResponseShopDto response = new ResponseShopDto()
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price
            };

            return response;
        }

    }
}
