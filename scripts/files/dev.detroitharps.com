server {
    root /detroitharps/client;
    index index.html index.htm;
    server_name dev.detroitharps.com www.dev.detroitharps.com;
    location / {
        try_files $uri $uri/ /index.html;
    }

    listen 443 ssl; # managed by Certbot
    ssl_certificate /etc/letsencrypt/live/dev.detroitharps.com/fullchain.pem; # managed by Certbot
    ssl_certificate_key /etc/letsencrypt/live/dev.detroitharps.com/privkey.pem; # managed by Cert$
    include /etc/letsencrypt/options-ssl-nginx.conf; # managed by Certbot
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem; # managed by Certbot


}
server {
    if ($host = www.dev.detroitharps.com) {
        return 301 https://$host$request_uri;
    } # managed by Certbot


    if ($host = dev.detroitharps.com) {
        return 301 https://$host$request_uri;
    } # managed by Certbot


    listen 80 default_server;
    server_name dev.detroitharps.com www.dev.detroitharps.com;
    return 404; # managed by Certbot




}
