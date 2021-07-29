# OAuthGitHub.Api

[![.NET](https://github.com/afgalvan/OAuthGitHub/actions/workflows/dotnet.yml/badge.svg)](https://github.com/afgalvan/OAuthGitHub/actions/workflows/dotnet.yml)

## Run it from Docker

```bash
# Run & build the containers in background
docker-compose -f docker/docker-compose.yml up --build -d
# Then, to see the nginx logs
docker attach api_proxy

# Or in attached mode
docker-compose -f docker/docker-compose.yml up --build
```

Open a browser and go to <http://localhost/WeatherForecast>
