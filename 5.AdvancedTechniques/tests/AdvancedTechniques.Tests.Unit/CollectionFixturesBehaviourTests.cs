using Xunit.Abstractions;

namespace AdvancedTechniques.Tests.Unit;

[Collection("My awesome collection fixture")]
public sealed class CollectionFixturesBehaviourTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly MyClassFixture _myClassFixture;

    public CollectionFixturesBehaviourTests(ITestOutputHelper testOutputHelper, MyClassFixture myClassFixture)
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

[Collection("My awesome collection fixture")]
public sealed class CollectionFixturesBehaviourTestsAgain
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly MyClassFixture _myClassFixture;

    public CollectionFixturesBehaviourTestsAgain(ITestOutputHelper testOutputHelper, MyClassFixture myClassFixture)
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