# Storm
Eshava.Storm - a simple tentaculated object-relational mapper for .Net

## Features
Eshava.Storm is a [NuGet library](https://nuget.org/packages/Eshava.Storm) that you can add in to your project that will extend your `IDbConnection` interface.

It provides 4 helpers:

Execute a query and map the results to a strongly typed List
------------------------------------------------------------

```csharp
public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, string sql, object parameter = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
```
Example usage:

```csharp
public class Alpha
{
    public Guid? Id { get; set; }
    public string Beta { get; set; }
    public int Gamma { get; set; }
}

var alphas = await connection.QueryAsync<Alpha>("SELECT * FROM alphas WHERE Beta = @Beta", new { Beta = "nonsense" });
```


Execute a query and map the results to a strongly typed List with several sub objects
------------------------------------------------------------

```csharp
public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, string sql, Func<IObjectMapper, T> map, object parameter = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
```
Example usage:

```csharp
public class Alpha
{
    public Guid? Id { get; set; }
    public string Beta { get; set; }
    public int Gamma { get; set; }
    public Guid OmegaId {get; set;}
    public Omega Omega {get; set;}
}

public class Omega
{
    public string Name { get; set; }
}

var query = $@"
SELECT 
     a.*
    ,o.*
FROM alphas a
LEFT OUTER JOIN omegas o ON o.Id = a.OmegaId
WHERE a.Beta = @Beta";

var alphas = await connection.QueryAsync<Alpha>(
    query, 
    mapper => 
    {
        var alpha = mapper.Map<Alpha>("a");
        var omega = mapper.Map<Omega>("o");
        
        alpha.Omega = omega;
        
        return alpha;
    },
    new { Beta = "nonsense" });
```


Execute a Command that returns no results
-----------------------------------------

```csharp
public static Task<int> ExecuteAsync(this IDbConnection connection, string sql, object parameter = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
```
Example usage:

```csharp
public class Alpha
{
    public Guid? Id { get; set; }
    public string Beta { get; set; }
    public int Gamma { get; set; }
}

await connection.ExecuteAsync("UPDATE alpha SET Beta = @Beta WHERE Id = @Id", new { Beta = "nonsense", Id = Guid.NewGuid() });
```


Execute a Command that returns a scalar
-----------------------------------------

```csharp
public static Task<T> ExecuteScalarAsync<T>(this IDbConnection connection, string sql, object parameter = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
```
Example usage:

```csharp
public class Alpha
{
    public Guid? Id { get; set; }
    public string Beta { get; set; }
    public int Gamma { get; set; }
}

await connection.ExecuteAsync("SELECT COUNT(Id) FROM alpha WHERE Beta = @Beta", new { Beta = "nonsense" });
```


Parameterized queries
---------------------

Parameters are passed in as anonymous classes. This allow you to name your parameters easily and gives you the ability to simply cut-and-paste SQL snippets and run them in your db platform's Query analyzer.

```csharp
new { Gamma = 1, Beta = "nonsense" } // Gamma will be mapped to the param @Gamma, Beta to the param @Beta
```

Also supported

```csharp
// Gamma will be mapped to the param @Gamma, Beta to the param @Beta
new List<KeyValuePair<string, object>() 
{ 
    new KeyValuePair<string, object>("Gamma", 1),
    new KeyValuePair<string, object>("Beta", "nonsense")
};
```


List Support
------------

Eshava.Storm allows you to pass in `IEnumerable<int>` and will automatically parameterize your query.

For example:

```csharp
connection.QueryAsync<Guid>("SELECT Id FROM alphas where Gamma in @Gammas", new { Gammas = new int[] { 3, 5, 7 } });
```

Will be translated to:

```sql
SELECT Id FROM alphas where Gamme in (@Gammas_p1, @Gammas_p2, @Gammas_p3)" -- @Gammas_p0 = 3 , @Gammas_p1 = 5 , @Gammas_p2 = 7
```


Owns One Property Support (entity framework behavior)
------------

Eshava.Storm allows you to fill sub objects which are marked as owns one properties.

```csharp
public class Alpha
{
    public Guid? Id { get; set; }
    public string Beta { get; set; }
    public int Gamma { get; set; }
    
    [OwnsOne]
    public Omega Omega {get; set;}
}

public class Omega
{
    public string Name { get; set; }
}
```

Sql query example:

```sql
SELECT * FROM alphas
```

OR

```sql
SELECT  
     Id
    ,Beta
    ,Gamma
    ,Omega_Name
FROM alphas
```


Custom data type handler
------------

Eshava.Storm allows you to define treatments for custom data types or to change the treatment of known data types

```csharp
public interface ITypeHandler
{
    bool ReadAsByteArray { get; }

    void SetValue(IDbDataParameter parameter, object value);
    object Parse(Type destinationType, object value);
}
```

OR

```csharp
public abstract class TypeHandler<T> : ITypeHandler
{
    public abstract void SetValue(IDbDataParameter parameter, T value);
    public abstract T Parse(object value);
}
```

For example:

```csharp
public class DateTimeHandler : TypeHandler<DateTime>
{
    public override void SetValue(IDbDataParameter parameter, DateTime value)
    {
        parameter.Value = value.ToUniversalTime();
    }

    public override DateTime Parse(object value)
    {
        return DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
    }
}
```


## CRUD Operations
Eshava.Storm provides also helpers to simple create, update and delete data objects


Query data
------------

The query method allows you to read a data object by id.
If the data object has an combined key, you can pass an anonymous type object as id parameter.

```csharp
public static Task<T> QueryEntityAsync<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
```


Insert data
------------

The insert method allows you to define the type of the primary key column

```csharp
public static Task<K> InsertAsync<T, K>(this IDbConnection connection, T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
```


Update data
------------

```csharp
public static async Task<bool> UpdateAsync<T>(this IDbConnection connection, T entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
```


Partial update data
------------

The partial update method analyzes the given data type and passed entity, so that only an anonymous object can be parsed

```csharp
    public static async Task<bool> UpdatePartialAsync<T>(this IDbConnection connection, object entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
```

For Example:

```csharp
public class Alpha
{
    public Guid? Id { get; set; }
    public string Beta { get; set; }
    public int Gamma { get; set; }
}

var partialAlpha = new 
{
    Id = Guid.NewGuid(),
    Beta = "nonsense"
};

await connection.UpdatePartialAsync<Alpha>(partialAlpha);
```


Delete data
------------

```csharp
public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
```


Bulk Insert data
------------

```csharp
public static Task BulkInsertAsync<T>(this SqlConnection connection, IEnumerable<T> entitiesToInsert, SqlTransaction transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
```


## Configuration via attributes

* `KeyAttribute`
* `DatabaseGeneratedAttribute`
* `ColumnAttribute`
* `TableAttribute`

There is also a custom attribute to support the entity framework behaviour with owns one objects

* `OwnsOneAttribute`

None of these attributes have to be set.

* For primary key columns, the property name `Id` will be used as fallback
* Table name will be the pluralized data type name 
* As default, the value of primary key columns are treated as manually generated and will be added to the insert query

The attribute `DatabaseGeneratedAttribute` allows you also to mark properties as identity column (need not to be a primary column).


## Configuration via fluid api pattern

Instead of defining the configuration of the classes via attributes, it is possible to control them via an EntityTypeConfiguration
The configuration is based on the behaviour of the Entity Framework Code.

Define:
```csharp
public class AlphaTypeConfiguration : IEntityTypeConfiguration<Alpha>
{
    public void Configure(EntityTypeBuilder<Alpha> builder)
    {
	    ...
    }
}
```

Register:
```csharp
Eshava.Storm.MetaData.TypeAnalyzer.AddType(new AlphaTypeConfiguration());
```
