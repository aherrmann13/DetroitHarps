dotnet build ../api/src/Repository.Migrator/Repository.Migrator.csproj -c Debug -o ~/dist/

cd ~/dist/
dotnet Repository.Migrator.dll Delete

dotnet Repository.Migrator.dll Migrate