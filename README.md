# SampleProject

Web API allows to enter date range and get price for if it meets to a requirements.

As a documentation used Swagger based on xml generated document

Rewrite of integration test for better readability.

Extracted Swagger registration into separate extension and redirect user to swagger documentation when navigated to site root url.

Implemented DataProvider for seeding DB Context from json file, Repostiory and Unit of Work patterns.

Updated Api settings for better testing through IIS Express.

Applied some refactoring for the rest sources.

Ford deployment App to Azure or G Cloud you must install the following prerequisits
Azule CLI
Google Cloud SDK
and create or use existing accounts in specific Cloud.
In SampleProject.Api folder you can find two publish script one for Azure and other for GCloud.
