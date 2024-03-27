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