# 🟧 GPressFeed ⬛

Welcome to [GPressFeed](https://www.gpressfeed.com), a web application designed to aggregate and display news articles from various sources. This README provides installation instructions and important configuration details.

## About 📰

The purpose of this application is to bring a project from the idea stage to a finished product with a single requirement: self-hosting.
To keep the documentation concise, I'll highlight the most noteworthy aspect of this project—the infrastructure.

The traffic is redirected using a tunnel to the Reverse Proxy (`./ReverseProxy/nginx.conf`), splitting the traffic. By using Regex, all traffic with the starting path `/api/pressfeed` will be redirected to the `GPressFeed.API` container, while everything else flows toward the `GPressFeed.UI` container. The NGINX Reverse Proxy is also configured to rely on the Docker DNS Resolver, allowing the use of `gpressfeedapi` and `gpressfeedui` domain names instead of Docker container IP addresses.

## Requirements 📋

Running the application is as simple as having Docker installed, but for development purposes, the following dependencies must be installed:

1. .NET 6+ SDK
2. Node v16+

## Installation 🛠️

1. Clone the Repository.
2. Run `docker-compose build` in the root directory of the project (the one containing the `docker-compose.yml` file).
3. Run `docker-compose up` in the same directory.

Note: The application runs on port 80; ensure that it is free.

Voila, typing `localhost` in any browser should lead you to the application.

## Deploying 🚀

To deploy the application, some configurations must be changed. This section is divided into multiple segments to cover each part of the project clearly.

### docker-compose.yml

Within this file, make the following changes:

1. Uncomment the tunnel section and replace the `cloudflareToken` argument with your [Cloudflare Tunnel token](https://developers.cloudflare.com/cloudflare-one/connections/connect-networks/get-started/create-remote-tunnel/).
2. Replace the `POSTGRES_PASSWORD` environment variable value with your own. Ensure it matches the password in `./GPressFeed.API/Hosts/GPressFeed.API/appsettings.json`.

### GPressFeed.API

1. Inside the `./GPressFeed.API/Dockerfile`, remove the `-p:Configuration=Development` argument to enable CORS.
2. Change the following credentials in `./GPressFeed.API/Hosts/GPressFeed.API/appsettings.json`: `dbpassword`, `yourgoogleapikey`, `CorsConfiguration.OriginAddress` (based on the domain name you intend to use for the Cloudflare Tunnel).

### GPressFeed.UI

For the UI part of this project, the change is simple. In `./GPressFeed.UI/.env`, change the value of `PUBLIC_IS_DEVELOPMENT_ENV` to `False`.

Congratulations! With a bit of luck, the application should be deployed on your computer, with the tunnel forwarding traffic from the Domain Name to GPressFeed.

## License 📜

GPressfeed is licensed under the [MIT License](https://chat.openai.com/c/LICENSE). Please review the license before using or contributing to the project.
