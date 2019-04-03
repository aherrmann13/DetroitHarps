server {
    root /detroitharps/application/Client/;
    index index.html index.htm;
    server_name detroitharps.com;

    if ($host = www.detroitharps.com) {		
        return 301 https://detroitharps.com$request_uri;		
    }

    location / {
        try_files $uri $uri/ /index.html;
    }

    listen 443 ssl; # managed by Certbot
    ssl_certificate /etc/letsencrypt/live/detroitharps.com/fullchain.pem; # managed by Certbot
    ssl_certificate_key /etc/letsencrypt/live/detroitharps.com/privkey.pem; # managed by Certbot
    include /etc/letsencrypt/options-ssl-nginx.conf; # managed by Certbot
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem; # managed by Certbot


}
server {
    if ($host = detroitharps.com) {
        return 301 https://$host$request_uri;
    } # managed by Certbot


    listen 80 default_server;
    server_name detroitharps.com;
    return 404; # managed by Certbot


}
