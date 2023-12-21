using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TopShop.Data.Dtos;
using TopShop.Data.Entities;
using TopShop.Interfaces;

namespace TopShop.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IDbConnection _connection;

        public ItemRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Item> Get(Guid id)
        {
            string sql = "SELECT * FROM public.items WHERE id = @Id AND \"isDeleted\"=false";
            var queryObject = new
            {
                Id = id
            };
            return await _connection.QuerySingleAsync<Item>(sql, queryObject);
        }


        public async Task<IEnumerable<Item>> Get()
        {
            string sql = "SELECT * FROM public.items WHERE \"isDeleted\"=false";
            return await _connection.QueryAsync<Item>(sql);
        }

        public Item Add(Item item)
        {
            string sql = "INSERT INTO items (name, price) VALUES (@Name, @Price) RETURNING id as Id, name as Name, price as Price";
            var queryObject = new
            {
                name = item.Name,
                price = item.Price
            };

            return _connection.QuerySingle<Item>(sql, queryObject);
        }

        public Item Edit(Item item)
        {
            string sql = "UPDATE items SET name=@Name, price=@Price WHERE id=@Id RETURNING id as Id, name as Name, price as Price";
            //var queryObject = new
            //{
            //    Id = item.Id,
            //    Name = item.Name,
            //    Price = item.Price
            //};

            return _connection.QuerySingle<Item>(sql, item);
        }

        public void Delete(Guid id)
        {
            string sql = "UPDATE items SET \"isDeleted\"=true WHERE id=@Id";
            var queryObject = new
            {
                Id = id
            };

            _connection.Execute(sql, queryObject);
        }
    }
}
