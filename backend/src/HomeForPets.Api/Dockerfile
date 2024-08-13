FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/HomeForPets.Api/HomeForPets.Api.csproj", "src/HomeForPets.Api/"]
COPY ["src/HomeForPets.Application/HomeForPets.Application.csproj", "src/HomeForPets.Application/"]
COPY ["src/HomeForPets.Domain/HomeForPets.Domain.csproj", "src/HomeForPets.Domain/"]
COPY ["src/HomeForPets.Infrastructure/HomeForPets.Infrastructure.csproj", "src/HomeForPets.Infrastructure/"]
RUN dotnet restore "src/HomeForPets.Api/HomeForPets.Api.csproj"
COPY . .
WORKDIR "/src/src/HomeForPets.Api"
RUN dotnet build "HomeForPets.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HomeForPets.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeForPets.Api.dll"]
