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
    [Fact]
    public async Task Get_GivenValidId_ReturnsDto()
    {
        //Arrange
        Guid guid = new Guid();
        Mock<IItemRepository> testRepositoryMock = new Mock<IItemRepository>();
        testRepositoryMock.Setup(m => m.Get(guid)).ReturnsAsync(new Item()
        {
            Id = guid
        });

        var testRepository = testRepositoryMock.Object;
        var testService = new ItemService(testRepository);

        //Act

        ResponseItem result = await testService.Get(guid);

        //Assert 
        result.Id.Should().Be(guid);
    }

    [Fact]
    public async Task Get_GivenInvalidId_ThrowsItemNotFoundException()
    {
        //Arrange
        Guid guid = new Guid();
        Mock<IItemRepository> testRepositoryMock = new Mock<IItemRepository>();
        testRepositoryMock.Setup(m => m.Get(guid)).ReturnsAsync((Item?)null);

        var testRepository = testRepositoryMock.Object;
        var testService = new ItemService(testRepository);

        //Act Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(() => testService.Get(guid));
    }

    [Fact]
    public async Task GetAll_ReturnsAllItems()
    {
        //Arrange
        List<Item> expectedList = new List<Item>() 
        {
            new Item() { Id = Guid.NewGuid(), Name = "Milk", Price = 10.0m },
            new Item() { Id = Guid.NewGuid(), Name = "Bread", Price = 15.0m },
        };
    
        Mock<IItemRepository> testRepositoryMock = new Mock<IItemRepository>();
        testRepositoryMock.Setup(m => m.Get()).ReturnsAsync(expectedList);

        var testRepository = testRepositoryMock.Object;
        var testService = new ItemService(testRepository);

        //Act

        var actualList = await testService.Get();

        //Assert 

        Assert.Equal(expectedList, actualList);
    }


    [Fact]
    public async Task Add_GivenAddDto_ReturnsResponseDto()
    {
        //Arrange
        var guid = new Guid();
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

        var testRepositoryMock = new Mock<IItemRepository>();
        testRepositoryMock.Setup(m => m.Add(It.IsAny<Item>())).ReturnsAsync(repoItem);

        var testRepository = testRepositoryMock.Object;
        var testService = new ItemService(testRepository);

        //Act
        ResponseItem actualResponseItem = await testService.Add(addItem);

        //Assert 
        Assert.Equivalent(expectedResponseItem, actualResponseItem, true);

    }

    [Fact]
    public async Task Delete_GivenInvalidId_ThrowsItemNotFoundException()
    {
        //Arrange
        var guid = new Guid();

        Mock<IItemRepository> testRepositoryMock = new Mock<IItemRepository>();
        testRepositoryMock.Setup(m => m.Get(guid)).ReturnsAsync(new Item()
        {
            Id = guid,
            Name = "Chocolate",
            Price = 1.99M
        });
        testRepositoryMock.Setup(m => m.Delete(guid)).ReturnsAsync(1);

        var testRepository = testRepositoryMock.Object;
        var testService = new ItemService(testRepository);

        //Act Assert
        await testService.Invoking(r => r.Delete(guid)).Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task Delete_GivenValidId_DoNotThrowItemNotFoundException()
    {
        //Arrange
        Guid guid = new Guid();
        Mock<IItemRepository> testRepositoryMock = new Mock<IItemRepository>();
        testRepositoryMock.Setup(m => m.Delete(guid)).Returns(Task.FromResult<Item>(null));

        var testRepository = testRepositoryMock.Object;
        var testService = new ItemService(testRepository);

        //Act Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(() => testService.Get(guid));
    }

}