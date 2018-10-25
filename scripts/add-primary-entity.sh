#!/bin/bash

## quick way to stub out the typical folder structure for a new business entity

entityName=$1
dir="$(cd "$(dirname "$0")" && pwd)/.."

if [[ $entityName == "" ]] ; then

  echo "please provide name for entity"
  exit 0

fi

if test -d $dir/src/Business/$entityName ; then

  echo "directory already exists"
  exit 0

fi

mkdir $dir/src/Business/$entityName
mkdir $dir/src/Business/$entityName/Entities
mkdir $dir/src/Business/$entityName/Models

entityFileName=$entityName".cs"
managerFileName=$entityName"Manager.cs"
managerInterfaceFileName="I"$managerFileName
modelFileName=$entityName"Model.cs"
profileFileName=$entityName"Profile.cs"

touch $dir/src/Business/$entityName/$managerFileName
touch $dir/src/Business/$entityName/$managerInterfaceFileName
touch $dir/src/Business/$entityName/$profileFileName

touch $dir/src/Business/$entityName/Entities/$entityFileName
touch $dir/src/Business/$entityName/Models/$modelFileName
