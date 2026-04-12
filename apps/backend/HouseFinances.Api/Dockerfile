# Build stage — context is apps/backend/
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files first for layer-cached restore
COPY HouseFinances.Domain/HouseFinances.Domain.csproj           HouseFinances.Domain/
COPY HouseFinances.Application/HouseFinances.Application.csproj HouseFinances.Application/
COPY HouseFinances.Infrastructure/HouseFinances.Infrastructure.csproj HouseFinances.Infrastructure/
COPY HouseFinances.Api/HouseFinances.Api.csproj                 HouseFinances.Api/

RUN dotnet restore HouseFinances.Api/HouseFinances.Api.csproj

COPY . .
RUN dotnet publish HouseFinances.Api/HouseFinances.Api.csproj \
    -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "HouseFinances.Api.dll"]
