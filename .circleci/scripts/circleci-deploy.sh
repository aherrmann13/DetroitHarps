#!/bin/bash

DEPLOY_FOLDER="/detroitharps/publish"
if [ "${CIRCLE_BRANCH}" == "dev" ]
then
  PUBLIC_KEY="AAAAB3NzaC1yc2EAAAADAQABAAABAQCaXdHcq9Ydf4H9E1jw9ydFKG81TKf5+P5LPMQmTR9zuvHSlm/4ZLnPSM+yzi6oceqU/QbwxhXefi/Mq6DhjNArCnKcWND1ty04uDiF7/XwqGDxWy8HWW1thji8CH+mxWt3TZoI11ZBGYr+O+0+O8Chp0vAAUueggeygKOwJVXswO7kaJGM2RVMoRpZholi8Pj1fcoAitwoW3Gjzw1005GQcTG7w0j5Nrtvd3Gnrlznyql80TWxpSF6UGlxqjbM+r1EzIwwe8XmeaIg/DqeNWii28w+seknj3IKtRGAAqJxeue3HCQ35xbpTZLykR9bTgHIpY4WjnQebn+BT36S5sod"
  SSH_ADDR="detroitharps@157.230.51.199"
else
  echo "no settings for branch: ${CIRCLE_BRANCH}"
  exit 1
fi

echo $PUBLIC_KEY >> ~/.ssh/known_hosts

ssh $SSH_ADDR "if [ -d $DEPLOY_FOLDER ]; then rm -rf $DEPLOY_FOLDER; fi"
ssh $SSH_ADDR "mkdir -p $DEPLOY_FOLDER"
scp -r /root/project/publish/* $SSH_ADDR:$DEPLOY_FOLDER/
ssh $SSH_ADDR $COMMAND

exit 0