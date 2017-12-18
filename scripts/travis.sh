case "$TRAVIS_BRANCH" in
  "master")
    ;;
  "client")
    travis-client.sh
    ;;    
esac