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
                throw new ShopNotFoundException();
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
            IEnumerable<ResponseShopDto> responseShops = shops
                .Select(shop => new ResponseShopDto
            {
                Id = shop.Id,
                Name = shop.Name,
                Address = shop.Address
            });

            return responseShops;
        }

        public async Task<ResponseShopDto> Add(AddShopDto addShop)
        {
            Shop shop = new Shop()
            {
                Name = addShop.Name,
                Address = addShop.Address
            };

            Shop result = await _shopRepository.Add(shop) ?? throw new Exception();
            ResponseShopDto response = new ResponseShopDto()
            {
                Id = result.Id,
                Name = result.Name,
                Address = result.Address
            };

            return response;
        }

        public async Task<ResponseShopDto> Edit(EditShopDto editShop, Guid id)
        {
            if (await Get(id) == null)
            {
                throw new ShopNotFoundException();
            }

            Shop shop = new Shop()
            {
                Id = id,
                Name = editShop.Name,
                Address = editShop.Address
            };
            Shop result = await _shopRepository.Edit(shop);

            ResponseShopDto response = new ResponseShopDto()
            {
                Id = result.Id,
                Name = result.Name,
                Address = result.Address
            };

            return response;
        }

        public async Task Delete(Guid id)
        {
            if (await Get(id) == null)
            {
                throw new ShopNotFoundException();
            }
            await _shopRepository.Delete(id);
        }

    }
}
