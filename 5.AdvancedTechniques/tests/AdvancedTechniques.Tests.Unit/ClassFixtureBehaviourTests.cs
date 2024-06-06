using Xunit.Abstractions;

namespace AdvancedTechniques.Tests.Unit;

public sealed class ClassFixtureBehaviourTests : IClassFixture<MyClassFixture>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly MyClassFixture _myClassFixture;

    public ClassFixtureBehaviourTests(ITestOutputHelper testOutputHelper, MyClassFixture myClassFixture)
    {
        _testOutputHelper = testOutputHelper;
        _myClassFixture = myClassFixture;
    }

    [Fact]
    public async Task ExampleTest1()
    {
        _testOutputHelper.WriteLine($"The Guid was: { _myClassFixture.Id }");
        await Task.Delay(2000);
    }

    [Fact]
    public async Task ExampleTest2()
    {
        _testOutputHelper.WriteLine($"The Guid was: { _myClassFixture.Id }");
        await Task.Delay(2000);
    }
}