server {
    listen 80 default_server;
    root /detroitharps/client;
    index index.html index.htm;
    server_name dev.detroitharps.com;
    location / {
        try_files $uri $uri/ /index.html;
    }
}