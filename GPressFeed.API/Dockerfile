# Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

COPY . .

# Remove '-p:Configuration=Development' when going Prod
RUN dotnet publish "./Hosts/GPressFeed.API" -p:Configuration=Development -c release -o /app 

ENV PATH="$PATH:/root/.dotnet/tools"

# Serve
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "GPressFeed.API.dll"]
