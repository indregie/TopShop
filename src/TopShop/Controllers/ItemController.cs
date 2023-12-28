﻿using Microsoft.AspNetCore.Mvc;
using Domain.Data.Dtos;
using Domain.Services;

namespace TopShop.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private ItemService _itemService;

    public ItemController(ItemService itemService)
    {
        _itemService = itemService;
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