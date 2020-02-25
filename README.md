# DotNetCore-ConfigurationOptionsValidationExamples

Examples of IOptions&lt;T> validation in .NET Core

## Examples

The Options pattern in .NET Core has a few [different ways of validating configuration/options](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1#options-validation).  The solutions in this repository attempt to explore the different approaches and the good/bad of each.

### Example 1: ValidateDataAnnotations()

Uses the [Microsoft Data Annotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1) approach of attribute-based validation on the C# model that represents the section in the configuration.

### Example 2: Validate()

Uses the `.Validate()` method and custom validation methods on the C# classes.  Note that use of a marker/trait interface is not required, but it made it easier for me to call the validation method from within a generic method.

