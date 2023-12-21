using Microsoft.AspNetCore.Mvc;
using TopShop.Data.Dtos;
using TopShop.Data.Entities;
using TopShop.Services;

namespace TopShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : Controller
    {
        private ItemService _itemService;

        public ItemController(ItemService itemservice)
        {
            _itemService = itemservice;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _itemService.Get(id));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _itemService.Get());
        }

        [HttpPost]
        public IActionResult Post(AddItem addItem)
        {
            return Ok(_itemService.Add(addItem));
        }

        [HttpPut("{id}")]
        public IActionResult Put(EditItem editItem, Guid id)
        {
            return Ok(_itemService.Edit(editItem, id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _itemService.Delete(id);
            return Ok();
        }

    }
}
