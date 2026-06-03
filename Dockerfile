# STAGE 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["PaulAlarba.csproj", "./"]
RUN dotnet restore "PaulAlarba.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "PaulAlarba.csproj" -c Release -o /app/build

# STAGE 2: Publish
FROM build AS publish
RUN dotnet publish "PaulAlarba.csproj" -c Release -o /app/publish /p:UseAppHost=false

# STAGE 3: Final Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the entry point to your app's dll
ENTRYPOINT ["dotnet", "PaulAlarba.dll"]
