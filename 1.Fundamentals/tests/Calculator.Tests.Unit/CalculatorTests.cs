
namespace Calculator.Tests.Unit;


/// <summary>
/// Each unit test instantiates a new instance of <see cref="CalculatorTests" />
/// </summary>
public sealed class CalculatorTests : IDisposable, IAsyncLifetime
{
    private readonly Guid _guid;

    // Used to setup anything before running unit tests
    public CalculatorTests()
    {
        _guid = Guid.NewGuid();
    }

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
        var result = calculator.Subtract(10, 5);

        // Assert
        Assert.Equal(5, result);
    }

    [Theory(/*Skip = "Can skip can theory level also"*/)]
    [InlineData(5, 2, 10, Skip = "This is just a test")]
    [InlineData(3, 2, 6)]
    [InlineData(0, 2, 0)]
    public void Multiply_Should_MultiplyTwoNumbers_When_TwoNumbersAreIntegers(int a, int b, int expected)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Multiply(a, b);

        // Assert
        Assert.Equal(expected, result);
    }

    // Used to cleanup any resources
    public void Dispose()
    {
        Console.WriteLine("Cleaning up code!!!");
    }

    // Used to setup anything asynchronously before running unit tests
    public async Task InitializeAsync()
    {
        await Task.Delay(1000);
        Console.WriteLine("Running setup asynchronously");
    }

    // Used to cleanup any resources asynchronously
    public Task DisposeAsync()
    {
        Console.WriteLine("Cleaning up code asynchronously");

        return Task.CompletedTask;
    }
}