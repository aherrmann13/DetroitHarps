cd ../api/src/Repository.Migrator

dotnet ef database update 0

dotnet ef migrations remove

dotnet ef migrations add init

dotnet ef database update