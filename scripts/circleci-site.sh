#!/bin/bash

if [ "${CIRCLE_BRANCH}" == "client" ]
then
  PUBLIC_KEY="|1|Zj1eT2OEObP2Ovh83T7dKNqaIqA=|lqu/pS4Wey1FHtQho7kFy2NJYJs= ecdsa-sha2-nistp256 AAAAE2VjZHNhLXNoYTItbmlzdHAyNTYAAAAIbmlzdHAyNTYAAABBBFmUQu3aTDNgYdhAtuUDQxq+OLFHAQ0Y6XCtpam+ZNZzos4p7s7MBbjPsyQeKKiUimwn11C3Xd8tednO8TikJOg="
  SSH_ADDR="root@45.33.89.196"
  BUILD_COMMAND="npm run build-dev -- --aot"
  DEPLOY_COMMAND="/deploy/client/scripts/deploy-site.sh dev"
elif [ "${CIRCLE_BRANCH}" == "master" ]
then
  PUBLIC_KEY="|1|n8BQP+Cd3BD+v2YCU26bDGQYHW8=|L9BkYNWDJYj0sNYMKawfxW0AYG4= ecdsa-sha2-nistp256 AAAAE2VjZHNhLXNoYTItbmlzdHAyNTYAAAAIbmlzdHAyNTYAAABBBHxOFi9vhcyZzffD8GcKbaqXKSh/wmKn+i/noqwTt3tcodaHkCnljZsruRQVrXWOtXz3ZWjUtfE0mOkaMZpnXAQ="
  SSH_ADDR="root@50.116.62.220"
  BUILD_COMMAND="npm run build -- --aot --prod"
  DEPLOY_COMMAND="/deploy/client/scripts/deploy-site.sh"
else
  echo "no settings for branch: ${CIRCLE_BRANCH}"
fi

cd client
$BUILD_COMMAND
cd ..

echo $PUBLIC_KEY >> ~/.ssh/known_hosts

ssh $SSH_ADDR "if [ -d /deploy/client ]; then rm -rf /deploy/client; fi"
ssh $SSH_ADDR "mkdir -p /deploy/client"
scp -r client/dist $SSH_ADDR:/deploy/client/
scp -r scripts $SSH_ADDR:/deploy/client/
ssh $SSH_ADDR $DEPLOY_COMMAND

exit 0