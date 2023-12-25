FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WeatherRepository.API/WeatherRepository.API.csproj", "WeatherRepository.API/"]
RUN dotnet restore "WeatherRepository.API/WeatherRepository.API.csproj"
COPY . .
WORKDIR "/src/WeatherRepository.API"
RUN dotnet build "WeatherRepository.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherRepository.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherRepository.API.dll"]
