FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["StoreService.Grpc/StoreService.Grpc.csproj", "StoreService.Grpc/"]
RUN dotnet restore "StoreService.Grpc/StoreService.Grpc.csproj"
COPY . .
WORKDIR "/src/StoreService.Grpc"
RUN dotnet build "StoreService.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StoreService.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StoreService.Grpc.dll"]
