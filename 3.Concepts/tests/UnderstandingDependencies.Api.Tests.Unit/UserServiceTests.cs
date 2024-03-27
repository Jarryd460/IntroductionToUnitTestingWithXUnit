using FluentAssertions;
using UnderstandingDependencies.Api.Services;

namespace UnderstandingDependencies.Api.Tests.Unit;

public class UserServiceTests
{
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(new FakeUserRepository());
    }

    [Fact]
    public async void GetAllAsync_Should_ReturnEmptyList_When_NoUsersExist()
    {
        // Arrange

        // Act
        var users = await _userService.GetAllAsync();

        // Assert
        users.Should().BeEmpty();
    }
}