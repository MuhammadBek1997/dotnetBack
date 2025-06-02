# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FudballManagement.API.csproj", "."]
RUN dotnet restore "FudballManagement.API.csproj"
COPY . .
RUN dotnet publish "FudballManagement.API.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "FudballManagement.API.dll"]