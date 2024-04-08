FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./Asteria.WebApp/Asteria.WebApp.csproj", "./"]
RUN dotnet restore "Asteria.WebApp.csproj"
COPY . .
RUN rm -rf "./Asteria.WebApp/obj" "./Asteria.WebApp/bin"
WORKDIR "/src/."
USER root
RUN dotnet build "Asteria.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Asteria.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
USER app
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Asteria.WebApp.dll"]