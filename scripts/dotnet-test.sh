#!/bin/bash

# Generates file for code coverage pipes in vs code
# Along with displaying unit test coverage per assembly
# https://github.com/tonerdo/coverlet

DIR="$(cd "$(dirname "$0")" && pwd)/.."

dotnet test $DIR/test/Business.Test/

