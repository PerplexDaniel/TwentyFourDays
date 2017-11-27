# TwentyFourDays
This project shows how custom attributes (e.g. UmbRequired and UmbDisplayName) can be used
to fetch validation error messages and display names from Umbraco and use those in client and server side validation.

# Installation
Open the .sln file and build, make sure NuGet packages are restored.

## .NET Framework 4.7.1
This projects targets the [.NET Framework 4.7.1](https://www.microsoft.com/en-us/download/details.aspx?id=56119). If you cannot or do not want to install it, make sure to install the System.ValueTuple NuGet package in order to be able to build this project when targeting a lower version of the framework.

```
Install-Package System.ValueTuple
```

## Umbraco Login
```
username: 24days
password: 24days
```

## Custom Database
An SQL CE database is included in this project, and will be used by default. However, if you wish to use the code with a database of your own choice, simply clear the `<connectionStrings>` in web.config and set `umbracoConfigurationStatus` under `<appSettings>` to `""`. Running the solution will then prompt a new installation where you can specify a custom database.

You can then install the provided 24days_content.zip (via `Developer > Packages > Install Local`) to get all the document types, templates, and content.