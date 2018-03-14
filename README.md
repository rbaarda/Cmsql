# CMSQL - CMS Query Language
CMSQL (CMS Query Language) is a DSL specifically designed to query over tree structured data/content in Content Management Systems.
At this point there is only an [implementation](https://github.com/rbaarda/Cmsql.EpiServer) for the [EPiServer CMS](https://www.episerver.com/) available which should be considered as a Proof Of Concept.

At this point the CMS Query Language is very minimalistic and offers basic query capabilities.

## Installation
You can install the NuGet package by running the following command (Please note that this is a pre-release).

```Install-Package Cmsql -Version 1.0.0-alpha1```

## Usage
On its own the Cmsql package can parse Cmsql queries but it needs a specific implementation to execute them.
You can use this package on itself if your project needs a Cmsql parser or if you're working on a specific implementation for Cmsql.

The Cmsql package contains a `CmsqlQueryService` which is basically a facade that takes care of parsing and executing queries through the `ExecuteQuery` method.
The `ExecuteQuery` returns an instance of `CmsqlQueryResultSet` which is a composite type that contains information about the parsing and execution process.
When no errors are encountered and data is found the result set should contain data in the form of a collection of `ICmsqlQueryResult`.

The following (EPiServer specific) example demonstrates how to execute a query, check for errors and get data from the result set.

```csharp
var resultSet = _cmsqlQueryService.ExecuteQuery("select ProductPage from start where PageName = 'Alloy Plan'");
if (!resultSet.ParseResult.Errors.Any() && !resultSet.ExecutionResult.Errors.Any())
{
  var pages = resultSet.ExecutionResult.QueryResults
    .OfType<PageDataCmsqlQueryResult>()
    .Select(p => p.Page)
    .ToList();
}
```

## Syntax
TODO: Documentation about the CMSQL language/syntax...