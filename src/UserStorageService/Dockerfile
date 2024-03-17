FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/UserStorageService/UserStorageService.csproj", "src/UserStorageService/"]
RUN dotnet restore "src/UserStorageService/UserStorageService.csproj"
COPY . .
WORKDIR "/src/src/UserStorageService"
RUN dotnet build "UserStorageService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserStorageService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserStorageService.dll"]
