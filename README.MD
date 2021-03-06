# Geldautomaat - WPF Application
This is a school assignment.

## Database set-up
take the ``database-template.sql`` and import it into a schema, the name doesn't matter as you will configure this in the next step!

### Database connection
In [``Library\Database.cs``](https://github.com/Elian0213/Geldautomaat/blob/main/Library/Database.cs) you must change the following variables to your according configuration.

```csharp
private void initClass()
{
    _server = "DATABASE IP";
    _database = "SCHEME NAME";
    _uid = "USERNAME";
    _password = "PASSWORD";
```

### Default accounts

**admin account**
```bash
$ E-Mail: elian@email.nl
$ Pincode: 1234
```


**Gebruikers account**
```bash
$ Rekening nummer: 17073693
$ Pincode: 1707
```

## Requirements
You need the follolwing library(s)

[MySql.Data 8.0.21](https://www.nuget.org/packages/MySql.Data/8.0.21?_src=template)

[SqlKata 2.2.0](https://www.nuget.org/packages/SqlKata/2.2.0?_src=template)

[SqlKata.Execution 2.2.0](https://www.nuget.org/packages/SqlKata.Execution/2.2.0?_src=template)
