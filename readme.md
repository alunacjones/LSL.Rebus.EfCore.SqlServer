[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-rebus-efcore-sqlserver.svg)](https://ci.appveyor.com/project/alunacjones/lsl-rebus-efcore-sqlserver)
[![Coveralls](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.Rebus.EfCore.SqlServer)](https://coveralls.io/github/alunacjones/LSL.Rebus.EfCore.SqlServer)
[![NuGet](https://img.shields.io/nuget/v/LSL.Rebus.EfCore.SqlServer.svg)](https://www.nuget.org/packages/LSL.Rebus.EfCore.SqlServer/)

# LSL.Rebus.EfCore.SqlServer

This package provides `ModelBuilder` extension methods to allow you to produce the tables expected by the [Rebus.SqlServer package](https://www.nuget.org/packages/Rebus.SqlServer/)

>NOTE: You will still need to create migrations to ensure the tables get created. Please see [here](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli) for further details.


## Sagas

To add the required entities to your `DbContext` then following should be added to `OnModelCreating` as below:

```csharp
    using LSL.Rebus.EfCore.SqlServer;
    ...
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ...
        modelBuilder.AddRebusSagaTablesForSqlServer();
        ...
    }
```

The default table names are `Sages` and `SagaIndex`. To use custom table names pass them into the call as below:

```csharp
    using LSL.Rebus.EfCore.SqlServer;
    ...
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ...
        modelBuilder.AddRebusSagaTablesForSqlServer("CustomSagaTable", "CustomSagaIndexTable");
        ...
    }
```

## Outbox

To add the required entities to your `DbContext` then following should be added to `OnModelCreating` as below:

```csharp
    using LSL.Rebus.EfCore.SqlServer;
    ...
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ...
        modelBuilder.AddRebusOutboxTableForSqlServer();
        ...
    }
```

The default table name is `Outbox`. To use a custom table name pass the table name into the call as below:

```csharp
    using LSL.Rebus.EfCore.SqlServer;
    ...
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ...
        modelBuilder.AddRebusOutboxTableForSqlServer("CustomOutboxTable");
        ...
    }
```

## Timeouts

To add the required entities to your `DbContext` then following should be added to `OnModelCreating` as below:

```csharp
    using LSL.Rebus.EfCore.SqlServer;
    ...
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ...
        modelBuilder.AddRebusTimeoutTableForSqlServer();
        ...
    }
```

The default table name is `Timeouts`. To use a custom table name pass the table name into the call as below:

```csharp
    using LSL.Rebus.EfCore.SqlServer;
    ...
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ...
        modelBuilder.AddRebusTimeoutTableForSqlServer("CustomTimeoutsTable");
        ...
    }
```
