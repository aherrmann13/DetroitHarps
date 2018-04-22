server {
    listen        80;
    server_name   api.detroitharps.com;
    location / {
        proxy_pass         http://localhost:5501;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $http_host;
        proxy_cache_bypass $http_upgrade;
    }
}