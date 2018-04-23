## TODO : make this less "magic stringy", maybe take "/deploy/client" as parameter?

## check if the folders exist and delete contents
if [ -d "/detroitharps/client" ]
then
    rm -r /detroitharps/client
else
    echo "Folder does not exist at /detroitharps/client"
fi

## this is a hack
environment="$1"

mkdir -p /detroitharps/client

cp -r /deploy/client/dist/* /detroitharps/client/

echo "Files copied over"

chown -R detroitharps /detroitharps

service nginx stop

echo "nginx config..."
if [ "$environment" = "dev" ]
then
    rm -rf /etc/nginx/sites-available/dev.detroitharps.com
    rm -rf /etc/nginx/sites-enabled/dev.detroitharps.com
    cp /deploy/client/scripts/files/dev.detroitharps.com /etc/nginx/sites-available/
    ln -s /etc/nginx/sites-available/dev.detroitharps.com /etc/nginx/sites-enabled/dev.detroitharps.com
else
    rm -rf /etc/nginx/sites-available/detroitharps.com
    rm -rf /etc/nginx/sites-enabled/detroitharps.com
    cp /deploy/client/scripts/files/detroitharps.com /etc/nginx/sites-available/
    ln -s /etc/nginx/sites-available/detroitharps.com /etc/nginx/sites-enabled/detroitharps.com
fi

service nginx start

echo "Done!"