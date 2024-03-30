[toc]
# FinanceTracker

## Local debugging
### Init Database
**current FinanceTracker only support postgresql** 
Set ConnectionStrings in the appsettings.Development.json
#### In Package Manager Console tools
1. run the script in Package Manager Console tools and the deflect project is FinanceTracker.EntityFramework
    ```power shell
    Add-Migration Init
    ```
2. run the script after success
    ```power shell
    Update-Database
    ```

Remove a migration
```
Remove-Migration
```
#### In .NET Core CLI tools
1. run the script in .NET Core CLI tools
    ```
    dotnet ef migrations
    ```
2. run the script after success
    ```
    dotnet ef database update
    ```

Remove a migration
```
dotnet ef migrations remove
```