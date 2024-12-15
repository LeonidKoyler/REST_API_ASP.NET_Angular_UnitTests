# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
 


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Calculation.API/Calculation.API.csproj", "Calculation.API/"]
COPY ["Calculate.Contract/Calculation.Contract.csproj", "Calculate.Contract/"]
COPY ["Calculation.Utility/Calculation.Common.csproj", "Calculation.Utility/"]
COPY ["Calculation.Application/Calculation.Application.csproj", "Calculation.Application/"]
COPY ["Calculation.Domain/Calculation.Domain.csproj", "Calculation.Domain/"]
RUN dotnet restore "./Calculation.API/Calculation.API.csproj"
COPY . .
WORKDIR "/src/Calculation.API"
RUN dotnet build "./Calculation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Calculation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
ENV ASPNETCORE_HTTP_PORTS=80
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calculation.API.dll"]