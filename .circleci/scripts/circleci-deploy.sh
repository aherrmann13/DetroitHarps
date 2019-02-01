#!/bin/bash

DEPLOY_FOLDER="/detroitharps/publish"
if [ "${CIRCLE_BRANCH}" == "dev" ]
then
  IP_ADDRESS="157.230.51.199"
  SSH_ADDR="detroitharps@$IP_ADDRESS"
else
  echo "no settings for branch: ${CIRCLE_BRANCH}"
  exit 1
fi

ssh-keyscan -H $IP_ADDRESS >> ~/.ssh/known_hosts

ssh $SSH_ADDR "if [ -d $DEPLOY_FOLDER ]; then rm -rf $DEPLOY_FOLDER; fi"
ssh $SSH_ADDR "mkdir -p $DEPLOY_FOLDER"
scp -r /root/project/publish/* $SSH_ADDR:$DEPLOY_FOLDER/
ssh $SSH_ADDR $COMMAND

exit 0