server {
    root /detroitharps/application/Client/;
    index index.html index.htm;
    server_name dev.detroitharps.com;

    location / {
        try_files $uri $uri/ /index.html;
    }

    listen 443 ssl; # managed by Certbot
    ssl_certificate /etc/letsencrypt/live/dev.detroitharps.com/fullchain.pem; # managed by Certbot
    ssl_certificate_key /etc/letsencrypt/live/dev.detroitharps.com/privkey.pem; # managed by Certbot
    include /etc/letsencrypt/options-ssl-nginx.conf; # managed by Certbot
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem; # managed by Certbot


}
server {
    if ($host = www.dev.detroitharps.com) {
        return 301 https://dev.detroitharps.com$request_uri;
    }


    listen 443 ssl; # managed by Certbot
    server_name www.dev.detroitharps.com;
    ssl_certificate /etc/letsencrypt/live/www.dev.detroitharps.com/fullchain.pem; # managed by Certbot
    ssl_certificate_key /etc/letsencrypt/live/www.dev.detroitharps.com/privkey.pem; # managed by Certbot
    include /etc/letsencrypt/options-ssl-nginx.conf; # managed by Certbot
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem; # managed by Certbot
}
server {
    if ($host = dev.detroitharps.com) {
        return 301 https://$host$request_uri;
    } # managed by Certbot

    if ($host = www.dev.detroitharps.com) {
        return 301 https://$host$request_uri;
    }
 
 
    listen 80 default_server;
    server_name www.dev.detroitharps.com dev.detroitharps.com;
    return 404; # managed by Certbot
}
