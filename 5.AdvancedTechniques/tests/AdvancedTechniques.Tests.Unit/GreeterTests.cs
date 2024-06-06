using NSubstitute;

namespace AdvancedTechniques.Tests.Unit;

public sealed class GreeterTests
{
    private readonly Greeter _sut;
    private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();

    public GreeterTests()
    {
        _sut = new Greeter(_dateTimeProvider);
    }

    [Fact]
    public void GenerateGreetMessage_Should_SayGoodEvening_When_ItIsEvening()
    {
        // Arrange
        _dateTimeProvider.DateTimeNow.Returns(new DateTime(2024, 6, 6, 20, 0, 0));

        // Act
        var result = _sut.GenerateGreetMessage();

        // Assert
        result.Should().Be("Good evening");
    }

    [Fact]
    public void GenerateGreetMessage_Should_SayGoodMorning_When_ItIsMorning()
    {
        // Arrange
        _dateTimeProvider.DateTimeNow.Returns(new DateTime(2024, 6, 6, 10, 0, 0));

        // Act
        var result = _sut.GenerateGreetMessage();

        // Assert
        result.Should().Be("Good morning");
    }

    [Fact]
    public void GenerateGreetMessage_Should_SayGoodAfternoon_When_ItIsAfternoon()
    {
        // Arrange
        _dateTimeProvider.DateTimeNow.Returns(new DateTime(2024, 6, 6, 15, 0, 0));

        // Act
        var result = _sut.GenerateGreetMessage();

        // Assert
        result.Should().Be("Good afternoon");
    }
}