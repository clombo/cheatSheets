FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FileManagementService.Grpc/FileManagementService.Grpc.csproj", "FileManagementService.Grpc/"]
RUN dotnet restore "FileManagementService.Grpc/FileManagementService.Grpc.csproj"
COPY . .
WORKDIR "/src/FileManagementService.Grpc"
RUN dotnet build "FileManagementService.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileManagementService.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileManagementService.Grpc.dll"]
