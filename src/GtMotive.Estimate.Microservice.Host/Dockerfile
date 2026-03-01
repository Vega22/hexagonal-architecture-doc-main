FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
ENV DOTNET_EnableHWIntrinsic=0
ENV COMPlus_EnableHWIntrinsic=0

COPY ["Directory.Build.props", "."]
COPY ["Directory.Build.targets", "."]
COPY ["NuGet.config", "."]

COPY ["src/GtMotive.Estimate.Microservice.Host/GtMotive.Estimate.Microservice.Host.csproj", "src/GtMotive.Estimate.Microservice.Host/"]
COPY ["src/GtMotive.Estimate.Microservice.Api/GtMotive.Estimate.Microservice.Api.csproj", "src/GtMotive.Estimate.Microservice.Api/"]
COPY ["src/GtMotive.Estimate.Microservice.ApplicationCore/GtMotive.Estimate.Microservice.ApplicationCore.csproj", "src/GtMotive.Estimate.Microservice.ApplicationCore/"]
COPY ["src/GtMotive.Estimate.Microservice.Infrastructure/GtMotive.Estimate.Microservice.Infrastructure.csproj", "src/GtMotive.Estimate.Microservice.Infrastructure/"]
COPY ["src/GtMotive.Estimate.Microservice.Domain/GtMotive.Estimate.Microservice.Domain.csproj", "src/GtMotive.Estimate.Microservice.Domain/"]

RUN dotnet restore "src/GtMotive.Estimate.Microservice.Host/GtMotive.Estimate.Microservice.Host.csproj" --disable-parallel
COPY . .

RUN dotnet build "src/GtMotive.Estimate.Microservice.Host/GtMotive.Estimate.Microservice.Host.csproj" \
    -c Release \
    -o /app/build \
    --no-restore \
    /p:RunAnalyzers=false \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "GtMotive.Estimate.Microservice.Host.dll"]