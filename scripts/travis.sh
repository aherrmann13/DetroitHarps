case "$TRAVIS_BRANCH" in
  "master")
    ;;
  "client")
    ./scripts/travis-client.sh
    ;;    
esac