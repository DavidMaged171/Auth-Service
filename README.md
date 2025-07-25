# Auth-Service
Authentication Service Using ASP.net C#

## Prerequisites

Before running this project, make sure you have the following installed:

1. **Visual Studio 2022**

2. **.NET 6 SDK**  
   - Download from: https://dotnet.microsoft.com/en-us/download/dotnet/6.0

3. **Microsoft SQL Server**  
   - SQL Server 2017 or higher is recommended  
   - SQL Server Management Studio (SSMS) is optional but helpful for DB management  
   - Ensure the SQL Server instance is running and accessible

## How to run
1. In appsetting.json confirm that the connectionstring is valid to your machine.
2. build the project.
3. Run this command to VS CLI "dotnet ef database update --project ../Auth.Infrastructure --startup-project ../AuthApi"
4. A database Schema will be created with one user as an admin, 3 roles(admin, Buyer, seller)
5. run the api.
