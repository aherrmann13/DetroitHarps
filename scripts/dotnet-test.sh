#!/bin/sh

dotnet test api/test/Business.Test /p:CollectCoverage=true /p:CoverletOutputFormat=json

dotnet test api/test/Repository.Test /p:CollectCoverage=true /p:CoverletOutputFormat=json