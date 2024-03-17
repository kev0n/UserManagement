FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/UserRegistrationService/UserRegistrationService.csproj", "src/UserRegistrationService/"]
RUN dotnet restore "src/UserRegistrationService/UserRegistrationService.csproj"
COPY . .
WORKDIR "/src/src/UserRegistrationService"
RUN dotnet build "UserRegistrationService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserRegistrationService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserRegistrationService.dll"]
