# IntroductionToUnitTestingWithXUnit 

### Description
An introduction to unit testing with xunit following [Nick Chapsas: Dometrain - From Zero to Hero Unit testing for](C#https://dometrain.com/course/from-zero-to-hero-unit-testing-in-c/).

### Dependencies:
* Microsoft.NET.Test.Sdk
* xunit
* xunit.runner.visualstudio

### Projects

##### Fundamentals
Learn the basics of unit testing such as:
* How to write a unit test.
* How to run the same unit test with different inputs and expected results.
* How to skip a unit test.
* How to setup and dispose of resources when running unit test.
* Naming convention.
* Structuring of solution.

##### TestingTechniques
Writing unit tests for testing strings, numbers, dates, objects, enumerables, 
thrown exceptions, raised events and private and internal methods.

##### Concepts
Learn how to test methods that have dependencies using Fakers and Mocking with
NSubstitute.
	* NSubstitute is prefered as it's syntax is less convoluted.

##### RealWorld
Learn how to test service layer and static and extention methods such 
as logging by creating an Adapter.
* We should not be writing unit tests for the repository layer if all we doing 
is getting the data from the repository
* We should also not be writing unit tests for controllers if we just passing the
data to the client.
	* You could write unit tests to check the appropriate status code and data is passed
	back to the client.
* The 2 above points should be covered with integration tests.

##### AdvancedTechniques
Learn about more advanced topics in unit testing such as:
* Default code excution
	* By default test cases in a class execute sequentially and classes themselves execute in parallel. Data is not shared among test cases and they each get their own instance of the class and it's data.
* Class Fixtures
	* Declaring a fixure allows data to be shared across unit tests. This is good in scenarios where you want to start and tear down a database for instance.
* Collection Fixtures
	* A collection fixture allows you to share data across not just unit tests in a class but across classes themselves.
* CollectionBehavior
	* CollectionBehaviour is an attribute which allows you to set the degree of parallelism of your unit tests. This means that you can set it to run all unit tests sequentially.
* MemberData
	* Instead of using InlineData, you could use member data which allows you to return a dynamic IEnumerable which willl be consumed by unit test method.
* ClassData
	* Like MemberData, allows one to dynamically specify data to be used by unit tests for testing SUT (system under test). You could use a Faker such as Bogus to dynamically generate data.
* Code coverage
	* Using the coverlet console dotnet tool, you can collection the code coverage of your code.
	* Install tool by running "dotnet tool install -g coverlet.console"
	* Run "coverlet bin/Debug/net8.0/AdvancedTech
niques.Tests.Unit.dll --target "dotnet" --targetargs "test
 --no-build""
		* Besides outputting the results to the console, you will see that a coverage.json file containing the code coverage data has also been generated in the same directory.
	* You can also exclude certain namespaces from the coverage by running: "coverlet bin/Debug/net8.0/AdvancedTechniques.Tests.Unit.dll --target "dotnet" --targetargs "test --no-build" --exclude "[*]AdvancedTechniques*""
	* Adjust the above accoring to your directory you executing from and the project you want to collect coverage for.
	* With the coverlet.collector package installed, we can generate a json file in the same directory with the collection coverage data in another format (cobertura) and use that to generate a UI off it using Report Generator.
		* Run dotnet test --collect:"XPLAT Code Coverage"
		* Install Report Generator: dotnet tool install -g dotnet-reportgenerator-globaltool
		* Generate Report: reportgenerator -reports:"TestResults/4681a5ec-d8d3-4a18-912c-a813914e5a9c/coverage.cobertura.xml" -targetdir:"codecoverage" -reporttypes:"html"
