FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
USER root

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Asteria.WebApp/Asteria.WebApp.csproj", "Asteria.WebApp/"]
RUN dotnet restore "./Asteria.WebApp/Asteria.WebApp.csproj"
COPY . .
WORKDIR "/src/Asteria.WebApp"
USER root
RUN dotnet build "./Asteria.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Asteria.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
USER root
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Asteria.WebApp.dll"]