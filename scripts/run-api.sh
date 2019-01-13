#!/bin/bash

# Builds with warn as error set to true
DIR="$(cd "$(dirname "$0")" && pwd)/.."

cd $DIR/src/Api/

dotnet run 

