FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OfficeService.Grpc/OfficeService.Grpc.csproj", "OfficeService.Grpc/"]
RUN dotnet restore "OfficeService.Grpc/OfficeService.Grpc.csproj"
COPY . .
WORKDIR "/src/OfficeService.Grpc"
RUN dotnet build "OfficeService.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OfficeService.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OfficeService.Grpc.dll"]
