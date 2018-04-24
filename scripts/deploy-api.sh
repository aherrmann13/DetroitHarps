
## TODO : make this less "magic stringy", maybe take "/deploy/api" as parameter?

## Check if service is running
## TODO : install if not there
if [ "`service detroitharps-api status`" == "Unit detroitharps-api.service could not be found." ] 
then 
        echo "detroitharps api needs to be installed as a service"
        exit 1
fi

## this is a hack
environment="$1"

systemctl stop detroitharps-api.service

## check if the folders exist and delete contents
if [ -d "/detroitharps/api" ]
then
    rm -r /detroitharps/api
else
    echo "Folder does not exist at /detroitharps/api"
fi

if [ -d "/detroitharps/tools" ]
then
    rm -r /detroitharps/tools
else
    echo "Folder does not exist at /detroitharps/tools"
fi

mkdir -p /detroitharps/api
mkdir -p /detroitharps/tools

cp -r /deploy/api/Repository.Migrator /detroitharps/tools/
cp -r /deploy/api/Repository.Dataloader /detroitharps/tools/

cp -r /deploy/api/Service /detroitharps/api/

echo "Files copied over"

if [ "$environment" = "dev" ]
then
    echo "Deleting db..."

    dotnet /detroitharps/tools/Repository.Migrator/Repository.Migrator.dll Delete
fi

if [ ! "$environment" = "dev" ]
then
    echo "copying appsettings.json"
    if [ ! -f /root/appsettings.json ]
    then
        echo "appsettings.json not found!"
        exit 1
    fi
    rm /detroitharps/tools/Repository.Migrator/appsettings*.json
    rm /detroitharps/tools/Repository.Dataloader/appsettings*.json
    rm /detroitharps/api/Service/appsettings*.json

    cp /root/appsettings.json /detroitharps/tools/Repository.Migrator/
    cp /root/appsettings.json /detroitharps/tools/Repository.Dataloader/
    cp /root/appsettings.json /detroitharps/api/Service/
fi


echo "Migrating database..."

dotnet /detroitharps/tools/Repository.Migrator/Repository.Migrator.dll Migrate

if [ "$environment" = "dev" ]
then
    echo "Running dataloader..."

    dotnet /detroitharps/tools/Repository.Dataloader/Repository.Dataloader.dll
fi

systemctl start detroitharps-api.service

service nginx stop

chown -R detroitharps /detroitharps

echo "nginx config..."
if [ "$environment" = "dev" ]
then
    rm -rf /etc/nginx/sites-available/api.dev.detroitharps.com
    rm -rf /etc/nginx/sites-enabled/api.dev.detroitharps.com
    cp /deploy/api/scripts/files/api.dev.detroitharps.com /etc/nginx/sites-available/
    ln -s /etc/nginx/sites-available/api.dev.detroitharps.com /etc/nginx/sites-enabled/api.dev.detroitharps.com
else
    rm -rf /etc/nginx/sites-available/api.detroitharps.com
    rm -rf /etc/nginx/sites-enabled/api.detroitharps.com
    cp /deploy/api/scripts/files/api.detroitharps.com /etc/nginx/sites-available/
    ln -s /etc/nginx/sites-available/api.detroitharps.com /etc/nginx/sites-enabled/api.detroitharps.com
fi

service nginx start

echo "Done!"