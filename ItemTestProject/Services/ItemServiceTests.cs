using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;  
using Moq;
using System;
using TopShop.Data.Dtos;
using TopShop.Data.Entities;
using TopShop.Exceptions;
using TopShop.Interfaces;
using TopShop.Services;

namespace ItemTestProject.Services;

public class ItemServiceTests
{
    private readonly Mock<IItemRepository> _itemRepositoryMock;
    private readonly ItemService _itemService;
    private readonly Fixture _fixture;

    public ItemServiceTests()
    {
        _itemRepositoryMock = new Mock<IItemRepository>();
        _itemService = new ItemService(_itemRepositoryMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    //[AutoData]
    public async Task Get_GivenValidId_ReturnsDto()
    {
        //Arrange
        var guid = Guid.NewGuid();

        _itemRepositoryMock.Setup(m => m.Get(guid)).ReturnsAsync(
            _fixture.Build<Item>()
            .With(x => x.Id, guid)
            .Create());

        //Act
        ResponseItem result = await _itemService.Get(guid);

        //Assert 
        _itemRepositoryMock.Verify(x => x.Get(guid), Times.Once);
        result.Id.Should().Be(guid);
    }

    [Fact]
    public async Task Get_GivenInvalidId_ThrowsItemNotFoundException()
    {
        //Arrange
        var guid = Guid.NewGuid();
        _itemRepositoryMock.Setup(m => m.Get(guid)).ReturnsAsync((Item?)null);

        //Act Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(() => _itemService.Get(guid));
        _itemRepositoryMock.Verify(x => x.Get(guid), Times.Once);
    }

    [Fact]
    public async Task GetAll_ReturnsAllItems()
    {
        //Arrange
        List<Item> expectedList = _fixture.Build<Item>()
            .With(item => item.Id, _fixture.Create<Guid>())
            .With(item => item.Name, _fixture.Create<string>())
            .With(item => item.Price, _fixture.Create<decimal>())
            .CreateMany(22)
            .ToList();

        _itemRepositoryMock.Setup(m => m.Get()).ReturnsAsync(expectedList);

        //Act
        var actualList = await _itemService.Get();

        //Assert 
        _itemRepositoryMock.Verify(x => x.Get(), Times.Once);
        actualList.Should().BeOfType<List<Item>>();
        actualList.Should().BeEquivalentTo(expectedList);
    }


    [Fact]
    public async Task Add_GivenAddDto_ReturnsResponseDto()
    {
        //Arrange
        var guid = Guid.NewGuid();
        AddItem addItem = new AddItem()
        {
            Name = "Test",
            Price = 10.01m
        };

        Item repoItem = new Item
        {
            Id = guid,
            Name = "Test",
            Price = 10.01m
        };

        ResponseItem expectedResponseItem = new ResponseItem 
        { 
            Id = guid,
            Name = "Test",
            Price = 10.01m
        };

        _itemRepositoryMock.Setup(m => m.Add(It.IsAny<Item>())).ReturnsAsync(repoItem);

        //Act
        ResponseItem actualResponseItem = await _itemService.Add(addItem);

        //Assert 
        _itemRepositoryMock.Verify(x => x.Add(repoItem), Times.Once);
        actualResponseItem.Should().BeEquivalentTo(expectedResponseItem);
    }

    [Fact]
    public async Task Delete_GivenValidId_DoesNotThrowItemNotFoundException()
    {
        //Arrange
        var guid = Guid.NewGuid();

        _itemRepositoryMock.Setup(m => m.Get(guid)).ReturnsAsync(new Item()
        {
            Id = guid,
            Name = "Chocolate",
            Price = 1.99M
        });

        //Act Assert
        await _itemService.Invoking(r => r.Delete(guid)).Should().NotThrowAsync<Exception>();
        _itemRepositoryMock.Verify(x => x.Delete(guid), Times.Once);
    }

    [Fact]
    public async Task Delete_GivenInvalidId_ThrowsItemNotFoundException()
    {
        //Arrange
        var guid = Guid.NewGuid();
        _itemRepositoryMock.Setup(m => m.Delete(guid)).Returns(Task.FromResult<Item>(null));

        //Act Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(() => _itemService.Get(guid));
        _itemRepositoryMock.Verify(x => x.Delete(guid), Times.Once);
    }

}