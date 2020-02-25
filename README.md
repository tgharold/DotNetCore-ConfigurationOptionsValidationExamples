# DotNetCore-ConfigurationOptionsValidationExamples

Examples of IOptions&lt;T> validation in .NET Core

## Table of Contents

- [DotNetCore-ConfigurationOptionsValidationExamples](#dotnetcore-configurationoptionsvalidationexamples)
  - [Table of Contents](#table-of-contents)
- [General](#general)
- [Examples](#examples)
  - [Example 1: ValidateDataAnnotations()](#example-1-validatedataannotations)
    - [General](#general-1)
    - [Pros/Cons](#proscons)
  - [Example 2: Validate()](#example-2-validate)
    - [General](#general-2)
    - [Pros/Cons](#proscons-1)
- [Reference Notes](#reference-notes)

# General

The Options pattern in .NET Core has a few [different ways of validating configuration/options](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1#options-validation).  The solutions in this repository attempt to explore the different approaches and the good/bad of each.

In both of the existing .NET Core approaches (example 1 and 2), validation of the options object instance does not happen until the first usage.  

One approach to dealing with the lazy-evaluation of valdiation rules would be to add those `IOptions<T>` as parameters to the `Startup.Configure()` and then instantiate a copy of every options object.  This would give you a way to validate that anything in the `appsettings*.json` files (or environment variables) injected at startup are correct.

TODO: Dealing with configuration that changes during run-time.

# Examples

The main differences between the approaches to validation takes place in the `ConfigureAndValidateSection<T>()` method in the `IServiceCollectionExtensions` class.

## Example 1: ValidateDataAnnotations()

Uses the [Microsoft Data Annotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1) approach of attribute-based validation on the C# model that represents the section in the configuration.

### General

- Validation will execute the first time that the instance is accessed (see the WeatherForecastController constructor).

### Pros/Cons

- Pro: The data annotation approach gives back a full list of all validation that failed.
- Con: Data annotation via attributes quickly gets complicated when you have fields relying on each other.

## Example 2: Validate()

Uses the `.Validate()` method and custom validation methods on the C# classes.  Note that use of a marker/trait interface is not required, but it made it easier for me to call the validation method from within a generic method.

### General

- Validation will execute the first time that the instance is accessed (see the WeatherForecastController constructor).

### Pros/Cons

- Con: It is harder to structure a useful message / object back to the caller.

# Reference Notes

- There is [discussion about an "eager" validation routine](https://github.com/dotnet/extensions/issues/459) in .NET Core, but it has not yet been added.
- The [comment on StackOverflow by "poke"](https://stackoverflow.com/a/51693303) explains some of the trade-offs to consider when talking about configuration / options validation.

