#!/bin/bash

# Builds with warn as error set to true
DIR="$(cd "$(dirname "$0")" && pwd)/.."

dotnet clean && dotnet build $DIR /warnaserror

