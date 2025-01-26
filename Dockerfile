FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/HotelUp.Employee.API/HotelUp.Employee.API.csproj", "Src/HotelUp.Employee.API/"]
COPY ["Src/HotelUp.Employee.Services/HotelUp.Employee.Services.csproj", "Src/HotelUp.Employee.Services/"]
COPY ["Src/HotelUp.Employee.Persistence/HotelUp.Employee.Persistence.csproj", "Src/HotelUp.Employee.Persistence/"]
COPY ["Shared/HotelUp.Employee.Shared/HotelUp.Employee.Shared.csproj", "Shared/HotelUp.Employee.Shared/"]
RUN dotnet restore "Src/HotelUp.Employee.API/HotelUp.Employee.API.csproj"
COPY . .
WORKDIR "/src/Src/HotelUp.Employee.API"
RUN dotnet build "HotelUp.Employee.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HotelUp.Employee.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl --silent --fail http://localhost:5002/api/lowercase_module_name/_health || exit 1
ENTRYPOINT ["dotnet", "HotelUp.Employee.API.dll"]
