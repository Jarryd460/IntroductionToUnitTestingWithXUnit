using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Users.Api.Logging;
using Users.Api.Models;
using Users.Api.Repositories;
using Users.Api.Services;
using Xunit;

namespace Users.Api.Tests.Unit.Application;

public class UserServiceTests
{
    private readonly UserService _sut;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly ILoggerAdapter<UserService> _logger = Substitute.For<ILoggerAdapter<UserService>>();

    public UserServiceTests()
    {
        _sut = new UserService(_userRepository, _logger);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        _userRepository.GetAllAsync().Returns(Enumerable.Empty<User>());

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnUsers_WhenSomeUsersExist()
    {
        // Arrange
        var nickChapsas = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Nick Chapsas"
        };
        var expectedUsers = new[]
        {
            nickChapsas
        };
        _userRepository.GetAllAsync().Returns(expectedUsers);

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        //result.Single().Should().BeEquivalentTo(nickChapsas);
        result.Should().BeEquivalentTo(expectedUsers);
    }

    [Fact]
    public async Task GetAllAsync_ShouldLogMessages_WhenInvoked()
    {
        // Arrange
        _userRepository.GetAllAsync().Returns(Enumerable.Empty<User>());

        // Act
        await _sut.GetAllAsync();

        // Assert
        _logger.Received(1).LogInformation(Arg.Is("Retrieving all users"));
        _logger.Received(1).LogInformation(Arg.Is("All users retrieved in {0}ms"), Arg.Any<long>());
    }

    [Fact]
    public async Task GetAllAsync_ShouldLogMessageAndException_WhenExceptionIsThrown()
    {
        // Arrange
        var sqliteException = new SqliteException("Something went wrong", 500);
        _userRepository.GetAllAsync()
            .Throws(sqliteException);

        // Act
        var requestAction = async () => await _sut.GetAllAsync();

        // Assert
        await requestAction.Should()
            .ThrowAsync<SqliteException>().WithMessage("Something went wrong");
        _logger.Received(1).LogError(Arg.Is(sqliteException), Arg.Is("Something went wrong while retrieving all users"));
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnUser_When_UserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User()
        {
            Id = userId,
            FullName = "Jarryd Deane"
        };

        _userRepository.GetByIdAsync(userId).Returns(user);

        // Act
        // ConfigureAwait is not needed in unit testing
        var result = await _sut.GetByIdAsync(userId);

        // Assert
        result.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnNull_When_AUserDoesntExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepository.GetByIdAsync(userId).ReturnsNull();

        // Act
        var result = await _sut.GetByIdAsync(userId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_Should_LogMessages_When_Invoked()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User()
        {
            Id = userId,
            FullName = "Jarryd Deane"
        };

        _userRepository.GetByIdAsync(userId).Returns(user);

        // Act
        // ConfigureAwait is not needed in unit testing
        await _sut.GetByIdAsync(userId);

        // Assert
        Assert.Multiple(
            () => _logger.Received(1).LogInformation(Arg.Is("Retrieving user with id: {0}"), userId),
            () => _logger.Received(1).LogInformation(Arg.Is("User with id {0} retrieved in {1}ms"), userId, Arg.Any<long>())
        );
    }

    [Fact]
    public void GetByIdAsync_Should_LogMessageAndException_When_ExceptionIsThrown()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var sqliteException = new SqliteException("Something went wrong", 500);
        _userRepository.GetByIdAsync(userId)
            .Throws(sqliteException);

        // Act
        // ConfigureAwait is not needed in unit testing
        var requestAction = async () => await _sut.GetByIdAsync(userId);

        // Assert
        Assert.Multiple(
            async () => await requestAction.Should()
                .ThrowAsync<SqliteException>().WithMessage("Something went wrong"),
            () => _logger.Received(1).LogError(Arg.Is(sqliteException), Arg.Is("Something went wrong while retrieving user with id {0}"), userId)
        );
    }

    [Fact]
    public async Task CreateAsync_Should_CreateUser_When_DetailsAreValid()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User()
        {
            Id = userId,
            FullName = "Jarryd Deane"
        };

        _userRepository.CreateAsync(user).Returns(true);

        // Act
        var result = await _sut.CreateAsync(user);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task CreateAsync_Should_LogMessages_When_Invoked()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User()
        {
            Id = userId,
            FullName = "Jarryd Deane"
        };

        _userRepository.CreateAsync(user).Returns(true);

        // Act
        await _sut.CreateAsync(user);

        // Assert
        Assert.Multiple(
            () => _logger.Received(1).LogInformation("Creating user with id {0} and name: {1}", userId, user.FullName),
            () => _logger.Received(1).LogInformation("User with id {0} created in {1}ms", userId, Arg.Any<long>())
        );
    }

    [Fact]
    public void CreateAsync_Should_LogMessageAndException_When_ExceptionIsThrown()
    {
        // Arrange
        var user = new User()
        {
            Id = Guid.NewGuid(),
            FullName = "Jarryd Deane"
        };
        var sqliteException = new SqliteException("Something went wrong", 500);
        _userRepository.CreateAsync(user)
            .Throws(sqliteException);

        // Act
        var requestAction = async () => await _sut.CreateAsync(user);

        // Assert
        Assert.Multiple(            
            async () => await requestAction.Should()
                .ThrowAsync<SqliteException>().WithMessage("Something went wrong"),
            () => _logger.Received(1).LogError(Arg.Is(sqliteException), Arg.Is("Something went wrong while creating a user"))
        );
    }

    [Fact]
    public async Task DeleteByIdAsync_Should_DeleteUser_When_UserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepository.DeleteByIdAsync(userId).Returns(true);

        // Act
        var result = await _sut.DeleteByIdAsync(userId);


        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void DeleteByIdAsync_Should_NotDeleteUser_When_UserDoesntExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepository.DeleteByIdAsync(userId).Returns(false);

        // Act
        var result = await _sut.DeleteByIdAsync(userId);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteByIdAsync_Should_LogMessages_When_DeletingUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepository.DeleteByIdAsync(userId).Returns(true);

        // Act
        await _sut.DeleteByIdAsync(userId);

        // Assert
        Assert.Multiple(
            () => _logger.Received(1).LogInformation("Deleting user with id: {0}", userId),
            () => _logger.Received(1).LogInformation("User with id {0} deleted in {1}ms", userId, Arg.Any<long>())
        );
    }

    [Fact]
    public void DeleteByIdAsync_Should_LogMessageAndException_When_ExceptionIsThrown()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var sqliteException = new SqliteException("Something went wrong", 500);
        _userRepository.DeleteByIdAsync(userId)
            .Throws(sqliteException);

        // Act
        var requestAction = async () => await _sut.DeleteByIdAsync(userId);

        // Assert
        Assert.Multiple(
            async () => await requestAction.Should()
                .ThrowAsync<SqliteException>().WithMessage("Something went wrong"),
            () => _logger.Received(1).LogError(Arg.Is(sqliteException), Arg.Is("Something went wrong while deleting user with id {0}"), userId)
        );
    }
}
