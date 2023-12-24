FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AgreementsService.API/AgreementsService.API.csproj", "AgreementsService.API/"]
RUN dotnet restore "AgreementsService.API/AgreementsService.API.csproj"
COPY . .
WORKDIR "/src/AgreementsService.API"
RUN dotnet build "AgreementsService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgreementsService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgreementsService.API.dll"]
