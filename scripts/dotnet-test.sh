#!/bin/sh

dotnet test api/test/Business.Test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov

dotnet test api/test/DataAccess.Test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov