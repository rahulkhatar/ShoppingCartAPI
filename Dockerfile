FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ShoppingCart.API/ShoppingCart.API.csproj", "ShoppingCart.API/"]
COPY ["ShoppingCart.Infrastucture/ShoppingCart.Infrastucture.csproj", "ShoppingCart.Infrastucture/"]
COPY ["ShoppingCart.Core/ShoppingCart.Core.csproj", "ShoppingCart.Core/"]

RUN dotnet restore "./ShoppingCart.API/ShoppingCart.API.csproj"
COPY . .
WORKDIR "/src/ShoppingCart.API"
RUN dotnet build "./ShoppingCart.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ShoppingCart.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingCart.API.dll"]