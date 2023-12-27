using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.WebApi.Controllers;
using TopShop.WebApi.Interfaces;
using TopShop.WebApi.Services;

namespace TopShop.UnitTests.Services;

public class UserServiceTests
{
    private readonly Mock<IJsonPlaceholderClient> _userRepositoryMock;
    private readonly UserService _userService;
    private readonly Fixture _fixture;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IJsonPlaceholderClient>();
        _userService = new UserService(_userRepositoryMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task Get_GivenValidId_ReturnsDto()
    {
        //Arrange
        var id = _fixture.Create<int>();
        id = id % 10 + 1;
        //_userRepositoryMock.Setup(m => m.GetUserById(id)).ReturnsAsync(id);

        //Act

        //Assert
    }

}
