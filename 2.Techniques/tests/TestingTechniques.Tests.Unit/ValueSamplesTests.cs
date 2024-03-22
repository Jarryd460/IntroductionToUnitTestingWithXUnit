using FluentAssertions;

namespace TestingTechniques.Tests.Unit;

public sealed class ValueSamplesTests
{
    private readonly ValueSamples _valueSamples = new ValueSamples();

    [Fact]
    public void StringAssertionExample()
    {
        var fullname = _valueSamples.FullName;

        // Even though strings are references type, they are spcial and when comparing them, their values are compared and not the reference
        fullname.Should().Be("Jarryd Deane");
        fullname.Should().NotBeEmpty();
        fullname.Should().StartWith("Jarryd");
        fullname.Should().EndWith("Deane");
    }

    [Fact]
    public void NumberAssertionExample()
    {
        var age = _valueSamples.Age;

        age.Should().Be(31);
        age.Should().BePositive();
        age.Should().BeGreaterThan(30);
        age.Should().BeLessThanOrEqualTo(31);
        age.Should().BeInRange(18, 60);
    }

    [Fact]
    public void DateAssertionExample()
    {
        var dateOfBirth = _valueSamples.DateOfBirth;

        dateOfBirth.Should().Be(new(1992, 4, 29));
        dateOfBirth.Should().BeOnOrAfter(new(1992, 1, 1));
        dateOfBirth.Should().BeOnOrBefore(new(1992, 12, 31));
    }

    [Fact]
    public void ObjectAssertionExample()
    {
        var expected = new User()
        {
            FullName = "Jarryd Deane",
            DateOfBirth = new(1992, 4, 29),
            Age = 31
        };

        var user = _valueSamples.AppUser;

        user.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void EnumerableObjectAssertionExample()
    {
        var expected = new User()
        {
            FullName = "Jarryd Deane",
            DateOfBirth = new(1992, 4, 29),
            Age = 31
        };

        var users = _valueSamples.Users.As<User[]>();

        users.Should().ContainEquivalentOf(expected);
        users.Should().HaveCount(3);
        users.Should().Contain(x => x.FullName.StartsWith("Jarryd") && x.Age > 5);
    }

    [Fact]
    public void EnumerableNumbersAssertionExample()
    {
        var numbers = _valueSamples.Numbers.As<int[]>();

        numbers.Should().Contain(5);
    }

    [Fact]
    public void ExceptionThrownAssertionExample()
    {
        var calculator = new Calculator();

        // We should not test private methods, we test the public methods that call them
        Action result = () => calculator.Divide(1, 0);

        result.Should()
            .Throw<DivideByZeroException>()
            .WithMessage("Attempted to divide by zero.");
    }

    [Fact]
    public void EventRaisedAssertionExample()
    {
        var monitorSubject = _valueSamples.Monitor();

        _valueSamples.RaiseExampleEvent();

        monitorSubject.Should().Raise("ExampleEvent");
    }

    [Fact]
    public void TestingInternalMembersExample()
    {
        var number = _valueSamples.InternalSecretNumber;

        number.Should().Be(1);
    }
}
