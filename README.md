# Sample .NET Core console application with Entity Framework

This is a barebones .NET Core console app with IoC, logging, configuration and EntityFramework configured. 
It also has a very simple sample DbContext (`BookLibraryContext`) and an initial migration.

Treat this as a starting point, a template to help you get started with EF Core without the hassle of 
getting everything set up which involves a surprising number of tiresome steps.

The main code should be placed in the `App.Run` method. The App class is created using the IoC 
container so any dependencies will be injected in the constructor (by default, the configuration
and `BookLibraryContext` are passed, add your own as required)

IoC can be configured in the `Program.AddCustomDependencies` method.

## Pre-requisites

- If it's not already done, install the EF Core CLI tools
- In a Developer command prompt run this command :

```
dotnet tool install --global dotnet-ef
```

## Creating the initial database

- The default connection string is `Server=.;Database=BookLibrary;Integrated Security=true`
  - It connects to a local sql server running on the current machine
  - It uses Windows Authentication
  - It looks for a database called BookLibrary
- If you need to, change it in `appsettings.json` to whatever works for you
- Once this is done, in a Developer command prompt 
	- Go to the in the project folder
	- Run the following command to initialize the database

```
dotnet ef database update
```
