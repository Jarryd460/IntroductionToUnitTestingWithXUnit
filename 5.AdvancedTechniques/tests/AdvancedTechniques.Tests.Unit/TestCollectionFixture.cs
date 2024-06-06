namespace AdvancedTechniques.Tests.Unit;

// This allows you to share state across multiple classes and their test cases
[CollectionDefinition("My awesome collection fixture")]
public sealed class TestCollectionFixture : ICollectionFixture<MyClassFixture>
{

}