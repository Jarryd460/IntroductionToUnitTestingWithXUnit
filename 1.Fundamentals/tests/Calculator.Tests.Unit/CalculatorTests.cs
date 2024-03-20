namespace Calculator.Tests.Unit;


/// <summary>
/// Each unit test instantiates a new instance of <see cref="CalculatorTests" />
/// </summary>
public sealed class CalculatorTests
{
    private readonly Guid _guid = Guid.NewGuid();

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

    [Fact]
    public void Subtract_Should_SubtractTwoNumbers_When_TwoNumbersAreIntegers()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Subtract(10, 4);

        // Assert
        Assert.Equal(6, result);
    }
}