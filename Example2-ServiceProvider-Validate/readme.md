
## References

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1#options-validation

Example:

    .Validate(o => YourValidationShouldReturnTrueIfValid(o), 
        "custom error");

