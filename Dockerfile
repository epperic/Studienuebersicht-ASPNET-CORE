FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Studienuebersicht.sln Studienuebersicht.sln
COPY Studienuebersicht.Model Studienuebersicht.Model
COPY Studienuebersicht Studienuebersicht
COPY Studienuebersicht.EFCore Studienuebersicht.EFCore
RUN dotnet restore Studienuebersicht/Studienuebersicht.MVC.csproj

# Copy everything else and build
RUN dotnet publish -c Release -o out ./Studienuebersicht/Studienuebersicht.MVC.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Studienuebersicht.MVC.dll"]