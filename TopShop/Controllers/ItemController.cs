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
        public async Task<IActionResult> Post(AddItem addItem)
        {
            return Ok(await _itemService.Add(addItem));
        }

        [HttpPut("{id}")]
        public async Task <IActionResult> Put(EditItem editItem, Guid id)
        {
            return Ok(await _itemService.Edit(editItem, id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _itemService.Delete(id);
            return Ok();
        }

    }
}
