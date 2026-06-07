# Stage 1: Build the application using the full .NET 8 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copy solution and project structure for package caching
COPY Main.sln ./
COPY Main.WebAppCore/Main.WebAppCore.csproj ./Main.WebAppCore/

# 2. Restore dependencies
RUN dotnet restore Main.sln

# 3. Copy remaining source code and publish the compiled binaries
COPY . .
WORKDIR /src/Main.WebAppCore
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# --- Stage 2: Runtime Environment ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# 4. Open ports for the web server
EXPOSE 8080

# 5. Copy only the compiled app from the build stage
COPY --from=build /app/publish .

# 6. Define the start command
ENTRYPOINT ["dotnet", "Main.WebAppCore.dll"]
