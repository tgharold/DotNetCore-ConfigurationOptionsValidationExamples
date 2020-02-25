# DotNetCore-ConfigurationOptionsValidationExamples

Examples of IOptions&lt;T> validation in .NET Core

## Table of Contents

- [DotNetCore-ConfigurationOptionsValidationExamples](#dotnetcore-configurationoptionsvalidationexamples)
  - [Table of Contents](#table-of-contents)
- [Examples](#examples)
  - [Example 1: ValidateDataAnnotations()](#example-1-validatedataannotations)
    - [General](#general)
  - [Example 2: Validate()](#example-2-validate)
    - [General](#general-1)
- [Notes](#notes)

# Examples

The Options pattern in .NET Core has a few [different ways of validating configuration/options](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1#options-validation).  The solutions in this repository attempt to explore the different approaches and the good/bad of each.

The main differences between the approaches to validation takes place in the `ConfigureAndValidateSection<T>()` method in the `IServiceCollectionExtensions` class.

## Example 1: ValidateDataAnnotations()

Uses the [Microsoft Data Annotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1) approach of attribute-based validation on the C# model that represents the section in the configuration.

### General

- Validation will execute the first time that the instance is accessed (see the WeatherForecastController constructor).

## Example 2: Validate()

Uses the `.Validate()` method and custom validation methods on the C# classes.  Note that use of a marker/trait interface is not required, but it made it easier for me to call the validation method from within a generic method.

### General

- Validation will execute the first time that the instance is accessed (see the WeatherForecastController constructor).

# Notes

- There is [discussion about an "eager" validation routine](https://github.com/dotnet/extensions/issues/459) in .NET Core, but it has not yet been added.
- The [comment on StackOverflow by "poke"](https://stackoverflow.com/a/51693303) explains some of the trade-offs to consider when talking about configuration / options validation.

