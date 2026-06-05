# Stage 1: Build the application using the full .NET SDK
FROM ://microsoft.com AS build
WORKDIR /src

# Copy all .csproj files to optimize NuGet package caching
COPY ["Main.WebAppCore/Main.WebAppCore.csproj", "Main.WebAppCore/"]
COPY ["Main.Services/Main.Services.csproj", "Main.Services/"]
COPY ["Main.Infrastructure/Main.Infrastructure.csproj", "Main.Infrastructure/"]
COPY ["Main.Model/Main.Model.csproj", "Main.Model/"]
COPY ["Main.Common/Main.Common.csproj", "Main.Common/"]
COPY ["ResourceLibrary/ResourceLibrary.csproj", "ResourceLibrary/"]
COPY ["*.sln", "./"]

# Restore dependencies for the entire solution layout
RUN dotnet restore

# Copy the actual source code files into the container
COPY . .

# Change workspace to the Web project directory
WORKDIR "/src/Main.WebAppCore"

# Build, compile dependencies, and publish binaries
# Added --no-restore since 'dotnet restore' was already executed above
RUN dotnet publish "Main.WebAppCore.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

# Stage 2: Final runtime image using the lightweight ASP.NET Core runtime
FROM ://microsoft.com AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose container network ports (.NET 8 defaults to 8080/8081)
EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "Main.WebAppCore.dll"]
