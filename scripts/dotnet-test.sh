#!/bin/bash

# Generates file for code coverage pipes in vs code
# Along with displaying unit test coverage per assembly
# https://github.com/tonerdo/coverlet

DIR="$(cd "$(dirname "$0")" && pwd)/.."

$DIR/scripts/dotnet-build.sh

dotnet test $DIR/test/Tools.Test/

dotnet test $DIR/test/Business.Test/

dotnet test $DIR/test/DataAccess.Test/

dotnet test $DIR/test/Repository.Test/

dotnet test $DIR/test/Api.Test/