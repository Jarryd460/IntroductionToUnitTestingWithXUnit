namespace Calculator.Tests.Unit;

public sealed class CalculatorTests
{
    [Fact]
    public void Add_Should_AddTwoNumbers_When_TwoNumbersAreIntegers()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(5, 4);

        // Assert
        Assert.Equal(9, result);
    }
}