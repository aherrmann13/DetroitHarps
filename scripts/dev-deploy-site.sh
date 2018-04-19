service nginx stop

rm -r /detroitharps/client/*

cp -r /deploy/client/dist/* /detroitharps/client/

chown -R detroitharps /detroitharps

service nginx start