
## TODO : make this less "magic stringy", maybe take "/deploy/api" as parameter?

dotnet /deploy/api/Repository.Migrator/Repository.Migrator.dll Delete

dotnet /deploy/api/Repository.Migrator/Repository.Migrator.dll Migrate

dotnet /deploy/api/Repository.Dataloader/Repository.Dataloader.dll

dotnet cp -r /deploy/api/Service /detroitharps/api/Service