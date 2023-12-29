using Domain.Data.Dtos;
using Domain.Data.Entities;

namespace Infrastructure.Repositories;

public interface IShopRepository
{
    Task<Shop?> Add(Shop shop);
    Task Delete(Guid id);
    Task<Shop> Edit(Shop shop);
    Task<IEnumerable<Shop>> Get();
    Task<Shop?> Get(Guid id);
}