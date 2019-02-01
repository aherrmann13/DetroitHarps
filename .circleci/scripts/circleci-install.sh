#!/bin/bash

DEPLOY_FOLDER="/detroitharps/publish"
RUN_FOLDER="/detroitharps/application"
if [ "${CIRCLE_BRANCH}" == "dev" ]
then
  IP_ADDRESS="157.230.51.199"
  SSH_ADDR="detroitharps@$IP_ADDRESS"
else
  echo "no settings for branch: ${CIRCLE_BRANCH}"
  exit 1
fi

ssh-keyscan -H $IP_ADDRESS >> ~/.ssh/known_hosts

ssh $SSH_ADDR "if [ -d $RUN_FOLDER ]; then rm -rf $RUN_FOLDER; fi"
ssh $SSH_ADDR "mkdir -p $RUN_FOLDER"
ssh $SSH_ADDR "cp -r $DEPLOY_FOLDER $RUN_FOLDER"

exit