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
  - [Example 3: IValidateOptions](#example-3-ivalidateoptions)
    - [General](#general-3)
    - [Pros/Cons](#proscons-2)
- [Reference Notes](#reference-notes)

# General

The Options pattern in .NET Core has a few [different ways of validating configuration/options](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1#options-validation).  The solutions in this repository attempt to explore the different approaches and the good/bad of each.

In all of the demonstrated .NET Core approaches (example 1, 2, and 3), validation of the options object instance does not happen until the first usage.  If the object instance is passed in as `IOptions<T>` then validation only happens on the first access (even if the underlying configuration changes).  On the other hand, if you pass the option object in as `IOptionsSnapshot<T>` then validation happens on every new scope (usually every request if passed into the constructor of the controller).

One approach to dealing with the lazy-evaluation of validation rules would be to add those `IOptions<T>` as parameters to the `Startup.Configure()` and then instantiate a copy of every options object.  This would give you a way to validate that anything in the `appsettings*.json` files (or environment variables) injected at startup are correct.

# Examples

The main differences between the approaches to validation takes place in the `ConfigureAndValidateSection<T>()` method in the `IServiceCollectionExtensions` class.

Because the `DatabaseOptions` object is passed into the `WeatherForecastController` constructor, simply running the project after editing the "`Database`" section of `appsettings.json` will let you experiment.  

## Example 1: ValidateDataAnnotations()

Uses the [Microsoft Data Annotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1) approach of attribute-based validation on the C# model that represents the section in the configuration.

### General

- Validation will not happen until the first time that the instance is accessed (see the WeatherForecastController constructor).

### Pros/Cons

- Pro: The data annotation approach gives back a full list of all validation that failed.
- Pro: Data annotation validation is simple to setup and works well for flat options.
- Con: Data annotation via attributes quickly gets complicated when you have fields relying on each other.
- Con: Data annotation validation does not validate sub-objects (without custom code).

## Example 2: Validate()

Uses the `.Validate()` method and custom validation methods on the C# classes.  Note that use of a marker/trait interface is not required, but it made it easier for me to call the validation method from within a generic method.  The use of a generic method makes it difficult to construct a detailed error message.

If I wasn't using a generic method (ConfigureAndValidateSection), it would be possible to separately validate each property of the Options object.  This would allow per-property validation messages.  An example of this can be seen in the [older aspnet/Options Github repository](https://github.com/aspnet/Options/blob/95495473d26eb30bbd079f20a04b15c9464c49d9/test/Microsoft.Extensions.Options.Test/OptionsBuilderTest.cs#L293-L295).

    .Validate(o => o.Boolean)
    .Validate(Options.DefaultName, o => o.Virtual == null, "Virtual")
    .Validate(o => o.Integer > 12, "Integer");

### General

- Validation will not happen until the first time that the instance is accessed (see the WeatherForecastController constructor).

### Pros/Cons

- Con: It is harder to structure a useful message / object back to the caller.

## Example 3: IValidateOptions

Uses the `IValidateOptions` interface on C# validation classes. See the `DatabaseOptionsValidator` class for an example validator.  This was a really easy to implement approach.

### General

- Validation will not happen until the first time that the instance is accessed (see the WeatherForecastController constructor).

### Pros/Cons

- Pro: Allows you to send back multiple string messages.
- Con: Have to wire up each pair of options class and the associated validator.

# Reference Notes

- There is [discussion about an "eager" validation routine](https://github.com/dotnet/extensions/issues/459) in .NET Core, but it has not yet been added.
- The [comment on StackOverflow by "poke"](https://stackoverflow.com/a/51693303) explains some of the trade-offs to consider when talking about configuration / options validation.

