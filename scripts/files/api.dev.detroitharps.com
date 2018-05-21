server {
    server_name   api.dev.detroitharps.com;
    location / {
        proxy_pass         http://localhost:5501;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $http_host;
        proxy_cache_bypass $http_upgrade;
    }

    listen 443 ssl; # managed by Certbot
    ssl_certificate /etc/letsencrypt/live/api.dev.detroitharps.com/fullchain.pem; # managed by Ce$
    ssl_certificate_key /etc/letsencrypt/live/api.dev.detroitharps.com/privkey.pem; # managed by $
    include /etc/letsencrypt/options-ssl-nginx.conf; # managed by Certbot
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem; # managed by Certbot

}server {
    if ($host = api.dev.detroitharps.com) {
        return 301 https://$host$request_uri;
    } # managed by Certbot


    listen        80;
    server_name   api.dev.detroitharps.com;
    return 404; # managed by Certbot
}