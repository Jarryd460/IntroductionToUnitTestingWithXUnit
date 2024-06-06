namespace AdvancedTechniques.Tests.Unit;

public sealed class MyClassFixture : IDisposable
{
    public Guid Id { get; } = Guid.NewGuid();

    public void Dispose()
    {
        // This method gets call once after 
        // the class has run all it's unit tests.
        // The same goes for the constructor of this class.
        // It is common for integration tests.
    }
}