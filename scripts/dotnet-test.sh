#!/bin/sh

dotnet test api/test/Business.Test /p:CollectCoverage=true /p:CoverletOutputFormat=json

dotnet test api/test/DataAccess.Test /p:CollectCoverage=true /p:CoverletOutputFormat=json