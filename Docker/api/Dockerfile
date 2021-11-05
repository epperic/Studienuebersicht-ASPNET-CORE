FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Studienuebersicht.sln Studienuebersicht.sln
COPY Studienuebersicht.Model Studienuebersicht.Model
COPY Studienuebersicht.API Studienuebersicht.API
COPY Studienuebersicht.EFCore Studienuebersicht.EFCore
RUN dotnet restore Studienuebersicht.API/Studienuebersicht.API.csproj

# Copy everything else and build
RUN dotnet publish -c Release -o outAPI ./Studienuebersicht.API/Studienuebersicht.API.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/outAPI .
ENTRYPOINT ["dotnet", "Studienuebersicht.API.dll"]