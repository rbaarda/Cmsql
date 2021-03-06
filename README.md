# CMSQL - CMS Query Language
CMSQL (CMS Query Language) is a DSL specifically designed to query over tree structured data/content in Content Management Systems.
At this point there is only an [implementation](https://github.com/rbaarda/Cmsql.EpiServer) for the [EPiServer CMS](https://www.episerver.com/) available which should be considered as a Proof Of Concept.

At this point the CMS Query Language is very minimalistic and offers basic query capabilities.

## Goal
CMSQL is trying to answer questions like *"Can you show me all news articles created by mr. X"* or *"Can you show me all product pages in category X"* et cetera. It tries to answer such question by means of a simple easy to learn query language. It was designed with content management systems in mind that work with content types and which structure their site content in a hierarchical way, for example systems like [Umbraco](https://umbraco.com/) or [EPiServer](https://www.episerver.com/).

The initial idea is that content-editors and developers should have a simple and easy to learn query language under their fingers available to quickly get that information they are looking for and probably want to edit, especially in large websites with huge content trees. The query language can be exposed to content-editors through some sort of editor in the CMS backend system.

## Getting started
On its own the Cmsql package can parse Cmsql queries but it needs a specific implementation to execute them.
You can use this package on itself if your project needs a Cmsql parser or if you're working on a specific implementation for Cmsql.

### Installation
You can install the NuGet package by running the following command (Please note that this is a pre-release).

`Install-Package Cmsql -Version 1.0.0-alpha1`

### Usage
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

## The query syntax
A query exists out of three parts, illustrated by the following grammar:

`select <CONTENT-TYPE> from <STARTING-POINT> [where <PROPERTY CONDITIONS>][;]`

Multiple queries can be parsed and run in one go when separated by a semicolon `;`.

* **\<CONTENT-TYPE>:** A content type (for now you can only query/search for one specific type). This is nothing more than a string literal, for example common things like: ProductPage, NewsDetailPage, BlogPage etc.
* **\<STARTING-POINT>:** A starting point in the content tree to start searching from. A starting point can be any of the following three values:
  * root: This indicates that a query should start at the root node in the content-tree.
    * Example: `select NewsDetailPage from root`
  * start: This indicates that a query should start at the root/start node of the current website.
    * Example: `select ProductCategoryPage from start`
  * Integer identifier: This can be any number (integer) as long as it matches the ID of some node in the content tree, examples are 123, 2118, 78645.
    * Example: `select BlogPage from 2118`
* **\<PROPERTY CONDITIONS>:** A set of conditions/criteria. Conditions on itself exist out of three parts and have the following structure: `<PROPERTY-NAME> <OPERATOR> <VALUE>`. 
  * **\<PROPERTY-NAME>:** A property name that should exist on the given `<CONTENT-TYPE>`. This is a string literal, examples are Author, PageName, PublishedDate.
  * **\<OPERATOR>:** Operators take any of the following forms:
    * = (Equals)
    * != (Not equals)
    * \> (Greater than)
    * \< (Less than)
    * \>= (Greater than or equals)
    * \<= (Less than or equals)
  * **\<VALUE>:** A string literal.

### Examples
* `select test from start where foo = 'bar'`
* `select test from root where foo = 'bar'`
* `select test from 12345 where foo = 'bar'`
* `select test from 12345 where (foo = 'bar')`
* `select test from 12345 where foo = 'bar' and foo = 'bar';`
* `select test from 12345 where foo = 'bar' or foo = 'bar';`
* `select test from 12345 where (foo = 'bar' and foo = 'bar');`
* `select test from 12345 where (foo = 'bar' or foo = 'bar');`
* `select test from 12345 where (foo = 'bar' and foo = 'bar') or (foo = 'bar' and foo = 'bar');`
* `select test from 12345 where (foo = 'bar' and foo = 'bar') and (foo = 'bar' and foo = 'bar')`
* `select test from 12345 where ((foo = 'bar' and foo = 'bar') and (foo = 'bar' and foo = 'bar'))`
* `select test from start;select foo from root where name = 'test'`