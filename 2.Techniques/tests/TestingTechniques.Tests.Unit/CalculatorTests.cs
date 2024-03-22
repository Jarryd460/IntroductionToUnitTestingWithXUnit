using FluentAssertions;

namespace TestingTechniques.Tests.Unit;

public sealed class CalculatorTests
{
    private readonly Calculator _calculator = new Calculator();

    [Theory]
    [InlineData(5, 5, 10)]
    [InlineData(-5, 5, 0)]
    [InlineData(-15, -5, -20)]
    public void Add_Should_AddTwoNumbers_When_TwoNumbersAreIntegers(int a, int b, int expected)
    {
        // Act
        var result = _calculator.Add(a, b);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(5, 5, 0)]
    [InlineData(15, 5, 10)]
    [InlineData(-5, -5, 0)]
    [InlineData(-15, -5, -10)]
    [InlineData(5, 10, -5)]
    public void Subtract_Should_SubtractTwoNumbers_When_TwoNumbersAreIntegers(int a, int b, int expected)
    {
        // Act
        var result = _calculator.Subtract(a, b);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(5, 5, 25)]
    [InlineData(50, 0, 0)]
    [InlineData(-5, 5, -25)]
    public void Multiply_Should_MultiplyTwoNumbers_When_TwoNumbersAreIntegers(int a, int b, int expected)
    {
        // Act
        var result = _calculator.Multiply(a, b);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(5, 5, 1)]
    [InlineData(15, 5, 3)]
    public void Divide_Should_DivideTwoNumbers_When_TwoNumbersAreIntegers(int a, int b, int expected)
    {
        // Act
        var result = _calculator.Divide(a, b);

        // Assert
        result.Should().Be(expected);
    }
}