# CMSQL - CMS Query Language
CMSQL (CMS Query Language) is a DSL specifically designed to query over tree structured data/content in Content Management Systems.
At this point there is only an [implementation](https://github.com/rbaarda/Cmsql.EpiServer) for the [EPiServer CMS](https://www.episerver.com/) available which should be considered as a Proof Of Concept.

At this point the CMS Query Language is very minimalistic and offers basic query capabilities.

## Goal
CMSQL is trying to answer questions like *"Can you show me all news articles created by mr. X"* or *"Can you show me all product pages in category X"* et cetera. It tries to answer such question by means of a simple easy to learn query language. It was designed with content management systems in mind that work with content types and which structure their site content in a hierarchical way, for example systems like [Umbraco](https://umbraco.com/) or [EPiServer](https://www.episerver.com/).

## Installation
You can install the NuGet package by running the following command (Please note that this is a pre-release).

`Install-Package Cmsql -Version 1.0.0-alpha1`

## Usage
On its own the Cmsql package can parse Cmsql queries but it needs a specific implementation to execute them.
You can use this package on itself if your project needs a Cmsql parser or if you're working on a specific implementation for Cmsql.

The Cmsql package contains a `CmsqlQueryService` which is basically a facade that takes care of parsing and executing queries through the `ExecuteQuery` method.
The `ExecuteQuery` method returns an instance of `CmsqlQueryResultSet` which is a composite type that contains information about the parsing and execution process.
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

## CMSQL Syntax
Basically a CMSQL query exists out of three parts:

* A content type (for now you can only query/search for one specific type).
* A starting point in the content tree to start searching from.
* A set of conditions/criteria

`select [CONTENT TYPE] from [QUERY START POINT IN CONTENT TREE] where [PROPERTY CONDITIONS]`