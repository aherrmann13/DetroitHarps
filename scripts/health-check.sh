#!/bin/bash

# Builds with warn as error set to true
URL=$1

RESPONSE=$(curl -s $URL)

if [ $RESPONSE == "Healthy" ]
then
    exit 0
else
    echo $RESPONSE
    exit 1
fi

