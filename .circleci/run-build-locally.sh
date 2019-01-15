#!/usr/bin/env bash
COMMIT_HASH="caab2768ce7b88dd3625fda68303c52cc65e6540"
curl --user ${CIRCLE_TOKEN}: \
    --request POST \
    --form revision=$COMMIT_HASH\
    --form config=@config.yml \
    --form notify=false \
        https://circleci.com/api/v1.1/project/github/aherrmann13/DetroitHarps/tree/dev