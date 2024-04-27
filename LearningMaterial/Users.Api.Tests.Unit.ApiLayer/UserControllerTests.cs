using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Users.Api.Contracts;
using Users.Api.Controllers;
using Users.Api.Mappers;
using Users.Api.Models;
using Users.Api.Services;
using Xunit;

namespace Users.Api.Tests.Unit.ApiLayer;

public class UserControllerTests
{
    private readonly UserController _sut;
    private readonly IUserService _userService = Substitute.For<IUserService>();

    public UserControllerTests()
    {
        _sut = new UserController(_userService);
    }

    [Fact]
    public async Task GetById_ReturnOkAndObject_WhenUserExists()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Nick Chapsas"
        };
        _userService.GetByIdAsync(user.Id).Returns(user);
        var userResponse = user.ToUserResponse();

        // Act
        var result = (OkObjectResult)await _sut.GetById(user.Id);

        // Assert
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(userResponse);
    }

    [Fact]
    public async Task GetById_ReturnNotFound_WhenUserDoesntExists()
    {
        // Arrange
        _userService.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

        // Act
        var result = (NotFoundResult)await _sut.GetById(Guid.NewGuid());

        // Assert
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GetAll_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        _userService.GetAllAsync().Returns(Enumerable.Empty<User>());

        // Act
        var result = (OkObjectResult)await _sut.GetAll();

        // Assert
        result.StatusCode.Should().Be(200);
        result.Value.As<IEnumerable<UserResponse>>().Should().BeEmpty();
    }

    [Fact]
    public async Task GetAll_ShouldReturnUsersResponse_WhenUsersExist()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Nick Chapsas"
        };
        var users = new[] { user };
        var usersResponse = users.Select(x => x.ToUserResponse());
        _userService.GetAllAsync().Returns(users);

        // Act
        var result = (OkObjectResult)await _sut.GetAll();

        // Assert
        result.StatusCode.Should().Be(200);
        result.Value.As<IEnumerable<UserResponse>>().Should().BeEquivalentTo(usersResponse);
    }


    [Fact]
    public async Task Create_Should_CreateUser_When_CreateUserRequestIsValid()
    {
        // Arrange
        var createUserRequest = new CreateUserRequest()
        {
            FullName = "Jarryd Deane"
        };
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = createUserRequest.FullName
        };
        /* 
         * Inside the _sut.Create method, a new user is created with the createUserRequest details.
         * To solve this there are the 2 ways below
        */

        // First way (hack way) - checks that the user matches the first name and if so returns True
        //_userService.CreateAsync(Arg.Is<User>(testUser => testUser.FullName == user.FullName)).Returns(true);

        // Second way (proper way) - Takes whatever was being passed in and assigns it to my user object and then return true.
        _userService.CreateAsync(Arg.Do<User>(testUser => user = testUser)).Returns(true);        

        // Act
        var result = (CreatedAtActionResult)await _sut.Create(createUserRequest);

        // Assert
        /* 
         * The conversion of user happens here because of the "second way" doing a reassignment of the user object
         * so we have to wait for the reassignment that happens inside _sut.Create before converting the 
         * user object.
        */
        var userResponse = user.ToUserResponse();
        result.StatusCode.Should().Be(201);
        // First way
        //result.Value.As<UserResponse>().Should().BeEquivalentTo(userResponse,
        //    options => options.Excluding(user => user.Id));
        // Second way
        result.Value.As<UserResponse>().Should().BeEquivalentTo(userResponse);
        result.RouteValues!["id"].Should().BeEquivalentTo(user.Id);
    }

    [Fact]
    public async Task Create_Should_ReturnBadRequest_When_CreateUserRequestIsInvalid()
    {
        // Arrange
        _userService.CreateAsync(Arg.Any<User>()).Returns(false);

        // Act
        var result = (BadRequestResult)await _sut.Create(new CreateUserRequest());

        // Assert
        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task DeleteById_Should_ReturnOk_When_UserWasDeleted()
    {
        // Arrange
        _userService.DeleteByIdAsync(Arg.Any<Guid>()).Returns(true);;

        // Act
        var result = (OkResult)await _sut.DeleteById(Guid.NewGuid());

        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task DeleteById_Should_ReturnNotFound_When_UserWasNotDeleted()
    {
        // Arrange
        _userService.DeleteByIdAsync(Arg.Any<Guid>()).Returns(false);

        // Act
        var result = (NotFoundResult)await _sut.DeleteById(Guid.NewGuid());

        // Assert
        result.StatusCode.Should().Be(404);
    }
}
