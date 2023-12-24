FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AgreementsEventLog.Grpc/AgreementsEventLog.Grpc.csproj", "AgreementsEventLog.Grpc/"]
RUN dotnet restore "AgreementsEventLog.Grpc/AgreementsEventLog.Grpc.csproj"
COPY . .
WORKDIR "/src/AgreementsEventLog.Grpc"
RUN dotnet build "AgreementsEventLog.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgreementsEventLog.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgreementsEventLog.Grpc.dll"]
