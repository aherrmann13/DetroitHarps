server {
    listen 80;
    root /detroitharps/application/Client/;
    index index.html index.htm;
    server_name detroitharps.com 134.209.77.76;
    location / {
        try_files $uri $uri/ =404;
    }
}