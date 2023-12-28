using Dapper;
using Domain.Data.Entities;
using System.Data;

namespace Infrastructure.Repositories;

public class ShopRepository : IShopRepository
{
    private readonly IDbConnection _connection;

    public ShopRepository(IDbConnection connection)
    {
        _connection = connection;
    }


    public async Task<Shop?> Get(Guid id)
    {
        string sql = "SELECT * FROM public.shops WHERE id = @Id AND \"isDeleted\"=false";
        var queryObject = new
        {
            Id = id
        };
        return await _connection.QueryFirstOrDefaultAsync<Shop?>(sql, queryObject);
    }


    public async Task<IEnumerable<Shop>> Get()
    {
        string sql = "SELECT * FROM public.shops WHERE \"isDeleted\"=false";
        return await _connection.QueryAsync<Shop>(sql);
    }

    public async Task<Shop?> Add(Shop shop)
    {
        string sql = "INSERT INTO public.shops (name, address) VALUES (@Name, @Address) RETURNING id as Id, name as Name, address as Address";
        var queryObject = new
        {
            name = shop.Name,
            address = shop.Address
        };

        return await _connection.QueryFirstOrDefaultAsync<Shop?>(sql, queryObject);
    }

    public async Task<Shop> Edit(Shop shop)
    {
        string sql = "UPDATE public.shops SET name=@Name, address=@Address WHERE id=@Id RETURNING id as Id, name as Name, address as Address";

        return await _connection.QuerySingleAsync<Shop>(sql, shop);
    }

    public async Task Delete(Guid id)
    {
        string sql = "UPDATE public.shops SET \"isDeleted\"=true WHERE id=@Id";
        var queryObject = new
        {
            Id = id
        };

        await _connection.ExecuteAsync(sql, queryObject);
    }
}
