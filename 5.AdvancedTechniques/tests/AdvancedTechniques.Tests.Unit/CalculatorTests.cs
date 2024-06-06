namespace AdvancedTechniques.Tests.Unit;

public sealed class CalculatorTests
{
    private readonly Calculator _calculator = new Calculator();

    [Theory]
    [MemberData(nameof(AddTestData))]
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

    public static IEnumerable<object[]> AddTestData =>
        new List<object[]>()
        {
            new object[] { 5, 5, 10 },
            new object[] { -5, 5, 0 },
            new object[] { -15, -5, -20 }
        };
}