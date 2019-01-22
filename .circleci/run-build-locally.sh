#!/usr/bin/env bash
COMMIT_HASH=$(git ls-remote git://github.com/aherrmann13/DetroitHarps.git refs/heads/dev | cut -f 1)
curl --user ${CIRCLE_TOKEN}: \
    --request POST \
    --form revision=$COMMIT_HASH\
    --form config=@config.yml \
    --form notify=false \
        https://circleci.com/api/v1.1/project/github/aherrmann13/DetroitHarps/tree/dev