# Stage 1: Build the application using the full .NET 8 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./

# Copy all .csproj files to optimize NuGet package caching
COPY Main.WebAppCore/Main.WebAppCore.csproj ./Main.WebAppCore/
COPY Main.Services/Main.Services.csproj ./Main.Services/
COPY Main.Infrastructure/Main.Infrastructure.csproj ./Main.Infrastructure/
COPY Main.Model/Main.Model.csproj ./Main.Model/
COPY Main.Common/Main.Common.csproj ./Main.Common/
COPY ResourceLibrary/ResourceLibrary.csproj ./ResourceLibrary/


# Restore dependencies for the entire solution layout
RUN dotnet restore

# Copy the actual source code files into the container
COPY . .

# Change workspace to the Web project directory
WORKDIR "/src/Main.WebAppCore"

# Build, compile dependencies, and publish binaries
RUN dotnet publish "Main.WebAppCore.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

# Stage 2: Final runtime image using the lightweight ASP.NET 8.0 runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# .NET 8 container environments run as non-root (UID 1654) by default for security
USER $APP_UID

# Expose .NET 8 default non-root container network ports
EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "Main.WebAppCore.dll"]