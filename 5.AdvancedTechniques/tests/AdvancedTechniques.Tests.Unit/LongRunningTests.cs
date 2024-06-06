namespace AdvancedTechniques.Tests.Unit;

public sealed class LongRunningTests
{
    [Fact(Timeout = 2000, Skip = "This test is made to fail to demonstrate timeout")]
    public async Task SlowTest()
    {
        await Task.Delay(10000);
    }
}