# ais-2025
Arhitektura informacionih sistema 2025
---
- Potrebni alati i razvojna okruzenja za rad na demo projektu

1. Visual Studio 2022 (https://visualstudio.microsoft.com/vs/community/)
2. Microsoft SQL Server Express (https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
 - Microsoft SQL Management Studio (https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
3. .NET 8 (https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
---

- Entity Framework Core - (Package Manager Console - PMC) komande

Spisak i opis svih komandi:

https://www.learnentityframeworkcore.com/migrations/commands/pmc-commands

- Reverse enginering (generisanje modela i Database Context-a na osnovu baze podataka):

<code>Scaffold-DbContext 'Server=.\SQLExpress;Database=WebShop;Trusted_Connection=True;TrustServerCertificate=True' Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models</code>

[-force] - parametar u slučaju da želimo ponovo generisati i prepisati postojeće fajlove

Add-Migration // Adds a new migration.<br>
Remove-Migration // Removes the last migration.<br>
Scaffold-DbContext // Scaffolds a DbContext and entity type classes for a specified database.<br>
Script-Migration // Generates a SQL script from migrations.<br>
Update-Database // Updates the database to a specified migration.<br>
Use-DbContext // Sets the default DbContext to use.

---