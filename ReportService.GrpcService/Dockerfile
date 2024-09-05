FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ReportService.GrpcService/ReportService.GrpcService.csproj", "ReportService.GrpcService/"]
COPY ["ReportService.Data/ReportService.Data.csproj", "ReportService.Data/"]
RUN dotnet restore "ReportService.GrpcService/ReportService.GrpcService.csproj"
COPY . .
WORKDIR "/src/ReportService.GrpcService"
RUN dotnet build "ReportService.GrpcService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ReportService.GrpcService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportService.GrpcService.dll"]
