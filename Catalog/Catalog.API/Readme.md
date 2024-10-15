
# SCRIPTS
Add a new migration to the database.
in the Catalog folder run the following commands.

```bash
dotnet ef migrations add <migration-name> -p ./Catalog.Infrastructure --startup-project ./Catalog.API
```

Apply the migrations to the database.

```bash
dotnet ef database update -p ./Catalog.Infrastructure --startup-project ./Catalog.API
```