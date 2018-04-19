service nginx stop

rm -r /detroitharps/client/*

cp -r /deploy/api/client/dist/* /detroitharps/client/

chown -R detroitharps /detroitharps

service nginx start