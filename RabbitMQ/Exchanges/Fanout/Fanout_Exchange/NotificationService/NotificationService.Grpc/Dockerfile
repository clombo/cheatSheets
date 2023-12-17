FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NotificationService.Grpc/NotificationService.Grpc.csproj", "NotificationService.Grpc/"]
RUN dotnet restore "NotificationService.Grpc/NotificationService.Grpc.csproj"
COPY . .
WORKDIR "/src/NotificationService.Grpc"
RUN dotnet build "NotificationService.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationService.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.Grpc.dll"]
