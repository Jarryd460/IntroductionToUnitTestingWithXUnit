using FluentAssertions;
using NSubstitute;
using UnderstandingDependencies.Api.Models;
using UnderstandingDependencies.Api.Repositories;
using UnderstandingDependencies.Api.Services;

namespace UnderstandingDependencies.Api.Tests.Unit;

public class UserServiceTests
{
    private readonly UserService _userServiceWithFaker;
    private readonly UserService _userServiceWithNSubstitute;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();

    public UserServiceTests()
    {
        _userServiceWithFaker = new UserService(new FakeUserRepository());
        _userServiceWithNSubstitute = new(_userRepository);
    }

    [Fact]
    public async void GetAllAsync_Should_ReturnEmptyList_When_NoUsersExist()
    {
        // Arrange

        // Act
        var users = await _userServiceWithFaker.GetAllAsync();

        // Assert
        users.Should().BeEmpty();
    }

    [Fact]
    public async void GetAllAsync_Should_ReturnAListOfUsers_When_UsersExist()
    {
        // Arrange
        var expectedUsers = new[]
        {
            new User()
            {
                Id = Guid.NewGuid(),
                FullName = "Jarryd Deane"
            }
        };
        _userRepository.GetAllAsync().Returns(expectedUsers);

        // Act
        var users = await _userServiceWithNSubstitute.GetAllAsync();

        // Assert
        users.Should().ContainSingle(x => x.FullName == "Jarryd Deane");
    }
}