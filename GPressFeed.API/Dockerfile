# Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

COPY . .

RUN dotnet publish "./Hosts/GPressFeed.API" -c release -o /app 

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Serve
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "GPressFeed.API.dll"]
