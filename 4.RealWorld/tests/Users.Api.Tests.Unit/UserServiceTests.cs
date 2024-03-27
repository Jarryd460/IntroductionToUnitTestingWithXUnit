using Users.Api.Repositories;
using Users.Api.Services;
using NSubstitute;
using Microsoft.Extensions.Logging;
using Users.Api.Models;
using FluentAssertions;
using Users.Api.Logging;
using NSubstitute.ExceptionExtensions;
using Microsoft.Data.Sqlite;

namespace Users.Api.Tests.Unit;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly ILoggerAdapter<UserService> _logger = Substitute.For<ILoggerAdapter<UserService>>();

    public UserServiceTests()
    {
        _userService = new UserService(_userRepository, _logger);
    }

    [Fact]
    public async Task GetAllAsync_Should_ReturnEmptyList_When_NoUsersExist()
    {
        // Arrange
        _userRepository.GetAllAsync().Returns(Enumerable.Empty<User>());

        // Act
        var result = await _userService.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_Should_ReturnUsers_When_SomeUsersExist()
    {
        // Arrange
        var jarrydDeane = new User()
        {
            Id = Guid.NewGuid(),
            FullName = "Jarryd Deane"
        };
        var expectedUsers = new[]
        {
            jarrydDeane
        };
        _userRepository.GetAllAsync().Returns(expectedUsers);

        // Act
        var result = await _userService.GetAllAsync();

        // Assert
        //result.Single().Should().BeEquivalentTo(jarrydDeane);
        result.Should().BeEquivalentTo(expectedUsers);
    }

    [Fact]
    public async Task GetAllAsync_Should_LogMessages_When_Invoked()
    {
        // Arrange
        _userRepository.GetAllAsync().Returns(Enumerable.Empty<User>());

        // Act
        await _userService.GetAllAsync();

        // Assert
        _logger.Received(1).LogInformation(Arg.Is("Retrieving all users"));
        _logger.Received(1).LogInformation(Arg.Is("All users retrieved in {0}ms"), Arg.Any<long>());
    }

    [Fact]
    public async Task GetAllAsync_Should_LogMessageAndException_When_ExceptionIsThrown()
    {
        // Arrange
        var sqliteException = new SqliteException("Something went wrong", 500);
        _userRepository.GetAllAsync()
            .Throws(sqliteException);

        // Act
        var requestAction = async () => await _userService.GetAllAsync();

        // Assert
        await requestAction.Should().ThrowAsync<SqliteException>().WithMessage("Something went wrong");
        _logger.Received(1).LogError(Arg.Is(sqliteException), Arg.Is("Something went wrong while retrieving all users"));
    }
}